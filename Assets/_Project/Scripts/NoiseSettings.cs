using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    [SerializeField] private float _strenght = 1;
    [Range(1,8)][SerializeField] private int _numLayers = 1;
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
