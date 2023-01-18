using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Towercontroller : MonoBehaviour
{
    private GameObject enemyroot;
    [SerializeField] private GameObject goal;
    [SerializeField] private GameObject timerUI;
    [SerializeField] private Text timertext;

    [NonSerialized] public float towercompletiontime;
    [NonSerialized] public float basedmg;
    [NonSerialized] public float completiondmg;
    private float towertimer;
    public bool dealdmg;

    private void OnEnable()
    {
        timerUI.SetActive(true);
        dealdmg = true;
        goal.SetActive(true);
        towertimer = towercompletiontime;
    }
    private void Update()
    {
        towertimer -= Time.deltaTime;
        float seconds = Mathf.FloorToInt(towertimer % 60);
        float milliseconds = towertimer % 1 * 100;
        timertext.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
        if (towertimer < 0)
        {
            if (dealdmg == true)
            {
                LoadCharmanager.Overallmainchar.GetComponent<SpielerHP>().TakeDamage(basedmg);
            }
            timerUI.SetActive(false);
            LoadCharmanager.Overallmainchar.transform.parent = null;
            gameObject.SetActive(false);
        }
    }

    public void setenemy(GameObject nigthshade)
    {
        enemyroot = nigthshade;
    }
    public void dealdmgtoenemyroot()
    {
        enemyroot.GetComponent<EnemyHP>().TakeDamage(completiondmg, 0, false);
    }
}
