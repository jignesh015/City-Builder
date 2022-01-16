using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("SCRIPTS")]
    public PlacementManager placementManager;
    public IInputManager inputManager;
    public UIController uiController;
    public CameraController cameraController;

    [Header("GRID STRUCTURE")]
    [SerializeField] private int cellSize = 3;
    [SerializeField] private int gridWidth = 100;
    [SerializeField] private int gridLength = 100;
    private GridStructure grid;

    [Header("STATES")]
    private PlayerState playerState;
    public PlayerSelectionState selectionState;
    public PlayerBuildingSingleStructureState buildingSingleStructureState;

    private void Awake()
    {
        grid = new GridStructure(cellSize, gridWidth, gridLength);
        selectionState = new PlayerSelectionState(this, cameraController);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, placementManager, grid);

        playerState = selectionState;
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraController.SetCameraBounds(0, gridWidth, 0, gridLength);

        inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();
        inputManager.AddListenerOnPointerChangeEvent(HandlePointerChange);
        inputManager.AddListenerOnPointerDownEvent(HandlePointerDown);
        inputManager.AddListenerOnPointerSecondChangeEvent(HandleCameraPanMovement);
        inputManager.AddListenerOnPointerSecondUpEvent(StopCameraPanMovement);

        uiController.AddListenerOnBuildAreaEvent(EnableBuildMode);
        uiController.AddListenerOnCancelActionEvent(CancelAction);

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

    // Update is called once per frame
    void Update()
    {
        
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
