using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject owner;

    [SerializeField]
    private string animationName;

    [SerializeField]
    private float energyCost;

    private FighterStat attackerStat;
    private FighterStat targetStat;

    private float damage = 0.0f;
    private Vector2 energyScale;

    private void Start()
    {
        energyScale = GameObject.Find("Hero Energy Bar Fill").GetComponent<RectTransform>().localScale;        
    }

    public void Fight(GameObject victim,int whichSkill)
    {
        attackerStat = owner.GetComponent<FighterStat>();
        targetStat = victim.GetComponent<FighterStat>();

        if(attackerStat.energy >= energyCost)
        {
            attackerStat.updateEnergyFill(energyCost);
            if(whichSkill == 1)
            {
                damage = attackerStat.attack;
                damage -= targetStat.defend;
                owner.GetComponent<Animator>().Play(animationName);
                targetStat.ReceiveDamage(damage);
            }
            else if(whichSkill == 2)
            {
                attackerStat.defend += 2;
                owner.GetComponent<Animator>().Play(animationName);
                targetStat.ReceiveDamage(damage);
            }           
        }
    }

}
