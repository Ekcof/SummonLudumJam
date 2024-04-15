using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteHolder", menuName = "Game Resources/SpriteHolder")]
[Serializable]
public class SpriteHolder : ScriptableObject
{
    [SerializeField] private SpriteWrapper[] _sprites;

    public SpriteWrapper GetSpriteWrapperById(string id)
    {
        return _sprites.FirstOrDefault(x => x.SpriteId == id);
    }
}

[Serializable]
public class SpriteWrapper
{
    public string SpriteId;
    public Sprite Sprite;
}