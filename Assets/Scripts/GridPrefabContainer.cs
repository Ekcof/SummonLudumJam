using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GridPrefabContainer", menuName = "Game Resources/Grid Prefab Container")]
[Serializable]
public class GridPrefabContainer : ScriptableObject
{
    [SerializeField] private Stone[] _prefabs;

    public Stone GetPrefabByType(Type type)
    {
        return _prefabs.FirstOrDefault(x => x.GetType() == type);
    }

    public Stone GetPrefabById(string id)
    {
        return _prefabs.FirstOrDefault(x => x.Id == id);
    }

    public Stone GetPrefabByStoneType(StoneType id)
    {
        return _prefabs.FirstOrDefault(x => x is Stonen stone && stone.StoneType == id);
    }
}