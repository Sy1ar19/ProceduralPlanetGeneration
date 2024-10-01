using UnityEngine;

public class ShapeGenerator 
{
    private ShapeSettings _settings;

    public ShapeGenerator(ShapeSettings settings)
    {
        _settings = settings;
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        return pointOnUnitSphere * _settings.planetRadius;
    }
}
