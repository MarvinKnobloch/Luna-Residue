using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemyattack
{
    public Enemymovement esm;
    private float sideposition;

    private bool ismovingrotation;

    const string idlestate = "Idle";
    const string runstate = "Run";
    const string spezialattackstate = "Spezial";
    public void gettomeleerange()                              //der enemy wartet bis er attackieren kann, danach geht er erst zum target
    {
        esm.normalattacktimer += Time.deltaTime;
        if (esm.normalattacktimer > esm.normalattackcd)
        {
            esm.followplayerafterattack += Time.deltaTime;
            if (esm.followplayerafterattack > 0.3f)
            {
                Ray ray = new Ray(LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 0.3f, Vector3.down);     //ray nach unten um die y vom navmesh zu finden(falls man im sprung ist)
                if (Physics.Raycast(ray, out RaycastHit hit, 5f))
                {
                    esm.meshagent.CalculatePath(hit.point, esm.path);                                                 //wenn die momentan posi nicht erreichbar ist attackiert der gegner
                    if (esm.path.status != NavMeshPathStatus.PathComplete)
                    {
                        esm.switchtoattackstate();
                        return;
                    }
                }
                Vector3 targetposi = new Vector3(esm.currenttarget.transform.position.x, esm.transform.position.y, esm.currenttarget.transform.position.z) + esm.transform.forward * -2;
                if (Vector3.Distance(esm.transform.position, targetposi) > esm.attackrange)                                  //+esm.transform.forward * -2      enemy bekommt selten einen drehwurm   
                {
                    esm.ChangeAnimationState(runstate);
                    esm.meshagent.SetDestination(targetposi);                                                                //+esm.transform.forward * -2
                }
                else
                {
                    esm.switchtoattackstate();
                    return;
                }
                esm.followplayerafterattack = 0;
            }
        }
        else
        {
            esm.FaceTraget();
        }
        checkforspezialattack();
    }
    public void backtowaitforattack()
    {
        int newposi = Random.Range(0, 100);
        if (newposi < esm.chancetochangeposi)
        {
            switchtochangeposi();
        }
        else
        {
            if (esm.currenttarget.gameObject == LoadCharmanager.Overallmainchar.gameObject)
            {
                Ray ray = new Ray(LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 0.3f, Vector3.down);   //falls der momentane punkt nicht erreicht werden kann wir zu switchtochangeposi gewechselt
                if (Physics.Raycast(ray, out RaycastHit hit, 5f))                                                          // switchtochangeposi findet den nähsten(durch navmeshraycast) punkte der noch erreicht werden kann
                {
                    esm.meshagent.CalculatePath(hit.point, esm.path);
                    if (esm.path.status != NavMeshPathStatus.PathComplete)
                    {
                        switchtochangeposi();
                    }
                    else switchtowaitforattack();
                }
                else switchtowaitforattack();
            }
            else switchtowaitforattack();
        }
    }
    private void switchtochangeposi()
    {
        esm.posiafterattack = esm.currenttarget.transform.position + Random.insideUnitSphere * 5;
        esm.posiafterattack.y = esm.transform.position.y;
        NavMeshHit hit;
        bool blocked;
        blocked = NavMesh.Raycast(esm.transform.position, esm.posiafterattack, out hit, NavMesh.AllAreas);
        if (blocked == true)
        {
            esm.posiafterattack = hit.position;
            esm.meshagent.SetDestination(esm.posiafterattack);
            esm.ChangeAnimationState(runstate);
            esm.state = Enemymovement.State.changeposi;
        }
        else
        {
            esm.meshagent.SetDestination(esm.posiafterattack);
            esm.ChangeAnimationState(runstate);
            esm.state = Enemymovement.State.changeposi;
        }
    }
    private void switchtowaitforattack()
    {
        ismovingrotation = false;
        esm.ChangeAnimationState(idlestate);
        sideposition = Random.Range(-2f, 2f);
        esm.state = Enemymovement.State.waitforattacks;
    }
    public void waitingforattacks()             //wenn nach dem attacken keine neue posi gesucht wird folgt der enemy dem target
    {    
        esm.normalattacktimer += Time.deltaTime;
        esm.followplayerafterattack += Time.deltaTime;
        if (esm.followplayerafterattack > 0.3f)
        {
            if (esm.currenttarget.gameObject != LoadCharmanager.Overallmainchar.gameObject)
            {
                if (esm.currenttarget.gameObject.GetComponent<Supportmovement>().currenttarget != esm.gameObject)
                {
                    Vector3 newposi = new Vector3(esm.currenttarget.transform.position.x, esm.transform.position.y, esm.currenttarget.transform.position.z) + esm.transform.forward * -2 + esm.currenttarget.transform.right * sideposition;
                    if (Vector3.Distance(esm.transform.position, newposi) > esm.attackrange)
                    {
                        ismovingrotation = true;
                        esm.ChangeAnimationState(runstate);
                        esm.meshagent.SetDestination(newposi);
                    }
                    else
                    {
                        if (esm.normalattacktimer > esm.normalattackcd)
                        {
                            esm.switchtoattackstate();
                            return;
                        }
                        else
                        {
                            esm.ChangeAnimationState(idlestate);
                            ismovingrotation = false;
                        }
                    }
                }
                else
                {
                    supportistarget();
                }
            }
            else
            {
                maincharistarget();
            }
            esm.followplayerafterattack = 0;
        }
        if (ismovingrotation == false) esm.FaceTraget();
        checkforspezialattack();
    }
    private void maincharistarget()
    {
        Ray ray = new Ray(LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 0.3f, Vector3.down);              //ray nach unten um die y vom navmesh zu finden(falls man im sprung ist)
        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            Vector3 targetposi = new Vector3(esm.currenttarget.transform.position.x, esm.transform.position.y, esm.currenttarget.transform.position.z) + esm.transform.forward * -2;
            esm.meshagent.CalculatePath(hit.point, esm.path);
            if (esm.path.status != NavMeshPathStatus.PathComplete)                   //wenn die momentan posi nicht erreichbar ist attackiert der gegner
            {
                if (esm.normalattacktimer > esm.normalattackcd)
                {
                    esm.switchtoattackstate();
                    return;
                }
                else
                {
                    ismovingrotation = false;
                    esm.ChangeAnimationState(idlestate);
                }
            }

            else if (Vector3.Distance(esm.transform.position, targetposi) > esm.attackrange)
            {
                ismovingrotation = true;
                esm.ChangeAnimationState(runstate);
                esm.meshagent.SetDestination(targetposi);
            }
            else
            {
                if (esm.normalattacktimer > esm.normalattackcd)
                {
                    esm.switchtoattackstate();
                    return;
                }
                else
                {
                    ismovingrotation = false;
                    esm.ChangeAnimationState(idlestate);
                }
            }
        }
        else
        {
            Debug.Log("ray doesnt hit");
            if (esm.normalattacktimer > esm.normalattackcd)
            {
                esm.switchtoattackstate();
                return;
            }
            else
            {
                ismovingrotation = false;
                esm.ChangeAnimationState(idlestate);
            }
                
        }
    }
    private void supportistarget()
    {
        if (Vector3.Distance(esm.transform.position, esm.currenttarget.transform.position) > esm.attackrange)
        {
            ismovingrotation = true;
            esm.ChangeAnimationState(runstate);
            esm.meshagent.SetDestination(esm.currenttarget.transform.position);
        }
        else
        {
            if (esm.normalattacktimer > esm.normalattackcd)
            {
                esm.switchtoattackstate();
                return;
            }
            else 
            {
                ismovingrotation = false;
                esm.ChangeAnimationState(idlestate);
            }
        }        
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
            esm.meshagent.ResetPath();
            esm.ChangeAnimationState(idlestate);
            esm.state = Enemymovement.State.gettomeleerange;
        }
        checkforspezialattack();
    }
    public void checkforspezialattack()
    {
        if (esm.spezialattack == true)
        {
            esm.spezialattack = false;
            esm.ChangeAnimationState(spezialattackstate);
            Statics.currentenemyspeziallvl = esm.enemyhp.enemylvl;
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
