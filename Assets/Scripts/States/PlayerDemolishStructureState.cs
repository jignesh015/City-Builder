using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemolishStructureState : PlayerState
{
    BuildingManager buildingManager;

    public PlayerDemolishStructureState(GameManager _gameManager, BuildingManager _buildingManager) : base(_gameManager)
    {
        buildingManager = _buildingManager;
    }

    public override void OnConfirmAction()
    {
        buildingManager.ConfirmDemolition();
        base.OnConfirmAction();
    }

    public override void OnBuildArea(string structureName)
    {
        buildingManager.CancelDemolition();
        base.OnBuildArea(structureName);
    }

    public override void OnBuildRoad(string structureName)
    {
        buildingManager.CancelDemolition();
        base.OnBuildRoad(structureName);
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        buildingManager.CancelDemolition();
        base.OnBuildSingleStructure(structureName);
    }

    public override void OnCancel()
    {
        buildingManager.CancelDemolition();
        gameManager.TransitionPlayerState(gameManager.selectionState);
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        buildingManager.ToggleDemolitionModeAt(position);
    }

    public override void OnInputPointerUp()
    {
        return;
    }
}
