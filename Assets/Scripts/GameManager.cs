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
    public PlayerBuildingZoneState buildingAreaState;
    public PlayerBuildingSingleStructureState buildingSingleStructureState;
    public PlayerBuildingRoadState buildingRoadState;
    public PlayerRemoveBuildingState removeBuildingState;

    private void Awake()
    {
        PrepareStates();

#if (UNITY_EDITOR && TEST) || !(UNITY_IOS || UNITY_ANDROID)
        inputManager = gameObject.AddComponent<InputManager>();
#endif
    }

    private void PrepareStates()
    {
        buildingManager = new BuildingManager(cellSize, gridWidth, gridLength, placementManager, uiController.structureRepository);
        selectionState = new PlayerSelectionState(this, cameraController);
        buildingAreaState = new PlayerBuildingZoneState(this, buildingManager);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, buildingManager);
        buildingRoadState = new PlayerBuildingRoadState(this, buildingManager);
        removeBuildingState = new PlayerRemoveBuildingState(this, buildingManager);

        playerState = selectionState;
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
        uiController.AddListenerOnBuildAreaEvent((_structureName) => playerState.OnBuildArea(_structureName));
        uiController.AddListenerOnBuildSingleStructureEvent((_structureName) => playerState.OnBuildSingleStructure(_structureName));
        uiController.AddListenerOnBuildRoadEvent((_structureName) => playerState.OnBuildRoad(_structureName));
        uiController.AddListenerOnCancelActionEvent(() => playerState.OnCancel());
        uiController.AddListenerOnDemolishEvent(() => playerState.OnDemolishStructure());
    }

    private void AssignInputListeners()
    {
        inputManager.MouseInputMask = inputMask;
        inputManager.AddListenerOnPointerChangeEvent((_position) => playerState.OnInputPointerChange(_position));
        inputManager.AddListenerOnPointerDownEvent((_position) => playerState.OnInputPointerDown(_position));
        inputManager.AddListenerOnPointerSecondChangeEvent((_position) => playerState.OnInputPointerSecondChange(_position));
        inputManager.AddListenerOnPointerSecondUpEvent(() => playerState.OnInputPointerSecondUp());
    }

    public void TransitionPlayerState(PlayerState _state, string _structureName = null)
    {
        playerState = _state;
        playerState.EnterState(_structureName);
    }
}
