using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgidNoiseFilter : INoiseFilter
{
    NoiseSettings.RidgidNoiseSettings _noiseSettings;
    Noise noise = new Noise();

    public RidgidNoiseFilter(NoiseSettings.RidgidNoiseSettings noiseSettings)
    {
        _noiseSettings = noiseSettings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = _noiseSettings.BaseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < _noiseSettings.NumLayers; i++)
        {
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + _noiseSettings.Center));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp01(v * _noiseSettings.WeightMultiplier);
            noiseValue += v * amplitude;
            frequency *= _noiseSettings.Roughness;
            amplitude *= _noiseSettings.Persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue - _noiseSettings.MinValue);
        return noiseValue * _noiseSettings.Strenght;
    }
}
