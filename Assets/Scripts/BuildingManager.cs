using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    PlacementManager placementManager;
    StructureRepository structureRepository;

    public BuildingManager(int _cellSize, int _width, int _length, PlacementManager _placementManager, StructureRepository _structureRepository)
    {
        grid = new GridStructure(_cellSize, _width, _length);
        placementManager = _placementManager;
        structureRepository = _structureRepository;
    }

    public void CreateBuildingAt(Vector3 _inputPosition, string _structureName, StructureType _structureType)
    {
        GameObject _buildingPrefab = structureRepository.GetBuildingPrefabByName(_structureName, _structureType);
        Vector3 gridPos = grid.CalculateGridPosition(_inputPosition);
        if (!grid.IsCellTaken(gridPos))
        {
            placementManager.PlaceStructureAt(gridPos, grid, _buildingPrefab);
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
