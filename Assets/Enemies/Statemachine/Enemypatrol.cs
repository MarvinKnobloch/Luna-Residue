using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemypatrol
{
    public Enemymovement esm;

    const string idlestate = "Idle";
    const string patrolstate = "Patrol";

    public void triggerenemy()        //ontriggerenter
    {
        esm.Meshagent.speed = esm.patrolspeed;
        esm.patroltimer = 0f;
        esm.ChangeAnimationState(idlestate);
        esm.state = Enemymovement.State.waitfornextpatrolpoint;
    }
    public void waitfornextpatrolpoint()
    {
        esm.patroltimer += Time.deltaTime;
        checkforplayerinrange();
        if (esm.patroltimer > esm.patrolwaittimer)
        {
            esm.patroltimer = 0f;
            esm.patrolposi = esm.spawnpostion + Random.insideUnitSphere * 10;
            esm.patrolposi.y = esm.transform.position.y;
            NavMeshHit hit;
            bool blocked;
            blocked = NavMesh.Raycast(esm.transform.position, esm.patrolposi, out hit, NavMesh.AllAreas);
            if (blocked == true)
            {
                esm.patrolposi = hit.position;
                esm.Meshagent.SetDestination(esm.patrolposi);
                esm.ChangeAnimationState(patrolstate);
                esm.state = Enemymovement.State.patrol;
            }
            else
            {
                esm.Meshagent.SetDestination(esm.patrolposi);
                esm.ChangeAnimationState(patrolstate);
                esm.state = Enemymovement.State.patrol;
            }
        }
    }
    public void patrol()
    {
        checkforplayerinrange();
        if (Vector3.Distance(esm.transform.position, esm.patrolposi) <= 2)
        {
            esm.patroltimer = 0f;
            esm.ChangeAnimationState(idlestate);
            esm.Meshagent.ResetPath();
            esm.state = Enemymovement.State.waitfornextpatrolpoint;
        }
        else
        {
            esm.patrolposi.y = esm.transform.position.y;
            esm.Meshagent.SetDestination(esm.patrolposi);
        }
    }
    public void checkforplayerinrange()
    {
        esm.checkforplayertimer += Time.deltaTime;
        if (esm.checkforplayertimer > 0.5f)
        {
            esm.checkforplayertimer = 0;
            if (Vector3.Distance(esm.transform.position, LoadCharmanager.Overallmainchar.transform.position) < esm.aggrorangecheck)
            {
                esm.Meshagent.CalculatePath(LoadCharmanager.Overallmainchar.transform.position, esm.path);
                if (esm.path.status == NavMeshPathStatus.PathComplete)
                {
                    if (Physics.Linecast(esm.transform.position + Vector3.up, LoadCharmanager.Overallmainchar.transform.position + Vector3.up, esm.checkforplayerlayer, QueryTriggerInteraction.Ignore) == false)
                    {
                        if (!Infightcontroller.infightenemylists.Contains(esm.transform.gameObject))
                        {
                            if(Musiccontroller.instance != null)
                            {
                                if (esm.gameObject.GetComponent<Enemyisrewardobject>())
                                {
                                    Musiccontroller.instance.spezialbattlemusic();
                                }
                                else Musiccontroller.instance.enemynormalbattle();
                            }

                            Infightcontroller.infightenemylists.Add(esm.transform.gameObject);
                            int enemycount = Infightcontroller.infightenemylists.Count;
                            Statics.currentenemyspecialcd = Statics.enemyspecialcd + (enemycount * 2);
                            if (Infightcontroller.infightenemylists.Count == 1)
                            {
                                Infightcontroller.instance.checkifinfight();
                            }
                            triggerotherenemiesinrange();
                        }
                        esm.currenttarget = LoadCharmanager.Overallmainchar;
                        esm.Meshagent.speed = esm.normalnavspeed;
                        esm.normalattacktimer = esm.normalattackcd;
                        esm.state = Enemymovement.State.gettomeleerange;
                    }
                }
            }
        }
    }
    private void triggerotherenemiesinrange()
    {
        Collider[] colliders = Physics.OverlapSphere(esm.transform.position, esm.enemeytriggerrange, esm.meleehitboxlayer, QueryTriggerInteraction.Ignore);
        foreach (Collider obj in colliders)
        {
            if (obj.gameObject.TryGetComponent(out Enemymovement enemymovement))
            {
                enemymovement.enemyinrangeistriggered();
            }
        }
    }
    public void enemyinrangeistriggered()
    {
        if (!Infightcontroller.infightenemylists.Contains(esm.transform.gameObject))
        {
            Infightcontroller.infightenemylists.Add(esm.transform.gameObject);
            int enemycount = Infightcontroller.infightenemylists.Count;
            Statics.currentenemyspecialcd = Statics.enemyspecialcd + enemycount;
            if (Infightcontroller.infightenemylists.Count == 1)
            {
                Infightcontroller.instance.checkifinfight();
            }
            //triggerotherenemiesinrange();
        }
        esm.currenttarget = LoadCharmanager.Overallmainchar;
        esm.Meshagent.speed = esm.normalnavspeed;
        esm.normalattacktimer = esm.normalattackcd;
        esm.state = Enemymovement.State.gettomeleerange;
    }
}
