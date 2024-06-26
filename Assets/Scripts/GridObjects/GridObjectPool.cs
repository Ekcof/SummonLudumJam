using Zenject;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class GridObjectPool : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [Inject] private GridPrefabContainer _prefabContainer;
    [Inject] private Bar bar;

    private readonly Dictionary<Stone, StoneType> _activeObjects = new();
    private readonly Dictionary<Stone, StoneType> _hiddenObjects = new();

    private void Awake()
    {
        EventsBus.Subscribe<OnFinishSummon>(this, OnFinishSummon);
    }

    private void OnFinishSummon(OnFinishSummon data)
    {
        foreach (var kvp in _activeObjects)
        {
            Pool(kvp.Key);
        }   
    }

    public void Pool(Stone arg)
    {
        _activeObjects.Remove(arg);
        bar.UpdateBar(_activeObjects.Count);

        if (_hiddenObjects.ContainsKey(arg))
            return;
        _hiddenObjects.Add(arg, arg.StoneType);
    }

    public Stone Get(StoneType type)
    {
        if (!_hiddenObjects.ContainsValue(type))
        {
            var obj = InstantiatePrefab(type);
            _activeObjects.Add(obj, type);
            bar.UpdateBar(_activeObjects.Count);

            return obj;
        }
        else
        {
            var obj = _hiddenObjects.FirstOrDefault(x => x.Value == type).Key;                    
            _activeObjects.Add(obj, type);
            bar.UpdateBar(_activeObjects.Count);
            _hiddenObjects.Remove(obj);
            return obj;
        }
    }

    public Stone GetByStoneType(StoneType type)
    {
        if (!_hiddenObjects.ContainsValue(type))
        {
            var obj = InstantiatePrefab(type);
            _activeObjects.Add(obj, type);
            return obj;
        }
        else
        {
            var obj = _hiddenObjects.FirstOrDefault(x => x.Value == type).Key;
            _activeObjects.Add(obj, type);
            _hiddenObjects.Remove(obj);
            return obj;
        }
    }

    private Stone InstantiatePrefab(StoneType prefabType)
    {
        var prefab = _prefabContainer.GetPrefabByStoneType(prefabType);
        if (prefab == null)
        {
            return null;
        }
        return _diContainer != null ?
           _diContainer.InstantiatePrefabForComponent<Stone>(prefab, transform) :
           Instantiate(prefab, transform);
    }
}