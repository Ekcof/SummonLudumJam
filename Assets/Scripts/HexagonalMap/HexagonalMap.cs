﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexagonalMap : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private Tilemap _floorTilemap;
    [SerializeField][Range(0f, 1f)] private float _maxDistanceRelation = 0.4f;
    private readonly Dictionary<Vector2Int, Stone> _gridObjects = new();
    private List<Vector2Int> _freeTiles;
    private List<Vector2Int> _obstacleTiles;
    public Vector2 CellSize => _grid.cellSize;
    public Dictionary<Vector2Int, Stone> GridObjects => _gridObjects;

    private void Awake()
    {
        _freeTiles = GetTexturedCells(_floorTilemap, _obstacleTiles);
        EventsBus.Subscribe<OnFinishSummon>(this, OnFinishSummon);
        Debug.Log($"There are {_freeTiles.Count} tiles");
    }

    #region GridMethods
    public List<Vector2Int> GetTexturedCells(Tilemap tilemap, List<Vector2Int> exclusionList = null)
    {
        var texturedCells = new List<Vector2Int>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Vector2Int gridPosition = new Vector2Int(bounds.xMin + x, bounds.yMin + y);
                    if (exclusionList == null || !exclusionList.Contains(gridPosition))
                        texturedCells.Add(gridPosition);
                }
            }
        }

        return texturedCells;
    }

    public Vector2 GetWorldCoordinatesOfCell(Vector2Int cell) => _grid.GetCellCenterWorld((Vector3Int)cell);

    public Vector2Int GetCurrentCell(Vector3 position)
    {
        Vector2Int tempCellPosition = (Vector2Int)_grid.WorldToCell(position);

        Debug.Log($"Current Cell is {tempCellPosition}");
        return tempCellPosition;
    }

    #endregion

    /// <summary>
    /// Returns the center of nearest grid cell
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public bool TryGetFreeCell(Vector3 worldPosition, out Vector2Int cell)
    {
        cell = new Vector2Int();

        Vector3Int tempCellPosition = _grid.WorldToCell(worldPosition);

        Vector3 cellCenter = _grid.GetCellCenterWorld(tempCellPosition);



        Vector3Int nextCellPosition = tempCellPosition + new Vector3Int(1, 0, 0);
        Vector3 nextCellCenter = _grid.GetCellCenterWorld(nextCellPosition);
        float nearestCellDistance = Vector3.Distance(cellCenter, nextCellCenter);

        cell = (Vector2Int)tempCellPosition;

        if (GetObjectAtCell(cell) == null && IsCellFree((Vector2Int)tempCellPosition))
        {
            return true;
        }
        return false;
    }

    public Stone GetObjectAtCell(Vector2Int cell)
    {
        if (_gridObjects.ContainsKey(cell))
        {
            var gridObject = _gridObjects[cell];
            return _gridObjects[cell];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Get it the cell is free from objects and has a floor
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    public bool IsCellFree(Vector2Int cell)
    {
        return _freeTiles.Contains(cell) && GetObjectAtCell(cell) == null;
    }

    public bool TryToPlaceGridObjectAtCell(Func<Stone> OnSpawn, Vector2Int cell)
    {
        if (IsCellFree(cell))
        {
            var obj = OnSpawn?.Invoke();
            if (obj != null)
            {
                Vector2Int cell2Int = new (cell.x, cell.y);
                Vector2 pos = GetWorldCoordinatesOfCell(cell2Int);
                _gridObjects.Add(cell, obj);
                obj.MoveToWorldPosition(pos);
                return true;
            }
        }
        Debug.Log($"Failed to spawn object at {cell}");
        return false;
    }

    public bool TryToRemoveObjectFromCell(Vector2Int cell)
    {
        var obj = GetObjectAtCell(cell);
        if (obj != null)
        {
            obj.OnRemoved();
            _gridObjects.Remove(cell);
            return true;
        }
        return false;
    }

    private void OnFinishSummon(OnFinishSummon data)
    {
        Debug.Log($"GridObjects count is {_gridObjects.Count}");
        //var tempDictionary = new Dictionary<Vector2Int, Stone>(_gridObjects);
        foreach (var obj in _gridObjects)
        {
            obj.Value.OnRemoved();
        }
        _gridObjects.Clear();
    }
}