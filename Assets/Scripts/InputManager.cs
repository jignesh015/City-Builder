using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IInputManager
{
    [SerializeField] private LayerMask mouseInputMask;

    private Action<Vector3> OnPointerDownHandler;
    private Action<Vector3> OnPointerChangeHandler;
    private Action OnPointerUpHandler;
    private Action<Vector3> OnPointerSecondChangeHandler;
    private Action OnPointerSecondUpHandler;

    public LayerMask MouseInputMask { get => mouseInputMask; set => mouseInputMask=value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetPointerPosition();
        GetPanningPosition();
    }

    #region PRIVATE METHODS
    private void GetPointerPosition()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3? hitPos = GetMousePosition();
            if (hitPos.HasValue)
            {
                OnPointerChangeHandler?.Invoke(hitPos.Value);
                hitPos = null;
            }
        }
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3? hitPos = GetMousePosition();
            if (hitPos.HasValue)
            { 
                OnPointerDownHandler?.Invoke(hitPos.Value);
                hitPos = null;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnPointerUpHandler?.Invoke();
        }
    }

    private void GetPanningPosition()
    {
        if (Input.GetMouseButton(1))
        {
            var position = Input.mousePosition;
            Debug.Log(position);
            OnPointerSecondChangeHandler?.Invoke(position);
        }
        if (Input.GetMouseButtonUp(1))
        {
            OnPointerSecondUpHandler?.Invoke();
        }
    }

    private Vector3? GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, mouseInputMask))
        {
            return(hit.point - transform.position);
        }
        return null;
    }
    #endregion

    #region PUBLIC METHODS
    public void AddListenerOnPointerDownEvent(Action<Vector3> listener)
    {
        OnPointerDownHandler += listener;
    }

    public void RemoveListenerOnPointerDownEvent(Action<Vector3> listener)
    {
        OnPointerDownHandler -= listener;
    }

    public void AddListenerOnPointerSecondChangeEvent(Action<Vector3> listener)
    {
        OnPointerSecondChangeHandler += listener;
    }

    public void RemoveListenerOnPointerSecondChangeEvent(Action<Vector3> listener)
    {
        OnPointerSecondChangeHandler -= listener;
    }

    public void AddListenerOnPointerSecondUpEvent(Action listener)
    {
        OnPointerSecondUpHandler += listener;
    }

    public void RemoveListenerOnPointerSecondUpEvent(Action listener)
    {
        OnPointerSecondUpHandler -= listener;
    }

    public void AddListenerOnPointerUpEvent(Action listener)
    {
        OnPointerUpHandler += listener;
    }

    public void RemoveListenerOnPointerUpEvent(Action listener)
    {
        OnPointerUpHandler -= listener;
    }

    public void AddListenerOnPointerChangeEvent(Action<Vector3> listener)
    {
        OnPointerChangeHandler += listener;
    }

    public void RemoveListenerOnPointerChangeEvent(Action<Vector3> listener)
    {
        OnPointerChangeHandler -= listener;
    }
    #endregion
}
