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

    [Header("GRID STRUCTURE")]
    [SerializeField] private int cellSize = 3;
    [SerializeField] private int gridWidth = 100;
    [SerializeField] private int gridLength = 100;
    private GridStructure grid;

    [Header("BOOLS")]
    private bool isBuildModeActive;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();
        inputManager.AddListenerOnPointerDownEvent(HandleInput);
        grid = new GridStructure(cellSize, gridWidth, gridLength);

        uiController.AddListenerOnBuildAreaEvent(EnableBuildMode);
        uiController.AddListenerOnCancelActionEvent(CancelAction);
    }

    private void HandleInput(Vector3 _position)
    {
        Vector3 gridPos = grid.CalculateGridPosition(_position);
        if (isBuildModeActive && !grid.IsCellTaken(gridPos))
        { 
            placementManager.CreateBuilding(gridPos, grid);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnableBuildMode()
    {
        isBuildModeActive = true;
    }

    private void CancelAction()
    {
        isBuildModeActive = false;
    }
}
