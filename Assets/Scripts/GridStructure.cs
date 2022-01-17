using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStructure
{
    private int cellSize;
    private int width;
    private int length;

    private Cell[,] grid;

    public GridStructure(int _cellSize, int _width, int _length)
    {
        this.cellSize = _cellSize;
        this.width = _width;
        this.length = _length;

        grid = new Cell[this.width, this.length];
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                grid[row, col] = new Cell();
            }
        }
    }

    public Vector3 CalculateGridPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt((float)inputPosition.x / cellSize);
        int z = Mathf.FloorToInt((float)inputPosition.z / cellSize);
        return new Vector3(x * cellSize, 0, z * cellSize);
    }

    public Vector2Int CalculateGridIndex(Vector3 gridPosition)
    {
        return new Vector2Int((int)gridPosition.x / cellSize, (int)gridPosition.z / cellSize);
    }

    public bool IsCellTaken(Vector3 gridPosition)
    {
        Vector2Int cellIndex = CalculateGridIndex(gridPosition);
       if(IsCellIndexValid(cellIndex))
            return grid[cellIndex.x, cellIndex.y].IsTaken;

        throw new IndexOutOfRangeException("No cell found at " + cellIndex);
    }

    public void PlaceStructureOnGrid(GameObject structure, Vector3 gridPosition)
    {
        Vector2Int cellIndex = CalculateGridIndex(gridPosition);
        if (IsCellIndexValid(cellIndex))
            grid[cellIndex.x,cellIndex.y].BuildStructure(structure);
    }

    public GameObject GetStructureOnGrid(Vector3 gridPosition)
    {
        Vector2Int cellIndex = CalculateGridIndex(gridPosition);
        if (IsCellIndexValid(cellIndex))
        {
            return grid[cellIndex.x, cellIndex.y].GetStructure();
        }
        throw new IndexOutOfRangeException("No cell found at " + cellIndex);
    }

    public void RemoveStructureFromGrid(Vector3 gridPosition)
    {
        Vector2Int cellIndex = CalculateGridIndex(gridPosition);
        if (IsCellIndexValid(cellIndex))
        {
            grid[cellIndex.x, cellIndex.y].RemoveStructure();
        }
    }

    private bool IsCellIndexValid(Vector2Int cellIndex)
    {
        if (cellIndex.x >= 0 && cellIndex.x < grid.GetLength(0) &&
           cellIndex.y >= 0 && cellIndex.y < grid.GetLength(1))
            return true;
        return false;
    }
}
