using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingRoadState : PlayerState
{
    BuildingManager buildingManager;
    string structureName;

    public PlayerBuildingRoadState(GameManager _gameManager,
       BuildingManager _buildingManager) : base(_gameManager)
    {
        buildingManager = _buildingManager;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        buildingManager.TogglePlacementModeAt(position, structureName, StructureType.Road);
    }

    public override void OnConfirmAction()
    {
        buildingManager.ConfirmPlacement();
        base.OnConfirmAction();
    }

    public override void OnBuildArea(string structureName)
    {
        buildingManager.CancelPlacement();
        base.OnBuildArea(structureName);
    }

    public override void OnBuildRoad(string structureName)
    {
        buildingManager.CancelPlacement();
        base.OnBuildRoad(structureName);
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        buildingManager.CancelPlacement();
        base.OnBuildSingleStructure(structureName);
    }

    public override void OnCancel()
    {
        buildingManager.CancelPlacement();
        gameManager.TransitionPlayerState(gameManager.selectionState);
    }

    public override void EnterState(string _structureName)
    {
        structureName = _structureName;
    }
}
