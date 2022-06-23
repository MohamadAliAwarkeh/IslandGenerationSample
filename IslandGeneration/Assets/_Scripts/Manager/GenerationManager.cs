using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private GenerationStates generationStates;
    public GenerationStates GenerationStates { get => generationStates; }

    [SerializeField] private IslandThemes islandThemes;
    public IslandThemes IslandThemes { get => islandThemes; }

    //Components
    private ColourThemeSelector colourThemeSelector;
    private RandomSeedController seedController;
    private IslandGeneration islandGeneration;

    private void Start() => AllComponents();

    private void AllComponents()
    {
        colourThemeSelector = GetComponent<ColourThemeSelector>();
        seedController = GetComponent<RandomSeedController>();
        islandGeneration = GetComponent<IslandGeneration>();
    }

    private void Update()
    {
        //Dev tool
        GenerateNewIsland();

        //Generate Island
        SwitchStates();
    }

    #region States & State Functions
    private void SwitchStates()
    {
        switch(generationStates)
        {
            case GenerationStates.ThemeSelection:
                RandomiseTheme();
                break;

            case GenerationStates.AssignNewColours:
                AssignColoursBasedOnTheme();
                break;

            case GenerationStates.GenerateSeed:
                GenerateNewSeed();
                break;

            case GenerationStates.IslandGeneration:
                GenerateIsland();
                break;

            case GenerationStates.Completed:
                //Just do nothing
                break;
        }
    }

    private void RandomiseTheme()
    {
        //Randomise
        int randomIndex = Random.Range(0, (int)IslandThemes.NumOfTypes);
        //Apply random theme
        islandThemes = (IslandThemes)randomIndex;

        //Change state
        generationStates = GenerationStates.AssignNewColours;
    }

    private void AssignColoursBasedOnTheme()
    {
        //Apply colours based on selected theme
        colourThemeSelector.RandomlySelectColourTheme((int)islandThemes);

        //Change state
        generationStates = GenerationStates.GenerateSeed;
    }

    private void GenerateNewSeed()
    {
        //Generate new seed
        seedController.GenerateSeed();

        //Change state
        generationStates = GenerationStates.IslandGeneration;
    }

    private void GenerateIsland()
    {
        //Island generation will handle its own states
        //and once that has completed, it will set a bool to true and manager will change state
    }
    #endregion

    #region Developer Testing
    private void GenerateNewIsland()
    {
        //Just for dev testing sake
        if (!Input.GetKeyDown(KeyCode.F5))
            return;

        //Destroy all children within the parents
        for (int i = 0; i < islandGeneration.ParentTransforms.Length; i++)
        {
            foreach (Transform child in islandGeneration.ParentTransforms[i])
                Destroy(child.gameObject);
        }
        //Reset state
        generationStates = GenerationStates.ThemeSelection;
    }
    #endregion
}

public enum GenerationStates
{
    ThemeSelection,
    AssignNewColours,
    GenerateSeed,
    IslandGeneration,
    Completed
}

public enum IslandThemes
{ 
    Grasslands = 0,
    Forest = 1,
    Desert = 2,
    Tundra = 3,
    Mystical = 4,
    Swamp = 5,
    Lava = 6,
    NumOfTypes
}
