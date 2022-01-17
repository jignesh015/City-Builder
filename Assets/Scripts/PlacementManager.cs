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

    public void PlaceStructureAt(Vector3 _gridPosition, GridStructure _grid)
    {
        GameObject structure = Instantiate(buildingPrefab, ground.position+ _gridPosition, Quaternion.identity);
        _grid.PlaceStructureOnGrid(structure, _gridPosition);
    }

    public void RemoveStructureAt(Vector3 _gridPosition, GridStructure _grid)
    {
        GameObject _structure = _grid.GetStructureOnGrid(_gridPosition);
        if (_structure != null)
        {
            Destroy(_structure);
            _grid.RemoveStructureFromGrid(_gridPosition);
        }
    }
}
