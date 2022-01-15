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
        structure = new GridStructure(3);
    }

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
}
