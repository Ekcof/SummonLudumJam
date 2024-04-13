using Zenject;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class GridObjectPool : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [Inject] private GridPrefabContainer _prefabContainer;
    private readonly Dictionary<GridObject, Type> _activeObjects = new();
    private readonly Dictionary<GridObject, Type> _hiddenObjects = new();

    public void Pool(GridObject arg)
    {
        _activeObjects.Remove(arg);
        if (_hiddenObjects.ContainsKey(arg))
            return;
        _hiddenObjects.Add(arg, arg.GetType());
    }

    public GridObject Get(Type type)
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

    private GridObject InstantiatePrefab(Type prefabType)
    {
        var prefab = _prefabContainer.GetPrefabByType(prefabType);
        if (prefab == null)
        {
            return null;
        }
        return _diContainer != null ?
           _diContainer.InstantiatePrefabForComponent<GridObject>(prefab, transform) :
           Instantiate(prefab, transform);
    }
}