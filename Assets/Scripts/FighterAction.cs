using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAction : MonoBehaviour
{
    private GameObject enemy;
    private GameObject hero;

    [SerializeField]
    private GameObject skill1Prefab;

    [SerializeField]
    private GameObject skill2Prefab;

    [SerializeField]
    private GameObject avatarPrefab;

    private GameObject currentAttack;

    private void Start()
    {
        hero = GameObject.FindWithTag("Hero");
        enemy = GameObject.FindWithTag("Enemy");
    }

    public void SelectAttack(string btn)
    {
        GameObject victim = hero;
        if(tag == "Hero")
        {
            victim = enemy;
        }

        if(btn.CompareTo("skill1") == 0)
        {
            skill1Prefab.GetComponent<Attack>().Fight(victim,1);
        }
        else if (btn.CompareTo("skill2") == 0)
        {
            skill2Prefab.GetComponent<Attack>().Fight(victim,2);
        }
        else
        {
            Debug.Log("3");
        }
    }
}
