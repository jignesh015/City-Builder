using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingZoneState : PlayerState
{
    BuildingManager buildingManager;
    string structureName;

    public PlayerBuildingZoneState(GameManager _gameManager,
       BuildingManager _buildingManager) : base(_gameManager)
    {
        buildingManager = _buildingManager;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        Debug.Log("Area");
        buildingManager.CreateBuildingAt(position, structureName, StructureType.Zone);
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
