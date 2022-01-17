using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingSingleStructureState : PlayerState
{
    BuildingManager buildingManager;

    public PlayerBuildingSingleStructureState(GameManager _gameManager,
       BuildingManager _buildingManager) : base(_gameManager)
    {
        buildingManager = _buildingManager;
    }
    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        buildingManager.CreateBuildingAt(position);
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

    public override void OnCancel()
    {
        gameManager.TransitionPlayerState(gameManager.selectionState);
    }
}
