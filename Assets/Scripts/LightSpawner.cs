using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LightSpawner : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [SerializeField] LightBall _prefab;
    [SerializeField] HexagonalDirection _direction;

    private void Awake()
    {
        EventsBus.Subscribe<OnDemandToSpawnLightBall>(this,OnDemandToSpawnLightBall);
    }

    private void OnDemandToSpawnLightBall(OnDemandToSpawnLightBall data)
    {
        // условие на спаун
        Spawn();
    }

    private void Spawn()
    {
        var light = _diContainer.InstantiatePrefabForComponent<LightBall>(_prefab, transform);
        light.Initialize(transform.position, _direction);
    }
}
