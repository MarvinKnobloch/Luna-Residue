using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemyreset
{
    public Enemymovement esm;

    const string idlestate = "Idle";
    const string runstate = "Run";
    const string dyingstate = "Enemydying";
    public void checkforreset()
    {
        esm.checkforresettimer += Time.deltaTime;
        if (esm.checkforresettimer > 0.5f)
        {
            if (Vector3.Distance(esm.spawnpostion, esm.transform.position) > esm.enemyresetrange || Vector3.Distance(LoadCharmanager.Overallmainchar.transform.position, esm.transform.position) > esm.enemyresetrange + 10)
            {
                esm.healticktimer = 0f;
                esm.gameObject.GetComponent<EnemyHP>().resetplayerhits();
                esm.spezialattack = false;
                esm.ChangeAnimationState(runstate);
                esm.healtickamount = esm.enemyhp.maxhealth * 0.02f;
                esm.state = Enemymovement.State.resetheal;
                if (Infightcontroller.infightenemylists.Contains(esm.transform.gameObject))
                {
                    Infightcontroller.infightenemylists.Remove(esm.transform.gameObject);
                    Infightcontroller.instance.checkifinfight();
                }
            }
            esm.checkforresettimer = 0f;
        }
    }
    public void resetheal()
    {
        esm.healticktimer += Time.deltaTime;
        if (esm.healticktimer > esm.healticksafterreset)
        {
            esm.Meshagent.SetDestination(esm.spawnpostion);                      //würde schon überschrieben als ich es bei checkforreset gecalled habe
            esm.enemyhp.enemyheal(esm.healtickamount);
            esm.healticktimer = 0f;
        }
        if (Vector3.Distance(esm.spawnpostion, esm.transform.position) < 2)
        {
            esm.currenttarget = LoadCharmanager.Overallmainchar;
            esm.Meshagent.ResetPath();
            esm.Meshagent.speed = esm.patrolspeed;
            esm.healtickamount = esm.enemyhp.maxhealth * 0.05f;
            esm.ChangeAnimationState(idlestate);
            esm.state = Enemymovement.State.idleheal;
        }
    }
    public void healwhileidle()
    {
        esm.healticktimer += Time.deltaTime;
        if (esm.healticktimer > esm.healticksafterreset)
        {
            esm.enemyhp.enemyheal(esm.healtickamount);
            esm.healticktimer = 0f;
            if (esm.enemyhp.currenthealth >= esm.enemyhp.maxhealth)
            {
                esm.patroltimer = 0f;
                esm.ChangeAnimationState(idlestate);
                esm.state = Enemymovement.State.waitfornextpatrolpoint;
            }
        }
        esm.checkforplayerinrange();
    }
    public void enemydied()
    {
        esm.ChangeAnimationState(dyingstate);
        esm.state = Enemymovement.State.empty;
    }
}
