using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class EventsBus
{
    private class Subscription
    {
        public WeakReference Subscriber { get; set; }
        public MethodInfo Method { get; set; }
    }

    private static readonly Dictionary<Type, List<Subscription>> eventSubscriptions = new Dictionary<Type, List<Subscription>>();

    public static void Subscribe<T>(object subscriber, Action<T> eventHandler)
    {
        Type eventType = typeof(T);

        if (!eventSubscriptions.ContainsKey(eventType))
        {
            eventSubscriptions[eventType] = new List<Subscription>();
        }

        eventSubscriptions[eventType].Add(new Subscription
        {
            Subscriber = new WeakReference(subscriber),
            Method = eventHandler.Method
        });
    }

    public static void Publish<T>(T eventData)
    {
        Type eventType = typeof(T);

        if (eventSubscriptions.TryGetValue(eventType, out var subscriptions))
        {
            var deadSubscriptions = new List<Subscription>();

            foreach (var subscription in subscriptions)
            {
                if (subscription.Subscriber.IsAlive)
                {
                    var target = subscription.Subscriber.Target;

                    if (target is MonoBehaviour monoBehaviourTarget)
                    {
                        if (monoBehaviourTarget == null)
                        {
                            deadSubscriptions.Add(subscription);
                            continue;
                        }
                    }

                    try
                    {
                        subscription.Method.Invoke(target, new object[] { eventData });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error invoking event handler: {ex.Message}");
                    }
                }
                else
                {
                    deadSubscriptions.Add(subscription);
                }
            }

            foreach (var dead in deadSubscriptions)
            {
                subscriptions.Remove(dead);
            }
        }
    }
}