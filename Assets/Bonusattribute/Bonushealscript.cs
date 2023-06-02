using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonushealscript : MonoBehaviour
{
    public float healovertimeremainingtime;
    private float lowesthealth;
    private GameObject currenttarget;

    private void Awake()
    {
        enabled = false;
    }
    private void OnEnable()
    {
        StopCoroutine("healovertime");
        StartCoroutine("healovertime");
    }
    private void OnDisable()
    {
        StopCoroutine("healovertime");
    }
    private void Update()
    {
        healovertimeremainingtime -= Time.deltaTime;
    }
    IEnumerator healovertime()
    {
        while (true)
        {
            yield return new WaitForSeconds(healovertimeremainingtime);
            healovertimeremainingtime = Statics.bonushealtimer;
            lowesthealth = Statics.charcurrenthealth[Statics.currentfirstchar];
            currenttarget = LoadCharmanager.Overallmainchar.gameObject;
            checkforlowesthealth(LoadCharmanager.Overallsecondchar, Statics.currentsecondchar);
            checkforlowesthealth(LoadCharmanager.Overallthirdchar, Statics.currentthirdchar);
            checkforlowesthealth(LoadCharmanager.Overallforthchar, Statics.currentforthchar);
            if (currenttarget.TryGetComponent(out Playerhp playerhp))
            {
                playerhp.addhealthwithtext(playerhp.maxhealth / Statics.bonushealpercentage);
            }
        }
    }
    private void checkforlowesthealth(GameObject target, int charnumber)
    {
        if(target != null)
        {
            if(Statics.charcurrenthealth[charnumber] < Statics.charmaxhealth[charnumber])
            {
                if (Statics.charcurrenthealth[charnumber] < lowesthealth)
                {
                    lowesthealth = Statics.charcurrenthealth[charnumber];
                    currenttarget = target.gameObject;
                }
            }
        }
    }
}
