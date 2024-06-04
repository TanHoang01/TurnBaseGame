using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FighterStat : MonoBehaviour,IComparable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject healthFill;

    [SerializeField]
    private GameObject energyFill;

    [Header("Stats")]
    public float health;
    public float energy;
    public float attack;
    public float defend;
    public float range;
    public float speed;

    private float startHealth;
    private float startEnergy;

    [HideInInspector]
    public int nextActTurn;

    private bool dead = false;

    private Transform healthTransform;
    private Transform energyTransform;

    private Vector2 healthScale;
    private Vector2 energyScale;

    private float xNewHealthScale;
    private float xNewEnergyScale;

    private GameObject gameControllerObj;

    private void Start()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        energyTransform = energyFill.GetComponent<RectTransform>();
        energyScale = energyFill.transform.localScale;

        startHealth = health;
        startEnergy = energy;

        gameControllerObj = GameObject.Find("Game Controller Object");
    }

    public void ReceiveDamage(float damage)
    {
        if (damage > 0)
        {
            health = health - damage;
            animator.Play("attacked");

            if (health <= 0)
            {
                dead = true;
                gameObject.tag = "Dead";
                Destroy(healthFill);
                Destroy(energyFill);
                Destroy(gameObject,1);
            }

            xNewHealthScale = healthScale.x * (health / startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);

            // restore 20 energy when being atatcked
            if (energy < startEnergy) restoreEnergyFill();

            // show damage deal
            gameControllerObj.GetComponent<GameController>().damageDealText.gameObject.SetActive(true);
            gameControllerObj.GetComponent<GameController>().damageDealText.text = "Damage: " + damage.ToString();
        }
        Invoke("KeepGameGoing", 2);
    }

    public void updateEnergyFill(float cost)
    {
        energy = energy - cost;
        xNewEnergyScale = energyScale.x * (energy / startEnergy);
        energyFill.transform.localScale = new Vector2(xNewEnergyScale, energyScale.y);
    }

    public void restoreEnergyFill()
    {
        energy = energy + 20;
        xNewEnergyScale = energyScale.x * (energy / startEnergy);
        energyFill.transform.localScale = new Vector2(xNewEnergyScale, energyScale.y);
    }

    public bool GetDead()
    {
        return dead; 
    }

    public void CountNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + 1;
        //Mathf.CeilToInt(100f / speed)
    }

    void KeepGameGoing()
    {
        GameObject.Find("Game Controller Object").GetComponent<GameController>().NextTurn();
    }

    public int CompareTo(object otherStat)
    {
        int nex = nextActTurn.CompareTo(((FighterStat)otherStat).nextActTurn);
        return nex;
    }
}
