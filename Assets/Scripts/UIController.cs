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

    [Header("PANELS")]
    [SerializeField] private GameObject cancelPanel;

    [Header("ACTIONS")]
    private Action OnBuildAreaHandler;
    private Action OnCancelActionHandler;

    // Start is called before the first frame update
    void Start()
    {
        cancelPanel.SetActive(false);
        buildResidentialAreaBtn.onClick.AddListener(OnBuildAreaCallback);
        cancelActionBtn.onClick.AddListener(OnCancelActionCallback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region PUBLIC METHODS
    public void OnBuildAreaCallback()
    {
        cancelPanel.SetActive(true);
        OnBuildAreaHandler?.Invoke();
    }

    public void OnCancelActionCallback()
    {
        cancelPanel.SetActive(false);
        OnCancelActionHandler?.Invoke();
    }

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
    #endregion
}
