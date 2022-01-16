using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private Transform ground;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBuilding(Vector3 _position, GridStructure grid)
    {
        GameObject structure = Instantiate(buildingPrefab, ground.position+_position, Quaternion.identity);
        grid.PlaceStructureOnGrid(structure, _position);
    }
}
