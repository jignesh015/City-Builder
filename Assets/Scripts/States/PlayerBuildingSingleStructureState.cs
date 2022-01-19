using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingSingleStructureState : PlayerState
{
    BuildingManager buildingManager;
    string structureName;

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
        Debug.Log("Single");
        buildingManager.CreateBuildingAt(position, structureName, StructureType.SingleStructure);
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

    public override void EnterState(string _structureName)
    {
        structureName = _structureName; 
    }
}
