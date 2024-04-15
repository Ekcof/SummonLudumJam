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
    [Inject] private MainGameManager _mainManager;

    [Inject(Id = "mainCamera")] private Camera _camera;
    [SerializeField] private float _doubleClickThreshold = 0.2f;
    private float _lastClickTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_mainManager.IsSummonState)
        {

            Vector3 worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            float timeSinceLastClick = Time.time - _lastClickTime;

            if (_map.TryGetFreeCell(worldPosition, out var cell))
            {
                //Debug.Log($"FFF is null {_pool.Get(_mainManager.CurrentStoneType)}");
                // Try to place a stone if it's type has been selected
                if (_mainManager.CurrentStoneType == StoneType.None || 
                    !_map.TryToPlaceGridObjectAtCell(() => _pool.Get(_mainManager.CurrentStoneType), cell))
                    EventsBus.Publish(new OnSelectButton { StoneType = StoneType.None });
            }
            else
            {
                Debug.Log("Cell is occupied");
                var gridObj = _map.GetObjectAtCell(cell);
                if (gridObj != null && timeSinceLastClick <= _doubleClickThreshold)
                {
                    Debug.Log("TryToRemoveObjectFromCell");
                    _map.TryToRemoveObjectFromCell(cell);
                }
                EventsBus.Publish(new OnSelectButton { StoneType = StoneType.None });
            }
            _lastClickTime = Time.time;
        }
    }
}
