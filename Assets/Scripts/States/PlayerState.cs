using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected GameManager gameManager;

    public PlayerState(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }

    public virtual void OnInputPointerChange(Vector3 position)
    { 

    }
    public virtual void OnInputPointerDown(Vector3 position)
    {

    }
    public virtual void OnInputPointerUp()
    {

    }
    public virtual void OnInputPointerSecondChange(Vector3 position)
    {

    }
    public virtual void OnInputPointerSecondUp()
    {

    }
    public abstract void OnCancel();

    public virtual void EnterState(string variable)
    { 
    
    }

    public virtual void OnBuildArea(string structureName)
    {
        gameManager.TransitionPlayerState(gameManager.buildingAreaState, structureName);
    }

    public virtual void OnBuildSingleStructure(string structureName)
    {
        gameManager.TransitionPlayerState(gameManager.buildingSingleStructureState, structureName);
    }

    public virtual void OnBuildRoad(string structureName)
    {
        gameManager.TransitionPlayerState(gameManager.buildingRoadState, structureName);
    }

    public virtual void OnDemolishStructure()
    {
        gameManager.TransitionPlayerState(gameManager.removeBuildingState);
    }

}
