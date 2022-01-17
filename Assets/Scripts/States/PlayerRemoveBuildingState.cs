using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRemoveBuildingState : PlayerState
{
    BuildingManager buildingManager;

    public PlayerRemoveBuildingState(GameManager _gameManager, BuildingManager _buildingManager) : base(_gameManager)
    {
        buildingManager = _buildingManager;
    }

    public override void OnCancel()
    {
        gameManager.TransitionPlayerState(gameManager.selectionState);
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        buildingManager.RemoveBuildingAt(position);
    }

    public override void OnInputPointerSecondChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerSecondUp()
    {
        return;
    }

    public override void OnInputPointerUp()
    {
        return;
    }
}
