using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemyattack
{
    public Enemymovement esm;
    private float sideposition;

    const string idlestate = "Idle";
    const string runstate = "Run";
    const string attack1state = "Attack1"; 
    const string spezialattackstate = "Spezial";
    public void gettomeleerange()                              //der enemy wartet bis er attackieren kann, danach geht er erst zum target
    {
        esm.normalattacktimer += Time.deltaTime;
        if (esm.normalattacktimer > esm.normalattackcd)
        {
            if (Vector3.Distance(esm.transform.position, esm.currenttarget.transform.position + esm.transform.forward * -2) > esm.attackrange)
            {
                esm.ChangeAnimationState(runstate);
                esm.Meshagent.SetDestination(esm.currenttarget.transform.position + esm.transform.forward * -2);
            }
            else attack();
        }
        esm.FaceTraget();
        esm.checkforspezialattack();
        esm.checkforreset();
    }
    private void attack()
    {
        esm.normalattacktimer = 0;
        esm.ChangeAnimationState(attack1state);
        esm.state = Enemymovement.State.isattacking;
        return;
    }
    public void backtowaitforattack()
    {
        int newposi = Random.Range(0, 100);
        if (newposi < esm.chancetochangeposi)
        {
            esm.posiafterattack = esm.currenttarget.transform.position + Random.insideUnitSphere * 5;
            esm.posiafterattack.y = esm.transform.position.y;
            NavMeshHit hit;
            bool blocked;
            blocked = NavMesh.Raycast(esm.transform.position, esm.posiafterattack, out hit, NavMesh.AllAreas);
            if (blocked == true)
            {
                esm.posiafterattack = hit.position;
                esm.Meshagent.SetDestination(esm.posiafterattack);
                esm.ChangeAnimationState(runstate);
                esm.state = Enemymovement.State.changeposi;
            }
            else
            {
                esm.Meshagent.SetDestination(esm.posiafterattack);
                esm.ChangeAnimationState(runstate);
                esm.state = Enemymovement.State.changeposi;
            }
        }
        else
        {
            esm.ChangeAnimationState(idlestate);
            sideposition = Random.Range(-2f, 2f);
            esm.state = Enemymovement.State.waitforattacks;
        }
    }
    public void waitingforattacks()             //wenn nach dem attacken keine neue posi gesucht wird folgt der enemy dem target
    {
        esm.FaceTraget();
        esm.normalattacktimer += Time.deltaTime;
        if (esm.normalattacktimer > esm.normalattackcd)
        {
            esm.normalattacktimer = 0;
            esm.ChangeAnimationState(attack1state);
            esm.state = Enemymovement.State.isattacking;
            return;
        }
        esm.followplayerafterattack += Time.deltaTime;
        if (esm.followplayerafterattack > 0.3f)
        {
            if (esm.currenttarget.gameObject != LoadCharmanager.Overallmainchar.gameObject)
            {
                if (esm.currenttarget.gameObject.GetComponent<Supportmovement>().currenttarget != esm.gameObject)
                {
                    Vector3 newposi = esm.currenttarget.transform.position + esm.currenttarget.transform.forward * -2 + esm.currenttarget.transform.right * sideposition;
                    if (Vector3.Distance(esm.transform.position, newposi) > esm.attackrange)
                    {
                        esm.ChangeAnimationState(runstate);
                        esm.Meshagent.SetDestination(newposi);
                    }
                    else esm.ChangeAnimationState(idlestate);
                }
                else
                {
                    if (Vector3.Distance(esm.transform.position, esm.currenttarget.transform.position + esm.transform.forward * -2) > esm.attackrange)
                    {
                        esm.ChangeAnimationState(runstate);
                        esm.Meshagent.SetDestination(esm.currenttarget.transform.position + esm.transform.forward * -2);
                    }
                    else esm.ChangeAnimationState(idlestate);
                }
            }
            else
            {
                if (Vector3.Distance(esm.transform.position, esm.currenttarget.transform.position + esm.transform.forward * -2) > esm.attackrange)
                {
                    esm.ChangeAnimationState(runstate);
                    esm.Meshagent.SetDestination(esm.currenttarget.transform.position + esm.transform.forward * -2);
                }
                else esm.ChangeAnimationState(idlestate);
            }
            esm.followplayerafterattack = 0;
        }
        esm.checkforspezialattack();
    }
    public void repositionafterattack()
    {
        esm.posiafterattack.y = esm.transform.position.y;
        esm.normalattacktimer += Time.deltaTime;
        if (Vector3.Distance(esm.transform.position, esm.posiafterattack) <= 2)
        {
            if (esm.normalattacktimer > esm.normalattackcd - (float)0.2)
            {
                esm.normalattacktimer = esm.normalattackcd - (float)0.2;              // damit sich der gegner noch dreht bevor er angreift
            }
            esm.Meshagent.ResetPath();
            esm.ChangeAnimationState(idlestate);
            esm.state = Enemymovement.State.gettomeleerange;
        }
        esm.checkforspezialattack();
        esm.checkforreset();
    }
    public void checkforspezialattack()
    {
        if (esm.spezialattack == true)
        {
            esm.spezialattack = false;
            esm.ChangeAnimationState(spezialattackstate);
            esm.state = Enemymovement.State.spezialattack;
        }
    }
}

/*if (esm.currenttarget.gameObject != LoadCharmanager.Overallmainchar.gameObject)
{
    if (esm.currenttarget.gameObject.GetComponent<Supportmovement>().currenttarget != esm.gameObject)
    {
        if (Vector3.Distance(esm.transform.position, esm.currenttarget.transform.position + esm.currenttarget.transform.forward * -1) > esm.attackrange) //new Vector3(esm.currenttarget.transform.position.x, esm.transform.position.y, esm.currenttarget.transform.position.z)
        {
            esm.ChangeAnimationState(runstate);
            esm.Meshagent.SetDestination(esm.currenttarget.transform.position + esm.currenttarget.transform.forward * -1);
        }
        else attack();
    }
    else
    {
        if (Vector3.Distance(esm.transform.position, esm.currenttarget.transform.position) > esm.attackrange)
        {
            esm.ChangeAnimationState(runstate);
            esm.Meshagent.SetDestination(esm.currenttarget.transform.position);
        }
        else attack();
    }
}
else
{
    if (Vector3.Distance(esm.transform.position, esm.currenttarget.transform.position) > esm.attackrange)
    {
        esm.ChangeAnimationState(runstate);
        esm.Meshagent.SetDestination(esm.currenttarget.transform.position);
    }
    else attack();
}*/
