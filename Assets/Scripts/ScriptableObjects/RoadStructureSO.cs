using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Road Structure", menuName = "City Builder/Structure Data/Road Structure")]
public class RoadStructureSO : StructureBaseSO
{
    [Tooltip("For connecting two roads")]
    public GameObject cornerPrefab;
    [Tooltip("For connecting three roads")]
    public GameObject threeWayPrefab;
    [Tooltip("For connecting four roads")]
    public GameObject fourWayPrefab;
}
