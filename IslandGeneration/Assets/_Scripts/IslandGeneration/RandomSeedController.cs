using UnityEngine;

public class RandomSeedController : MonoBehaviour
{
    [SerializeField] private string gameSeed = "Default";
    public string GameSeed { get => gameSeed; set => gameSeed = value; }
    [SerializeField] private int currentSeed;

    private IslandGeneration islandGeneration;

    private void Start() => islandGeneration = GetComponent<IslandGeneration>();

    public void GenerateSeed()
    {
        //Randomise seed
        int newSeed = Random.Range(0, 9999999);
        currentSeed = newSeed;
        Random.InitState(currentSeed);

        //Reset generation
        islandGeneration.IslandStates = IslandGenerationStates.Water;
    }
}
