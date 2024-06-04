using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private List<FighterStat> fighterStats;

    [SerializeField]
    private GameObject battleMenu;

    [SerializeField]
    public TextMeshProUGUI damageDealText;

    // Start is called before the first frame update
    void Start()
    {
        fighterStats = new List<FighterStat>();
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        FighterStat currentFighterStat = hero.GetComponent<FighterStat>();
        currentFighterStat.CountNextTurn(0);
        fighterStats.Add(currentFighterStat);
        foreach (var x in fighterStats)
        {
            Debug.Log(x.ToString());
        }

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        FighterStat currentEnemyStat = enemy.GetComponent<FighterStat>();
        currentEnemyStat.CountNextTurn(0);
        fighterStats.Add(currentEnemyStat);
        foreach (var x in fighterStats)
        {
            Debug.Log(x.ToString());
        }
        fighterStats.Sort();
        this.battleMenu.SetActive(false);
        NextTurn();
    }

    public void NextTurn()
    {
        damageDealText.gameObject.SetActive(false);
        FighterStat currentFighterStats = fighterStats[0];
        fighterStats.Remove(currentFighterStats);
        if (!currentFighterStats.GetDead())
        {
            GameObject currentUnit = currentFighterStats.gameObject;
            currentFighterStats.CountNextTurn(currentFighterStats.nextActTurn);
            fighterStats.Add(currentFighterStats);
            fighterStats.Sort();
            if (currentUnit.tag == "Hero")
            {
                this.battleMenu.SetActive(true);
            }
            else
            {
                this.battleMenu.SetActive(false);
                currentUnit.GetComponent<FighterAction>().SelectAttack("skill1");
            }
        }
        else
        {
            NextTurn();
        }
    }
}
