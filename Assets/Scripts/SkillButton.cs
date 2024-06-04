using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    private GameObject hero;

    // Start is called before the first frame update
    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallBack(temp));
        hero = GameObject.FindGameObjectWithTag("Hero");
    }

    private void AttachCallBack(string btn)
    {
        if (btn.CompareTo("Skill 1") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("skill1");
        }
        else if (btn.CompareTo("Skill 2") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("skill2");
        }
        else
        {
            hero.GetComponent<FighterAction>().SelectAttack("skill3");
        }
    }
}
