using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAction : MonoBehaviour
{
    private GameObject currentEnemy;
    private GameObject currentPlayer;

    [SerializeField]
    private GameObject attackPreFab;

    [SerializeField]
    private GameObject spellPreFab;

    [SerializeField]
    private GameObject abilityPreFab;

    [SerializeField]
    private GameObject inventoryPreFab;

    [SerializeField]
    private GameObject restPreFab;

    [SerializeField]
    private GameObject optionsPreFab;


    [SerializeField]
    private Sprite faceIcon;

    private GameObject currentAction;

    void Awake()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("currentPlayer");
        currentEnemy = GameObject.FindGameObjectWithTag("currentEnemy");

    }



    public void SelectAttack(string btn)
    {
        GameObject victim = currentPlayer;

        if (tag == "currentPlayer")
        {
            victim = currentEnemy;
        }

        if (btn.CompareTo("attack") == 0)
        {
            Debug.Log("Attack!");
            attackPreFab.GetComponent<ActionScript>().Attack(victim);
        } else if (btn.CompareTo("spell") == 0)
        {
            Debug.Log("Spell!");
            spellPreFab.GetComponent<ActionScript>().Spell(victim);
        } else if (btn.CompareTo("ability") == 0)
        {
            Debug.Log("Ability!");
            abilityPreFab.GetComponent<ActionScript>().Ability(victim);
        }
        else if (btn.CompareTo("inventory") == 0)
        {
            Debug.Log("Inventory!");
            inventoryPreFab.GetComponent<ActionScript>().Inventory(victim);
        }
        else if (btn.CompareTo("rest") == 0)
        {
            Debug.Log("Rest!");
            restPreFab.GetComponent<ActionScript>().Rest(victim);
        }
        else if (btn.CompareTo("options") == 0)
        {
            Debug.Log("Options!");
            optionsPreFab.GetComponent<ActionScript>().Options(victim);
        }


    }
}
