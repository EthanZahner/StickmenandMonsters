using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InitiativeTracker : MonoBehaviour
{
    public GameObject portraitTemplate; // Assign a simple UI image prefab in the Inspector
    public GameController gameController; // Reference to the GameController script
    private Dictionary<FighterStats, GameObject> portraitMap = new Dictionary<FighterStats, GameObject>();

    public void Initialize(List<FighterStats> fighters)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        portraitMap.Clear();

        foreach (var fighter in fighters)
        {
            if (fighter != null)
            {
                GameObject portrait = Instantiate(portraitTemplate, transform);
                portraitMap[fighter] = portrait;

                Sprite faceIcon = fighter.GetComponent<FighterAction>().faceIcon;
                Image portraitImage = portrait.GetComponent<Image>();
                portraitImage.sprite = faceIcon;

                portraitImage.color = Color.white; // Default color
            }
        }
    }


    public void UpdateHighlight(FighterStats currentTurn)
    {
        foreach (var fighter in portraitMap.Keys)
        {
            Image portraitImage = portraitMap[fighter].GetComponent<Image>();
            if (fighter == currentTurn)
            {
                portraitImage.color = Color.yellow; // Highlight current turn
            }
            else
            {
                portraitImage.color = Color.white; // Default color for others
            }
        }
    }

    public void RemovePortrait(FighterStats defeatedFighter)
    {
        if (portraitMap.ContainsKey(defeatedFighter))
        {
            Destroy(portraitMap[defeatedFighter]);
            portraitMap.Remove(defeatedFighter);
        }
    }


}
