using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private GameObject structureModel = null;
    private bool isTaken = false;

    public bool IsTaken { get => isTaken; }

    public void BuildStructure(GameObject _model)
    {
        if (_model == null) return;
        structureModel = _model;
        isTaken = true;
    }

    public GameObject GetStructure()
    {
        return structureModel;
    }

    public void RemoveStructure()
    {
        structureModel = null;
        isTaken = false;
    }
}
