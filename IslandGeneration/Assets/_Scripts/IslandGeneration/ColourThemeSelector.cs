using UnityEngine;
using System.Collections.Generic;

public class ColourThemeSelector : MonoBehaviour
{
    [SerializeField] private List<IslandColourPresets> colourThemes = new List<IslandColourPresets>();

    public void RandomlySelectColourTheme(int index)
    {
        //Change colours of Water
        colourThemes[index].waterMat.SetColor("_DepthGradientShallow", colourThemes[index].shallowWaterColour);
        colourThemes[index].waterMat.SetColor("_DepthGradientDeep", colourThemes[index].deepWaterColour);

        //Changes colours of Island objects
        for (int i = 0; i < colourThemes[index].islandColours.Length; i++)
            colourThemes[index].islandColours[i].mat.SetColor("_BaseColor", colourThemes[index].islandColours[i].newColour);

        //Change colours of Trees & Cosmetics
        for (int i = 0; i < colourThemes[index].environmentalPresets.Length; i++)
        {
            //Randomise index
            int randomIndex = Random.Range(0, colourThemes[index].newColours.Length);
            Debug.Log(randomIndex);
            //Assign colour
            colourThemes[index].environmentalPresets[i].mat.SetColor("_BaseColor", colourThemes[index].newColours[randomIndex]);

        }
    }
}
