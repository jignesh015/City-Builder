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
        Debug.Log("Road");
        buildingManager.CreateBuildingAt(position, structureName, StructureType.Road);
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
