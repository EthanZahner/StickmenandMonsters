using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FighterStats : MonoBehaviour, IComparable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject healthFill;

    [SerializeField]
    private GameObject manaFill;

    [Header("Stats")]
    public float health;
    public float mana;
    public float attack;
    public float spellAttack;
    public float defense;
    public float speed;
    public float armorClass;
    public float experience;
    public float gold;

    private float startHealth;
    private float startMana;

    [HideInInspector]
    public int nextActTurn;

    public bool dead = false;

    //resize health and magic bar
    private Transform healthTransform;
    private Transform manaTransform;

    private Vector2 healthScale;
    private Vector2 manaScale;

    private float xNewHealthScale;
    private float xNewManaScale;

    private GameObject GameControllerObj;

    void Awake()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        manaTransform = manaFill.GetComponent<RectTransform>();
        manaScale = manaFill.transform.localScale;

        startHealth = health;
        startMana = mana;

        GameControllerObj = GameObject.Find("GameControllerObject");

    }

    public void ReceiveDamage(float damage)
    {

        health = health - damage;
        
        
        if(health <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";
            animator.Play("death");
            Destroy(healthFill, 1);
            Destroy(gameObject, 1);
        }
        else if (damage > 0)
        {
            animator.Play("hit");
            xNewHealthScale = healthScale.x * (health / startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
        }
        if (damage > 0)
        {
            GameControllerObj.GetComponent<GameController>().battleText.gameObject.SetActive(true);
            GameControllerObj.GetComponent<GameController>().battleText.text = (damage.ToString() + " Damage");

        }

        Invoke("ContinueGame", 2);
    }

    public void updateManaFill(float cost)
    {

        if(cost > 0)
        {
            mana = mana - cost;
            xNewManaScale = manaScale.x * (mana / startMana);
            manaFill.transform.localScale = new Vector2(xNewManaScale, manaScale.y);
        }
    }

    public bool GetDead()
    {
        return dead;
    }
    void ContinueGame()
    {
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn();
    }

    void DeathAnimation()
    {

    }


    public void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f / speed);
    }

    public int CompareTo(object otherStats)
    {
        int nex = nextActTurn.CompareTo(((FighterStats)otherStats).nextActTurn);
        return nex;
    }
}