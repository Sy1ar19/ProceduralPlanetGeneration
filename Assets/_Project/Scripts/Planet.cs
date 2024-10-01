using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField, HideInInspector] private MeshFilter[] _meshFilters;
    [SerializeField] private TerrainFace[] _terrainFaces;
    [Range(2, 256)][SerializeField] int _resolution = 10;

    [SerializeField] private ShapeSettings _shapeSettings;
    [SerializeField] private ColorSettings _colorSettings;
    [SerializeField] private bool _autoUpdate = true;

    public bool ShapeSettingsFoldout;
    public bool ColorSettingsFoldout;

    private ShapeGenerator _shapeGenerator;

    public ShapeSettings ShapeSettings => _shapeSettings;
    public ColorSettings ColorSettings => _colorSettings;


    /*    private void OnValidate()
        {
            GeneratePlanet();
            UpdateMeshResolution();
        }*/

    /*    private void Start()
        {
            Initialize();
            GenerateMesh();
        }*/

    public void Initialize()
    {
        _shapeGenerator = new ShapeGenerator(_shapeSettings);

        if (_meshFilters == null || _meshFilters.Length == 0)
        {
            _meshFilters = new MeshFilter[6];
        }

        _terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (_meshFilters[i] == null)
            {
                GameObject meshObject = new GameObject("mesh");
                meshObject.transform.parent = transform;

                meshObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                _meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                _meshFilters[i].sharedMesh = new Mesh();
            }

            _terrainFaces[i] = new TerrainFace(_shapeGenerator, _meshFilters[i].sharedMesh, _resolution, directions[i]);
        }
    }

    public void UpdateMeshResolution()
    {
        if (_terrainFaces == null)
        {
            Debug.LogError("TerrainFaces array is not initialized!");
            return;
        }

        for (int i = 0; i < 6; i++)
        {
            if (_terrainFaces[i] != null)
            {
                _terrainFaces[i].UpdateResolution(_resolution);
            }
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColor();
    }

    public void OnShapeSettingsUpdated()
    {
        if (_autoUpdate == true)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColorSettingsUpdated()
    {
        if (_autoUpdate == true)
        {
            Initialize();
            GenerateColor();
        }
    }


    public void GenerateMesh()
    {
        foreach (TerrainFace face in _terrainFaces)
        {
            face.ConstructMesh();
        }
    }

    public void GenerateColor()
    {
        foreach (MeshFilter filter in _meshFilters)
        {
            filter.GetComponent<MeshRenderer>().sharedMaterial.color = _colorSettings.planetColor;
        }
    }
}
