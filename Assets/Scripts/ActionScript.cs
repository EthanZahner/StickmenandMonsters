using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Transactions;
using UnityEngine.SocialPlatforms;

public class ActionScript : MonoBehaviour
{
    public GameObject owner;

    [SerializeField]
    private string animationName;

    [SerializeField]
    private bool manaAttack;

    [SerializeField]
    private float manaCost;

    [SerializeField]
    private float minAttackMultiplier;

    [SerializeField]
    private float maxAttackMultiplier;

    [SerializeField]
    private float minDefenseMultiplier;

    [SerializeField]
    private float maxDefenseMultiplier;

    private FighterStats attackerStats;
    private FighterStats targetStats;
    private float damage = 0.0f;

    private GameObject GameControllerObj;
    public string manaMessage = "Not Enough Mana!";

    void Awake()
    {
        GameControllerObj = GameObject.Find("GameControllerObject");
    }

    public void Attack(GameObject victim)
    {
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();

        float multiplier = Random.Range(minAttackMultiplier, maxAttackMultiplier);

         damage = multiplier * attackerStats.attack;

        float defenseMultiplier = Random.Range(minDefenseMultiplier, maxDefenseMultiplier);
        damage = Mathf.Max(0, damage - (defenseMultiplier * targetStats.defense));
        owner.GetComponent<Animator>().Play("attack");
        targetStats.ReceiveDamage(Mathf.CeilToInt(damage));
    }
    public void Spell(GameObject victim)
    {
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();

        if (attackerStats.mana >= manaCost)
        {
            float multiplier = Random.Range(minAttackMultiplier, maxAttackMultiplier);
            damage = multiplier * attackerStats.spellAttack;
            float defenseMultiplier = Random.Range(minDefenseMultiplier, maxDefenseMultiplier);
            damage = Mathf.Max(0, damage - (defenseMultiplier * targetStats.defense));
            owner.GetComponent<Animator>().Play("attack");
            targetStats.ReceiveDamage(Mathf.CeilToInt(damage));
            attackerStats.updateManaFill(manaCost);
        }
        else
        {
            Invoke("SkipTurnContinueGame", 2);
        }
    }
        
    public void Ability(GameObject victim)

    {
        owner.GetComponent<Animator>().Play(animationName);
    }
    public void Inventory(GameObject victim)

    {
        owner.GetComponent<Animator>().Play(animationName);
    }
    public void Rest(GameObject victim)

    {
        owner.GetComponent<Animator>().Play(animationName);
    }
    public void Options(GameObject victim)

    {
        owner.GetComponent<Animator>().Play(animationName);
    }

    void SkipTurnContinueGame()
    {
        //doesn't work
        GameControllerObj.GetComponent<GameController>().battleText.gameObject.SetActive(true);
        GameControllerObj.GetComponent<GameController>().battleText.text = ("Not Enough Mana");
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn();
    }
}

