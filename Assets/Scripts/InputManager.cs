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
    [SerializeField] private float _doubleClickThreshold;
    private float _lastClickTime = 0f;

    void Start()
    {

    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            float timeSinceLastClick = Time.time - _lastClickTime;

            if (_map.TryGetFreeCell(worldPosition, out var cell))
            {
                _map.TryToPlaceGridObjectAtCell(() => _pool.Get(typeof(Stone)), cell);
            }
            else
            {
                var gridObj = _map.GetObjectAtCell(cell);
                if (gridObj != null)
                {
                    if (gridObj is Stone prism)
                    {

                    }
                }
            }

            if (timeSinceLastClick <= _doubleClickThreshold)
            {
                EventsBus.Publish(new OnDoubleClick() { Position = cell });
            }

            _lastClickTime = Time.time;
        }
#endif
    }
}
