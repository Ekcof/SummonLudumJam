using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public enum HexagonalDirection
{
    up = 0,
    upright = 60,
    downright = 120,
    down = 180,
    downleft = 240,
    upleft = 300
}

public class LightBall : MonoBehaviour
{
    [Inject] HexagonalMap _hexMap;
    [SerializeField] HexagonalDirection _direction;
    [SerializeField] float _speed = 5.5f;
    private Vector2Int _currentCell;

    [ContextMenu("Initialize")]
    public void Initialize(Vector3 position, HexagonalDirection direction)
    {
        transform.position = position;
        _direction = direction;
        _currentCell = _hexMap.GetCurrentCell(transform.position);
        transform.position = _hexMap.GetWorldCoordinatesOfCell(_currentCell);
        MoveToNextCell();
    }

    private void MoveToNextCell()
    {
        DOTween.Kill(transform);
        Vector2Int nextCell = _hexMap.GetNextCell(transform, _direction);
        Vector2 nextCellPos = _hexMap.GetWorldCoordinatesOfCell(nextCell);

        transform.DOMove(nextCellPos, _speed).SetEase(Ease.Linear).OnComplete(
            _hexMap.IsCellFree(nextCell) ?
            MoveToNextCell :
            () => CheckObstacle(nextCell));
    }

    private void CheckObstacle(Vector2Int cell)
    {
        var gridObject = _hexMap.GetObjectAtCell(cell);
        if (gridObject != null && gridObject is Stonen prism)
        {
            //_direction = prism.Direction;
            MoveToNextCell();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        
    }
}
