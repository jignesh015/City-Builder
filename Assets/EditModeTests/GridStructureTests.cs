using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridStructureTests
{
    GridStructure structure;
    [OneTimeSetUp]
    public void Init()
    {
        structure = new GridStructure(3, 100,100);
    }

    #region GRID POSITION TEST
    [Test]
    public void CalculateGridPositionPasses()
    {
        Vector3 position = new Vector3(0, 0, 0);
        Vector3 returnPos = structure.CalculateGridPosition(position);
        Assert.AreEqual(Vector3.zero, returnPos);
    }

    [Test]
    public void CalculateGridPositionFloatPasses()
    {
        Vector3 position = new Vector3(5.9f, 0, 5.9f);
        Vector3 returnPos = structure.CalculateGridPosition(position);
        Assert.AreEqual(new Vector3(3,0,3), returnPos);
    }

    [Test]
    public void CalculateGridPositionFails()
    {
        Vector3 position = new Vector3(5.3f, 0, 1.5f);
        Vector3 returnPos = structure.CalculateGridPosition(position);
        Assert.AreNotEqual(Vector3.zero, returnPos);
    }
    #endregion

    #region IS CELL TAKEN TEST
    [Test]
    public void IsCellTakenMinPasses()
    { 
        Vector3 position = Vector3.zero;
        Vector3 gridPos = structure.CalculateGridPosition(position);
        GameObject testObj = new GameObject("Test Object");
        structure.PlaceStructureOnGrid(testObj, gridPos);
        Assert.IsTrue(structure.IsCellTaken(gridPos));
    }

    [Test]
    public void IsCellTakenMaxPasses()
    {
        Vector3 position = new Vector3(297, 0, 297);
        Vector3 gridPos = structure.CalculateGridPosition(position);
        GameObject testObj = new GameObject("Test Object");
        structure.PlaceStructureOnGrid(testObj, gridPos);
        Assert.IsTrue(structure.IsCellTaken(gridPos));
    }

    [Test]
    public void IsCellTaken404Passes()
    {
        Vector3 position = new Vector3(4, 0, 4);
        Vector3 gridPos = structure.CalculateGridPosition(position);
        GameObject testObj = new GameObject("Test Object");
        structure.PlaceStructureOnGrid(testObj, gridPos);
        Assert.IsTrue(structure.IsCellTaken(gridPos));
    }

    [Test]
    public void IsCellTaken404Fails()
    {
        Vector3 position = new Vector3(4, 0, 4);
        Vector3 gridPos = structure.CalculateGridPosition(position);
        GameObject testObj = null;
        structure.PlaceStructureOnGrid(testObj, gridPos);
        Assert.IsFalse(structure.IsCellTaken(gridPos));
    }

    [Test]
    public void IsCellTakenOutOfBoundsFails()
    {
        Vector3 position = new Vector3(303,0,303);
        Assert.Throws<IndexOutOfRangeException>(() => structure.IsCellTaken(position));
    }
    #endregion
}
