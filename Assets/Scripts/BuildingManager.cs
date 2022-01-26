using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    PlacementManager placementManager;
    StructureRepository structureRepository;
    Dictionary<Vector3Int, GameObject> structuresToBeModified = new Dictionary<Vector3Int, GameObject>();

    public BuildingManager(int _cellSize, int _width, int _length, PlacementManager _placementManager, StructureRepository _structureRepository)
    {
        grid = new GridStructure(_cellSize, _width, _length);
        placementManager = _placementManager;
        structureRepository = _structureRepository;
    }

    public void TogglePlacementModeAt(Vector3 _inputPosition, string _structureName, StructureType _structureType)
    {
        GameObject _buildingPrefab = structureRepository.GetBuildingPrefabByName(_structureName, _structureType);
        Vector3 gridPos = grid.CalculateGridPosition(_inputPosition);
        Vector3Int gridPosInt = Vector3Int.FloorToInt(gridPos);
        if (!grid.IsCellTaken(gridPos))
        {
            if (structuresToBeModified.ContainsKey(gridPosInt))
            {
                //Remove the ghost structure
                var _structure = structuresToBeModified[gridPosInt];
                placementManager.DestroySingleStructure(_structure);
                structuresToBeModified.Remove(gridPosInt);
            }
            else
            { 
                //Create the ghost structure
                structuresToBeModified.Add(gridPosInt, placementManager.CreateGhostStructure(gridPos, _buildingPrefab));
            }
        }
    }

    public void ConfirmPlacement()
    {
        placementManager.PlaceStructureOnTheMap(structuresToBeModified.Values);
        foreach (var _keyValuePair in structuresToBeModified)
        {
            grid.PlaceStructureOnGrid(_keyValuePair.Value, _keyValuePair.Key);
        }
        structuresToBeModified.Clear();
    }

    public void CancelPlacement()
    {
        placementManager.DestroyStructure(structuresToBeModified.Values);
        structuresToBeModified.Clear();
    }

    public void ToggleDemolitionModeAt(Vector3 _inputPosition)
    {
        Vector3 gridPos = grid.CalculateGridPosition(_inputPosition);
        Vector3Int gridPosInt = Vector3Int.FloorToInt(gridPos);
        if (grid.IsCellTaken(gridPos))
        {
            var _structure = grid.GetStructureOnGrid(gridPos);
            if (_structure != null)
            {
                if (structuresToBeModified.ContainsKey(gridPosInt))
                {
                    //Remove from demolition queue
                    placementManager.ResetStructureMaterials(_structure);
                    structuresToBeModified.Remove(gridPosInt);
                }
                else
                {
                    //Add to demolition queue
                    placementManager.SetStructureForDemolition(_structure);
                    structuresToBeModified.Add(gridPosInt, _structure);
                }
            }
        }
    }

    public void ConfirmDemolition()
    {
        foreach (var _gridPos in structuresToBeModified.Keys)
        {
            grid.RemoveStructureFromGrid(_gridPos);
        }
        placementManager.DestroyStructure(structuresToBeModified.Values);
        structuresToBeModified.Clear();
    }

    public void CancelDemolition()
    {
        placementManager.PlaceStructureOnTheMap(structuresToBeModified.Values);
        structuresToBeModified.Clear();
    }
}
