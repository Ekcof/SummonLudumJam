using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GridPrefabContainer", menuName = "Game Resources/Grid Prefab Container")]
[Serializable]
public class GridPrefabContainer : ScriptableObject
{
    [SerializeField] private GridObject[] _prefabs;

    public GridObject GetPrefabByType(Type type)
    {
        return _prefabs.FirstOrDefault(x => x.GetType() == type);
    }

    public GridObject GetPrefabById(string id)
    {
        return _prefabs.FirstOrDefault(x => x.Id == id);
    }
}