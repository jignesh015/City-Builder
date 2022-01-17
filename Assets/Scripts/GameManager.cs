using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("SCRIPTS")]
    public BuildingManager buildingManager;
    public PlacementManager placementManager;
    public IInputManager inputManager;
    public UIController uiController;
    public CameraController cameraController;

    [Header("GRID STRUCTURE")]
    [SerializeField] private int cellSize = 3;
    [SerializeField] private int gridWidth = 100;
    [SerializeField] private int gridLength = 100;
    [SerializeField] private LayerMask inputMask;

    [Header("STATES")]
    private PlayerState playerState;
    public PlayerSelectionState selectionState;
    public PlayerBuildingSingleStructureState buildingSingleStructureState;
    public PlayerRemoveBuildingState removeBuildingState;

    private void Awake()
    {
        buildingManager = new BuildingManager(cellSize, gridWidth, gridLength, placementManager);
        selectionState = new PlayerSelectionState(this, cameraController);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, buildingManager);
        removeBuildingState = new PlayerRemoveBuildingState(this, buildingManager);

        playerState = selectionState;

#if (UNITY_EDITOR && TEST) || !(UNITY_IOS || UNITY_ANDROID)
        inputManager = gameObject.AddComponent<InputManager>();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraController.SetCameraBounds(0, gridWidth, 0, gridLength);
        AssignInputListeners();
        AssignUIControllerListeners();
    }

    private void AssignUIControllerListeners()
    {
        uiController.AddListenerOnBuildAreaEvent(EnableBuildMode);
        uiController.AddListenerOnCancelActionEvent(CancelAction);
        uiController.AddListenerOnDemolishEvent(EnableDemolishMode);
    }

    private void AssignInputListeners()
    {
        inputManager.MouseInputMask = inputMask;
        inputManager.AddListenerOnPointerChangeEvent(HandlePointerChange);
        inputManager.AddListenerOnPointerDownEvent(HandlePointerDown);
        inputManager.AddListenerOnPointerSecondChangeEvent(HandleCameraPanMovement);
        inputManager.AddListenerOnPointerSecondUpEvent(StopCameraPanMovement);
    }

    private void EnableDemolishMode()
    {
        TransitionPlayerState(removeBuildingState);
    }

    private void StopCameraPanMovement()
    {
        playerState.OnInputPointerSecondUp();
    }

    private void HandleCameraPanMovement(Vector3 _pointerPos)
    {
        playerState.OnInputPointerSecondChange(_pointerPos);
    }

    private void HandlePointerChange(Vector3 _pointerPos)
    {
        playerState.OnInputPointerChange(_pointerPos);
    }

    private void HandlePointerDown(Vector3 _position)
    {
        playerState.OnInputPointerDown(_position);
    }

    private void EnableBuildMode()
    {
        TransitionPlayerState(buildingSingleStructureState);
    }

    private void CancelAction()
    {
        playerState.OnCancel();
    }

    public void TransitionPlayerState(PlayerState _state)
    {
        playerState = _state;
        playerState.EnterState();
    }
}
