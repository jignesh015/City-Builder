using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("BUTTONS")]
    [SerializeField] private Button buildResidentialAreaBtn;
    [SerializeField] private Button cancelActionBtn;
    [SerializeField] private Button openBuildMenuBtn;
    [SerializeField] private Button demolishBtn;


    [Header("PANELS")]
    [SerializeField] private GameObject cancelPanel;
    [SerializeField] private GameObject buildingMenuPanel;

    [Header("ACTIONS")]
    private Action OnBuildAreaHandler;
    private Action OnCancelActionHandler;
    private Action OnDemolishHandler;

    // Start is called before the first frame update
    void Start()
    {
        cancelPanel.SetActive(false);
        buildingMenuPanel.SetActive(false);
        buildResidentialAreaBtn.onClick.AddListener(OnBuildAreaCallback);
        cancelActionBtn.onClick.AddListener(OnCancelActionCallback);
        openBuildMenuBtn.onClick.AddListener(OnOpenBuildMenu);
        demolishBtn.onClick.AddListener(OnDemolishCallback);
    }

    private void OnDemolishCallback()
    {
        buildingMenuPanel.SetActive(false);
        cancelPanel.SetActive(true);
        OnDemolishHandler?.Invoke();
    }

    private void OnOpenBuildMenu()
    {
        buildingMenuPanel.SetActive(true);
    }

    private void OnBuildAreaCallback()
    {
        cancelPanel.SetActive(true);
        buildingMenuPanel.SetActive(false);
        OnBuildAreaHandler?.Invoke();
    }

    private void OnCancelActionCallback()
    {
        cancelPanel.SetActive(false);
        buildingMenuPanel.SetActive(false);
        OnCancelActionHandler?.Invoke();
    }

    #region PUBLIC METHODS
    public void AddListenerOnBuildAreaEvent(Action listener)
    {
        OnBuildAreaHandler += listener;
    }

    public void RemoveListenerOnBuildAreaEvent(Action listener)
    {
        OnBuildAreaHandler -= listener;
    }

    public void AddListenerOnCancelActionEvent(Action listener)
    {
        OnCancelActionHandler += listener;
    }

    public void RemoveListenerOnCancelActionEvent(Action listener)
    {
        OnCancelActionHandler -= listener;
    }

    public void AddListenerOnDemolishEvent(Action listener)
    {
        OnDemolishHandler += listener;
    }

    public void RemoveListenerOnDemolishEvent(Action listener)
    {
        OnDemolishHandler -= listener;
    }
    #endregion
}
