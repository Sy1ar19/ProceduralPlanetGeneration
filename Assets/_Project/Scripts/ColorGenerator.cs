using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    private ColorSettings _settings;

    private Texture2D _texture;
    const int TextureResolution = 50;
    INoiseFilter biomeNoiseFilter;

    public void UpdateSettings(ColorSettings settings)
    {
        _settings = settings;

        if (_texture == null || _texture.height != settings.biomeColorSettings.biomes.Length)
        {
            _texture = new Texture2D(TextureResolution, settings.biomeColorSettings.biomes.Length);
        }
        biomeNoiseFilter = NoiseFilterFactory.CreateNoiseFilter(_settings.biomeColorSettings.noise);
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        _settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public float BiomePercentFromPoint(Vector3 pointOfUnitSphere)
    {
        float highPercent = (pointOfUnitSphere.y + 1) / 2f;
        highPercent += (biomeNoiseFilter.Evaluate(pointOfUnitSphere) - _settings.biomeColorSettings.noiseOffset)
            * _settings.biomeColorSettings.noiseStrenght;
        float biomeIndex = 0;
        int numBiomes = _settings.biomeColorSettings.biomes.Length;
        float blendRange = _settings.biomeColorSettings.blendAnount / 2f + .001f;

        for (int i = 0; i < numBiomes; i++)
        {
            float distance = highPercent - _settings.biomeColorSettings.biomes[i].startHeight;
            float weight = Mathf.InverseLerp(-blendRange, blendRange, distance);
            biomeIndex *= (1 - weight);
            biomeIndex += i * weight;
        }

        return biomeIndex / Mathf.Max(1, numBiomes - 1);
    }

    public void UpdateColors()
    {
        Color[] colors = new Color[_texture.width * _texture.height];
        int colorIndex = 0;

        foreach (var biome in _settings.biomeColorSettings.biomes)
        {
            for (int i = 0; i < TextureResolution; i++)
            {
                Color gradientColor = biome.gradient.Evaluate(i / (TextureResolution - 1f));
                Color tintColor = biome.tint;
                colors[colorIndex] = gradientColor * (1 - biome.tintPercent) + tintColor * biome.tintPercent;
                colorIndex++;
            }
        }



        _texture.SetPixels(colors);
        _texture.Apply();
        _settings.planetMaterial.SetTexture("_texture", _texture);
    }
}
