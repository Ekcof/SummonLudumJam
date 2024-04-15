using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundHolder", menuName = "Game Resources/SoundHolder")]
[Serializable]
public class SoundHolder : ScriptableObject
{
    [SerializeField] private SoundWrapper[] _sounds;

    public SoundWrapper GetSpriteWrapperById(string id)
    {
        return _sounds.FirstOrDefault(x => x.SoundId == id);
    }
}

[Serializable]
public class SoundWrapper
{
    public string SoundId;
    public AudioClip AudioClip;
}