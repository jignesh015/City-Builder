using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionState : PlayerState
{
    CameraController cameraController;
    public PlayerSelectionState(GameManager _gameManager, CameraController _camController) : base(_gameManager)
    {
        cameraController = _camController;
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerSecondChange(Vector3 position)
    {
        cameraController.MoveCamera(position);
    }

    public override void OnInputPointerSecondUp()
    {
        cameraController.StopCameraMovememt();
    }

    public override void OnInputPointerUp()
    {
        return;
    }

    public override void OnCancel()
    {
        return;
    }
}
