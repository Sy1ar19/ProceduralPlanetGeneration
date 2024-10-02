using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter
{
    NoiseSettings.SimpleNoiseSettings _noiseSettings;
    Noise noise = new Noise();

    public SimpleNoiseFilter(NoiseSettings.SimpleNoiseSettings noiseSettings)
    {
        _noiseSettings = noiseSettings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = _noiseSettings.BaseRoughness;
        float amplitude = 1;

        for (int i = 0; i < _noiseSettings.NumLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + _noiseSettings.Center);
            noiseValue += (v + 1) * .5f * amplitude;
            frequency *= _noiseSettings.Roughness;
            amplitude *= _noiseSettings.Persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue - _noiseSettings.MinValue);
        return noiseValue * _noiseSettings.Strenght;
    }
}
