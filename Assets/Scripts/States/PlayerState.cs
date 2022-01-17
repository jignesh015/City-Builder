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

    public abstract void OnInputPointerChange(Vector3 position);
    public abstract void OnInputPointerDown(Vector3 position);
    public abstract void OnInputPointerUp();
    public abstract void OnInputPointerSecondChange(Vector3 position);
    public abstract void OnInputPointerSecondUp();
    public abstract void OnCancel();

    public virtual void EnterState()
    { 
    
    }

}
