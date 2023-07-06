using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Towercontroller : MonoBehaviour
{
    private Nightshadecontroller nightshadecontroller;

    [SerializeField] private GameObject goal;
    [SerializeField] private GameObject timerUI;
    [SerializeField] private Text timertext;

    [NonSerialized] public float towercompletiontime;
    [NonSerialized] public float basedmg;
    [NonSerialized] public float completiondmg;
    private float towertimer;
    public bool dealdmg;

    private void Awake()
    {
        nightshadecontroller = GetComponentInParent<Nightshadecontroller>();
    }
    private void OnEnable()
    {
        StopAllCoroutines();
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
            if (dealdmg == true && Statics.infight == true)
            {
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 1), true);
            }
            towerdisable();
        }
    }
    private void towerdisable()
    {
        timerUI.SetActive(false);
        LoadCharmanager.Overallmainchar.transform.parent = null;
        gameObject.SetActive(false);
        nightshadecontroller.gameObject.SetActive(false);
    }

    public void bonusdmgandtowerdisable()
    {
        Statics.nextattackdealbonusdmg = true;
        Statics.nextattackbonusdmgamount += Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl * 1.5f, 1);
        StartCoroutine("waitfortowerdisable");
    }
    IEnumerator waitfortowerdisable()
    {
        yield return new WaitForSeconds(0.5f);
        towerdisable();
    }
}
