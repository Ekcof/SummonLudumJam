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

    public static void Unsubscribe<T>(object subscriber, Action<T> eventHandler)
    {
        Type eventType = typeof(T);
        if (!eventSubscriptions.ContainsKey(eventType))
        {
            return;
        }

        var list = eventSubscriptions[eventType];
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Subscriber.Target == subscriber)
            {
                list[i].Subscriber.Target = null;
                return;
            }
        }
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

                    // Проверка, является ли подписчик MonoBehaviour.
                    if (target is MonoBehaviour monoBehaviourTarget)
                    {
                        // Для MonoBehaviour проверяем, не был ли объект уничтожен.
                        if (monoBehaviourTarget == null)
                        {
                            // Если MonoBehaviour был уничтожен, считаем подписку "мертвой".
                            deadSubscriptions.Add(subscription);
                            continue; // Пропускаем текущую итерацию цикла.
                        }
                    }

                    try
                    {
                        // Вызываем метод подписки.
                        subscription.Method.Invoke(target, new object[] { eventData });
                    }
                    catch (Exception ex)
                    {
                        // Обработка исключений, возникающих во время вызова метода подписчика.
                        // Например, можно логировать ошибку или обрабатывать её как-то иначе.
                        Console.WriteLine($"Error invoking event handler: {ex.Message}");
                    }
                }
                else
                {
                    // Если подписчик не жив, добавляем подписку в список для удаления.
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