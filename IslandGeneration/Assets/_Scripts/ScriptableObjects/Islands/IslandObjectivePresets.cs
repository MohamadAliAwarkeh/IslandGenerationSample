using UnityEngine;

[CreateAssetMenu(fileName = "IslandObjectivePreset", menuName = "ScriptableObjects/Island/IslandObjectivePreset")]
public class IslandObjectivePresets : ScriptableObject
{
    [Header("Scene Variables")]
    [SerializeField] private int sceneToLoad;
    public int SceneToLoad { get => sceneToLoad; set => sceneToLoad = value; }

    [Header("Island Information")]
    public string islandName;
    public string islandType;
    public string islandObjective;
}
