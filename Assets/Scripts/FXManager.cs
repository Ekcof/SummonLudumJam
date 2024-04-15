using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FXWrapper
{
    public string Id;
    public GameObject[] GameObjects;
}

public class FXManager : MonoBehaviour
{
    [SerializeField] private FXWrapper[] _fxWrappers;

    public void SetActive(string id, bool active)
    {
        if (_fxWrappers == null || _fxWrappers.Length == 0)
        {
            Debug.Log("No FX are available");
            return;
        }
        var wrapper = GetFXById(id);
        if (wrapper != null)
        {
            foreach (var go in wrapper.GameObjects)
            {
                if (go != null)
                {
                    go.SetActive(active);
                }
            }
        }
    }

    private FXWrapper GetFXById(string id)
    {
        for (int i = 0; i < _fxWrappers.Length; ++i)
        {
            if (_fxWrappers[i].Id == id) return _fxWrappers[i];
        }
        return null;
    }
}
