using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("BUTTONS")]
    [SerializeField] private Button buildResidentialAreaBtn;
    [SerializeField] private Button confirmActionBtn;
    [SerializeField] private Button cancelActionBtn;
    [SerializeField] private Button openBuildMenuBtn;
    [SerializeField] private Button closeBuildMenuBtn;
    [SerializeField] private Button demolishBtn;

    [Header("BUTTONS PREFAB")]
    [SerializeField] private GameObject buildButtonPrefab;

    [Header("PANELS")]
    [SerializeField] private GameObject cancelPanel;
    [SerializeField] private GameObject buildingMenuPanel;
    [SerializeField] private GameObject zonesPanel;
    [SerializeField] private GameObject facilitiesPanel;
    [SerializeField] private GameObject roadsPanel;

    [Header("ACTIONS")]
    private Action<string> OnBuildAreaHandler;
    private Action<string> OnBuildSingleStructureHandler;
    private Action<string> OnBuildRoadHandler;
    private Action OnConfirmActionHandler;
    private Action OnCancelActionHandler;
    private Action OnDemolishHandler;

    [Header("SCRIPTS")]
    public StructureRepository structureRepository;

    // Start is called before the first frame update
    void Start()
    {
        cancelPanel.SetActive(false);
        buildingMenuPanel.SetActive(false);
        //buildResidentialAreaBtn.onClick.AddListener(OnBuildAreaCallback);
        confirmActionBtn.onClick.AddListener(OnConfirmActionCallback);
        cancelActionBtn.onClick.AddListener(OnCancelActionCallback);
        openBuildMenuBtn.onClick.AddListener(OnOpenBuildMenu);
        closeBuildMenuBtn.onClick.AddListener(OnCloseBuildMenu);
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
        PrepareBuildMenu();
    }
    private void OnCloseBuildMenu()
    {
        buildingMenuPanel.SetActive(false);
    }

    private void PrepareBuildMenu()
    {
        CreateButtonsInPanel(zonesPanel.transform, structureRepository.GetZoneNames(), OnBuildAreaCallback);
        CreateButtonsInPanel(facilitiesPanel.transform, structureRepository.GetSingleStructureNames(), OnBuildSingleStructureCallback);
        CreateButtonsInPanel(roadsPanel.transform, new List<string>() { structureRepository.GetRoadStructureName() }, OnBuildRoadCallback);
    }

    private void CreateButtonsInPanel(Transform _panelTransform, List<string> _dataToShow, Action<string> _callback)
    {
        if (_dataToShow.Count > _panelTransform.childCount)
        {
            int _diff = _dataToShow.Count - _panelTransform.childCount;
            for (int i = 0; i < _diff; i++)
            {
                Instantiate(buildButtonPrefab, _panelTransform);
            }
        }

        for (int i = 0; i < _panelTransform.childCount; i++)
        {
            var _button = _panelTransform.GetChild(i).GetComponent<Button>();
            if (_button != null)
            {
                _button.GetComponentInChildren<TextMeshProUGUI>().text = _dataToShow[i];
                _button.onClick.RemoveAllListeners();
                _button.onClick.AddListener(() => _callback(_button.GetComponentInChildren<TextMeshProUGUI>().text));
            }
        }
    }

    private void OnBuildAreaCallback(string _structureName)
    {
        PrepareUIForBuilding();
        OnBuildAreaHandler?.Invoke(_structureName);
    }

    private void OnBuildSingleStructureCallback(string _structureName)
    {
        PrepareUIForBuilding();
        OnBuildSingleStructureHandler?.Invoke(_structureName);
    }

    private void OnBuildRoadCallback(string _structureName)
    {
        PrepareUIForBuilding();
        OnBuildRoadHandler?.Invoke(_structureName);
    }

    private void PrepareUIForBuilding()
    {
        cancelPanel.SetActive(true);
        OnCloseBuildMenu();
    }

    private void OnCancelActionCallback()
    {
        cancelPanel.SetActive(false);
        OnCloseBuildMenu();
        OnCancelActionHandler?.Invoke();
    }

    private void OnConfirmActionCallback()
    {
        cancelPanel.SetActive(false);
        OnConfirmActionHandler?.Invoke();
    }

    #region PUBLIC METHODS
    public void AddListenerOnBuildAreaEvent(Action<string> listener)
    {
        OnBuildAreaHandler += listener;
    }

    public void RemoveListenerOnBuildAreaEvent(Action<string> listener)
    {
        OnBuildAreaHandler -= listener;
    }

    public void AddListenerOnBuildSingleStructureEvent(Action<string> listener)
    {
        OnBuildSingleStructureHandler += listener;
    }

    public void RemoveListenerOnBuildSingleStructureEvent(Action<string> listener)
    {
        OnBuildSingleStructureHandler -= listener;
    }

    public void AddListenerOnBuildRoadEvent(Action<string> listener)
    {
        OnBuildRoadHandler += listener;
    }

    public void RemoveListenerOnBuildRoadEvent(Action<string> listener)
    {
        OnBuildRoadHandler -= listener;
    }

    public void AddListenerOnCancelActionEvent(Action listener)
    {
        OnCancelActionHandler += listener;
    }

    public void RemoveListenerOnCancelActionEvent(Action listener)
    {
        OnCancelActionHandler -= listener;
    }

    public void AddListenerOnConfirmActionEvent(Action listener)
    {
        OnConfirmActionHandler += listener;
    }

    public void RemoveListenerOnConfirmActionEvent(Action listener)
    {
        OnConfirmActionHandler -= listener;
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
