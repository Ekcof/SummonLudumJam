using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowerPanel : MonoBehaviour
{
    [SerializeField] private Button _spawnButton;

    private void Awake()
    {
        _spawnButton.onClick.RemoveAllListeners();
        _spawnButton.onClick.AddListener(() => EventsBus.Publish<OnDemandToSpawnLightBall>(new()));
    }

    private void OnDestroy()
    {
        _spawnButton.onClick.RemoveAllListeners();
    }
}
