using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{
    [SerializeField]
    private bool physical;

    private GameObject currentPlayer;

    void Start()
    {
        Debug.Log("GameLoaded");
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallback(temp));
        currentPlayer = GameObject.FindGameObjectWithTag("currentPlayer");
    }


    private void AttachCallback(string btn)
    {
        if (btn.CompareTo("AttackBtn") == 0)
        {
            currentPlayer.GetComponent<FighterAction>().SelectAttack("attack");
        } else if (btn.CompareTo("SpellBtn") == 0)
        {
            currentPlayer.GetComponent<FighterAction>().SelectAttack("spell");
        } else if (btn.CompareTo("AbilityBtn") == 0)
        {
            currentPlayer.GetComponent<FighterAction>().SelectAttack("ability");
        } else if (btn.CompareTo("InventoryBtn") == 0)
        {
            currentPlayer.GetComponent<FighterAction>().SelectAttack("inventory");
        } else if (btn.CompareTo("RestBtn") == 0)
        {
            currentPlayer.GetComponent<FighterAction>().SelectAttack("rest");
        }
        else if (btn.CompareTo("OptionsBtn") == 0)
        {
            currentPlayer.GetComponent<FighterAction>().SelectAttack("options");
        }
    }
}
