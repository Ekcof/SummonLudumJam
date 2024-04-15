using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GridPrefabContainer", menuName = "Game Resources/Grid Prefab Container")]
[Serializable]
public class GridPrefabContainer : ScriptableObject
{
    [SerializeField] private Stone[] _prefabs;

    public Stone GetPrefabByStoneType(StoneType id)
    {
        return _prefabs.FirstOrDefault(x => x.StoneType == id);
    }
}