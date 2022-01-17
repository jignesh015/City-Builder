using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    PlacementManager placementManager;

    public BuildingManager(int _cellSize, int _width, int _length, PlacementManager _placementManager)
    {
        grid = new GridStructure(_cellSize, _width, _length);
        placementManager = _placementManager;
    }

    public void CreateBuildingAt(Vector3 _inputPosition)
    {
        Vector3 gridPos = grid.CalculateGridPosition(_inputPosition);
        if (!grid.IsCellTaken(gridPos))
        {
            placementManager.PlaceStructureAt(gridPos, grid);
        }
    }

    public void RemoveBuildingAt(Vector3 _inputPosition)
    {
        Vector3 gridPos = grid.CalculateGridPosition(_inputPosition);
        if (grid.IsCellTaken(gridPos))
        {
            placementManager.RemoveStructureAt(gridPos, grid);
        }
    }
}
