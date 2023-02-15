using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemyreset
{
    public Enemymovement esm;
    public static event Action infightlistupdate;

    const string idlestate = "Idle";
    const string runstate = "Run";
    public void checkforreset()
    {
        esm.checkforresettimer += Time.deltaTime;
        if (esm.checkforresettimer > 0.5f)
        {
            if (Vector3.Distance(esm.spawnpostion, esm.transform.position) > esm.enemyresetrange)
            {
                esm.healticktimer = 0f;
                esm.gameObject.GetComponent<EnemyHP>().enemyhasreset();
                esm.spezialattack = false;
                esm.ChangeAnimationState(runstate);
                esm.state = Enemymovement.State.resetheal;
                if (Infightcontroller.infightenemylists.Contains(esm.transform.root.gameObject))
                {
                    Infightcontroller.infightenemylists.Remove(esm.transform.root.gameObject);
                    infightlistupdate?.Invoke();
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
            esm.enemyhpscript.enemyheal(esm.healtickamount);
            esm.healticktimer = 0f;
        }
        esm.Meshagent.SetDestination(esm.spawnpostion);
        if (Vector3.Distance(esm.spawnpostion, esm.transform.position) < 2)
        {
            esm.currenttarget = LoadCharmanager.Overallmainchar;
            esm.Meshagent.ResetPath();
            esm.ChangeAnimationState(idlestate);
            esm.state = Enemymovement.State.idleheal;
        }
    }
    public void healwhileidle()
    {
        esm.healticktimer += Time.deltaTime;
        if (esm.healticktimer > esm.healticksafterreset)
        {
            esm.enemyhpscript.enemyheal(esm.healtickamount);
            esm.healticktimer = 0f;
            if (esm.enemyhpscript.currenthealth >= esm.enemyhpscript.maxhealth)
            {
                esm.patroltimer = 0f;
                esm.ChangeAnimationState(idlestate);
                esm.state = Enemymovement.State.waitfornextpatrolpoint;
            }
        }
        esm.checkforplayerinrange();
    }
}
