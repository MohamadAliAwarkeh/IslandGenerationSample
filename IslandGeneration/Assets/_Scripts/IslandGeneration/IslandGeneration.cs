using UnityEngine;
using System.Collections.Generic;

public class IslandGeneration : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private IslandGenerationStates islandStates;
    public IslandGenerationStates IslandStates { get => islandStates; set => islandStates = value; }

    [Header("Lists")]
    [SerializeField] private List<ProceduralPresets> proceduralPresets = new List<ProceduralPresets>();
    [SerializeField] private Transform[] parentTransforms;
    public Transform[] ParentTransforms { get => parentTransforms; set => parentTransforms = value; }

    //Variables
    private bool hasCompletedGeneration;
    public bool HasCompletedGeneration { get => hasCompletedGeneration; }
    private bool hasCompletedLootGeneration;
    public bool HasCompletedLootGeneration { get => hasCompletedLootGeneration; }

    [SerializeField]private int currentAmountOfItems;
    public int CurrentAmountOfItems { get => currentAmountOfItems; set => currentAmountOfItems = value; }

    private bool allItemsPlaced;
    private int maxAmountOfItems;

    //Components
    private GenerationManager generationManager;

    public static IslandGeneration instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start() => generationManager = GetComponent<GenerationManager>();

    private void Update() => HandleGenerationStates();

    private void HandleGenerationStates()
    {
        //Dont start until a certain state has triggered
        if (generationManager.GenerationStates != GenerationStates.IslandGeneration)
            return;

        //Switch between states until generation is completed
        switch (islandStates)
        {
            case IslandGenerationStates.Water:
                hasCompletedGeneration = false;
                CreateObject(IslandGenerationStates.Sand, true);
                break;

            case IslandGenerationStates.Sand:
                CreateObject(IslandGenerationStates.Mountain, true);
                break;

            case IslandGenerationStates.Mountain:
                CreateObject(IslandGenerationStates.Island, true);
                break;

            case IslandGenerationStates.Island:
                CreateObject(IslandGenerationStates.Rocks, true);
                break;

            case IslandGenerationStates.Rocks:
                CreateObject(IslandGenerationStates.Trees, false);
                break;

            case IslandGenerationStates.Trees:
                CreateObject(IslandGenerationStates.Cosmetics, false);
                break;

            case IslandGenerationStates.Cosmetics:
                CreateObject(IslandGenerationStates.Loot, false);
                break;

            case IslandGenerationStates.Loot:
                CreateObject(IslandGenerationStates.Player, false);
                break;

            case IslandGenerationStates.Player:
                CreateObject(IslandGenerationStates.Completed, false);
                break;

            case IslandGenerationStates.Completed:
                hasCompletedGeneration = true;
                break;
        }
    }

    #region Procedural Generation Function
    public void CreateObject(IslandGenerationStates nextState, bool increment)
    {
        //Set index
        int index = (int)IslandStates;
        //Set max number of objects
        maxAmountOfItems = RandomiseNumOfObjects(index);

        if (!proceduralPresets[index].requiresMultipleObjects)
            CalculateObjectInstantiation(index,
                parentTransforms[index], proceduralPresets[index].scaleUniformly);
        else
        {
            for (int i = 0; i < maxAmountOfItems; i++)
            {
                CalculateObjectInstantiation(index,
                     parentTransforms[index], proceduralPresets[index].scaleUniformly);

                if (increment)
                    currentAmountOfItems++;
            }
        }

        //A simply check to ensure that if all items
        if (currentAmountOfItems >= maxAmountOfItems)
        {
            currentAmountOfItems = 0;
            allItemsPlaced = true;
        }

        //Change state
        if (allItemsPlaced)
            islandStates = nextState;
    }

    /// <summary>
    /// This function allows for an object to be selected randomly 
    /// from an array whilst taking into account the chances of the object appearing
    /// </summary>
    private GameObject CalculateObjectInstantiation(int index, Transform transform, bool uniformScaling)
    {
        //value
        float itemWeight = 0;
    
        //Adds all rarities info a single value
        for (int i = 0; i < proceduralPresets[index].generationObjects.Count; i++)
            itemWeight += proceduralPresets[index].generationObjects[i].objectRarity;
    
        //Randomly picks value between that
        float randomValue = Random.Range(0, itemWeight);
    
        //Randomly select object and then select it
        for (int i = 0; i < proceduralPresets[index].generationObjects.Count; i++)
        {
            if (randomValue <= proceduralPresets[index].generationObjects[i].objectRarity)
            {
                //Instantiate the obj
                GameObject newObj = Instantiate(proceduralPresets[index].generationObjects[i].objectPrefab, RandomisePosition(index, transform), RandomiseRotation(index));
                //Randomise Scale
                newObj.transform.localScale = RandomiseScale(index, uniformScaling) * proceduralPresets[index].globalScaleMultiplier;
                newObj.transform.parent = transform;
                return newObj;
            }
            
            randomValue -= proceduralPresets[index].generationObjects[i].objectRarity;
        }
    
        return null;
    }
    #endregion

    #region Randomisation Functions
    //Randomises the X & Z position of an object
    public Vector3 RandomisePosition(int index, Transform parentPos)
    {
        return new Vector3(
            Random.Range(-proceduralPresets[index].xSpawnPos, proceduralPresets[index].xSpawnPos),
            parentPos.position.y, 
            Random.Range(-proceduralPresets[index].zSpawnPos, proceduralPresets[index].zSpawnPos));
    }
    
    //Randomises the Y rotation of an object
    public Quaternion RandomiseRotation(int index)
    {
        return Quaternion.Euler(
            Random.Range(0, proceduralPresets[index].xRotation),
            Random.Range(0, proceduralPresets[index].yRotation),
            Random.Range(0, proceduralPresets[index].zRotation));
    }
    
    //Randomises the scale of the object based on a set of parameters
    public Vector3 RandomiseScale(int index, bool uniformScaling)
    {
        if (uniformScaling)
        {
            float randomScale = Random.Range(proceduralPresets[index].uniformMinScale, proceduralPresets[index].uniformMaxScale);
            return new Vector3(randomScale, randomScale, randomScale);
        }
        else
            return new Vector3(
                Random.Range(proceduralPresets[index].xMinScale, proceduralPresets[index].xMaxScale), 
                Random.Range(proceduralPresets[index].yMinScale, proceduralPresets[index].yMaxScale), 
                Random.Range(proceduralPresets[index].zMinScale, proceduralPresets[index].zMaxScale));      
    }

    //Randomise the number of objects to create
    public int RandomiseNumOfObjects(int index)
    {
        return Random.Range(proceduralPresets[index].minNumOfObjects, proceduralPresets[index].maxNumOfObjects); ;
    }
    #endregion
}

public enum IslandGenerationStates
{
    Water,
    Sand,
    Mountain,
    Island,
    Rocks,
    Trees,
    Cosmetics,
    Loot,
    Player,
    Completed
}

