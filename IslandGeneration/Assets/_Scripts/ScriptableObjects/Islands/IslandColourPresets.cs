using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "IslandColourPresets", menuName = "ScriptableObjects/Island/IslandColourPresets")]
public class IslandColourPresets : ScriptableObject
{
    [Header("Water Colours")]
    public Material waterMat;
    public Color shallowWaterColour;
    public Color deepWaterColour;

    [Header("Island Colours")]
    public IslandPreset[] islandColours;

    [Header("Tress & Cosmetic Colours")]
    public Color[] newColours;
    public EnvironmentalPreset[] environmentalPresets;
}

[System.Serializable]
public struct IslandPreset
{
    public string materialName;
    public Material mat;
    public Color newColour;
    [HideInInspector] public Color originalColour;
}

[System.Serializable]
public struct EnvironmentalPreset
{
    public string materialName;
    public Material mat;
    [HideInInspector] public Color originalColour;
}