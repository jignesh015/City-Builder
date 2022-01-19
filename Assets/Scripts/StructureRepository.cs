using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureRepository : MonoBehaviour
{
    public CollectionSO modelDataCollection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<string> GetZoneNames()
    {
        return modelDataCollection.zoneList.Select(zone => zone.buildingName).ToList();
    }

    public List<string> GetSingleStructureNames()
    {
        return modelDataCollection.singleStructureList.Select(facility => facility.buildingName).ToList();
    }  

    public string GetRoadStructureName()
    {
        return modelDataCollection.roadStructure.buildingName;
    }

    public GameObject GetBuildingPrefabByName(string _structureName, StructureType _structureType)
    {
        GameObject _prefabToReturn = null;
        switch (_structureType)
        {
            case StructureType.Zone:
                _prefabToReturn = GetZonePrefabByName(_structureName);
                break;
            case StructureType.SingleStructure:
                _prefabToReturn = GetSingleStructurePrefabByName(_structureName);
                break;
            case StructureType.Road:
                _prefabToReturn = GetRoadPrefab();
                break;
            default:
                throw new Exception("Structure type " + _structureType.ToString() + " not implemented");
        }
        if (_prefabToReturn == null)
        {
            throw new Exception("Prefab with name " + _structureType.ToString() + " does not exist");
        }
        return _prefabToReturn;
    }

    private GameObject GetRoadPrefab()
    {
        return modelDataCollection.roadStructure.prefab;
    }

    private GameObject GetSingleStructurePrefabByName(string structureName)
    {
        return modelDataCollection.singleStructureList.Find(s => s.buildingName == structureName).prefab;
    }

    private GameObject GetZonePrefabByName(string structureName)
    {
        return modelDataCollection.zoneList.Find(z => z.buildingName == structureName).prefab;
    }
}

public enum StructureType
{
    Zone,
    SingleStructure,
    Road
}