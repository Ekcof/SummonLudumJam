using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Provisional code for testing input in editor
/// </summary>
public class InputManager : MonoBehaviour
{
    [Inject] private GridObjectPool _pool;
    [Inject] private HexagonalMap _map;
    [Inject(Id = "mainCamera")] private Camera _camera;

    void Start()
    {

    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (_map.TryGetFreeCell(worldPosition, out var cell))
            {
                _map.TryToPlaceGridObjectAtCell(() => _pool.Get(typeof(Prism)), cell);
            }
            else
            {
                var gridObj = _map.GetObjectAtCell(cell);
                if (gridObj != null)
                {
                    if (gridObj is Prism prism)
                    {

                    }
                }
            }
        }
#endif
    }
}
