using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private Transform ground;
    [SerializeField] private Material transparentMaterial;
    private Dictionary<GameObject, Material[]> originalMaterials = new Dictionary<GameObject, Material[]>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    public GameObject CreateGhostStructure(Vector3 _gridPosition, GameObject _buildingPrefab)
    {
        GameObject newStructure = Instantiate(_buildingPrefab, ground.position + _gridPosition, Quaternion.identity);
        Color colorToSet = Color.green;
        ModifyStructureMaterial(newStructure, colorToSet);
        return newStructure;
    }

    private void ModifyStructureMaterial(GameObject newStructure, Color colorToSet)
    {
        for (int i = 0; i < newStructure.transform.childCount; i++)
        {
            Transform _child = newStructure.transform.GetChild(i);
            var _renderer = _child.GetComponent<MeshRenderer>();
            if (_renderer == null) continue;
            if (!originalMaterials.ContainsKey(_child.gameObject))
            {
                originalMaterials.Add(_child.gameObject, _renderer.materials);
            }
            Material[] _materialsToSet = new Material[_renderer.materials.Length];
            for (int j = 0; j < _materialsToSet.Length; j++)
            {
                _materialsToSet[j] = transparentMaterial;
                _materialsToSet[j].color = colorToSet;
            }
            _renderer.materials = _materialsToSet;
        }
    }

    public void PlaceStructureOnTheMap(IEnumerable<GameObject> _structureCollection)
    {
        foreach (GameObject _structure in _structureCollection)
        {
            ResetStructureMaterials(_structure);
        }
        originalMaterials.Clear();
    }

    public void ResetStructureMaterials(GameObject _structure)
    {
        foreach (Transform _child in _structure.transform)
        {
            var _renderer = _child.GetComponent<MeshRenderer>();
            if (originalMaterials.ContainsKey(_child.gameObject))
            {
                _renderer.materials = originalMaterials[_child.gameObject];
            }
        }
    }

    public void DestroyStructure(IEnumerable<GameObject> _structureCollection)
    {
        foreach (GameObject _structure in _structureCollection)
        {
            DestroySingleStructure(_structure);
        }
        originalMaterials.Clear();
    }

    public void DestroySingleStructure(GameObject _structure)
    {
        Destroy(_structure);
    }

    public void SetStructureForDemolition(GameObject _structureToDemolish)
    {
        Color colorToSet = Color.red;
        ModifyStructureMaterial(_structureToDemolish, colorToSet);
    }
}
