using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingSingleStructureState : PlayerState
{
    PlacementManager placementManager;
    GridStructure grid;

    public PlayerBuildingSingleStructureState(GameManager _gameManager,
        PlacementManager _placementManager, GridStructure _grid) : base(_gameManager)
    {
        placementManager = _placementManager;
        grid = _grid;
    }
    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        Vector3 gridPos = grid.CalculateGridPosition(position);
        if (!grid.IsCellTaken(gridPos))
        {
            placementManager.CreateBuilding(gridPos, grid);
        }
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
