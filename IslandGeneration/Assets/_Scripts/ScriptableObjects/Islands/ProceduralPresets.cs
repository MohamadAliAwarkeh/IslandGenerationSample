using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ProceduralPresets", menuName = "ScriptableObjects/Island/ProceduralPresets")]
public class ProceduralPresets : ScriptableObject
{
    [Header("Object Variables")]
    public List<GenerationObjects> generationObjects;

    [Header("Spawning Variables")]
    public bool requiresMultipleObjects;
    [Tooltip("Only fill this out if you want to spawn more than 1 obj")]
    public int minNumOfObjects;
    public int maxNumOfObjects;

    [Header("Position Variables")]
    public float xSpawnPos;
    public float zSpawnPos;

    [Header("Rotation Variables")]
    public float xRotation;
    public float yRotation;
    public float zRotation;

    [Header("Scaling Variables")]
    public float globalScaleMultiplier;
    public bool scaleUniformly;
    public float uniformMinScale;
    public float uniformMaxScale;
    public float xMinScale;
    public float xMaxScale;
    public float yMinScale; 
    public float yMaxScale;
    public float zMinScale; 
    public float zMaxScale;
}

[System.Serializable]
public struct GenerationObjects
{
    public string objectName;
    public GameObject objectPrefab;
    public float objectRarity;
}
