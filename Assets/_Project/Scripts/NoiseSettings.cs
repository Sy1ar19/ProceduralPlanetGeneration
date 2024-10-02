using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public enum FilterType { Simple, Ridgid};
    public FilterType filterType;

    [ConditionalHide("filterType", 0)]
    public SimpleNoiseSettings simpleNoiseSettings;
    [ConditionalHide("filterType", 1)]
    public RidgidNoiseSettings ridgidNoiseSettings;

    [System.Serializable]
    public class SimpleNoiseSettings
    {
        [SerializeField] private float _strenght = 1;
        [Range(1, 8)][SerializeField] private int _numLayers = 1;
        [SerializeField] private float _baseRoughness = 1;
        [SerializeField] private float _roughness = 2;
        [SerializeField] private float _persistence = .5f;
        [SerializeField] private Vector3 _center;
        [SerializeField] private float _minValue;

        public float Strenght => _strenght;
        public float Roughness => _roughness;
        public Vector3 Center => _center;
        public float Persistence => _persistence;

        public float BaseRoughness => _baseRoughness;

        public int NumLayers => _numLayers;

        public float MinValue => _minValue;
    }

    [System.Serializable]
    public class RidgidNoiseSettings : SimpleNoiseSettings
    {
        [SerializeField] private float _weightMultiplier = .8f;

        public float WeightMultiplier => _weightMultiplier;
    }
}
