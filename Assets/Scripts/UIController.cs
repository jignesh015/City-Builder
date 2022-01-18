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
    private Action OnBuildAreaHandler;
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
        CreateButtonsInPanel(zonesPanel.transform, structureRepository.GetZoneNames());
        CreateButtonsInPanel(facilitiesPanel.transform, structureRepository.GetSingleStructureNames());
        CreateButtonsInPanel(roadsPanel.transform, new List<string>() { structureRepository.GetRoadStructureName() });
    }

    private void CreateButtonsInPanel(Transform _panelTransform, List<string> _dataToShow)
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
                _button.onClick.AddListener(OnBuildAreaCallback);
            }
        }
    }

    private void OnBuildAreaCallback()
    {
        cancelPanel.SetActive(true);
        OnCloseBuildMenu();
        OnBuildAreaHandler?.Invoke();
    }

    private void OnCancelActionCallback()
    {
        cancelPanel.SetActive(false);
        OnCloseBuildMenu();
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
