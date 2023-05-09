using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemypatrol
{
    public Enemymovement esm;

    private float distance;
    private float maxenemytriggerdistance = 50;

    const string idlestate = "Idle";
    const string patrolstate = "Patrol";

    public void triggerenemy()        //ontriggerenter
    {
        esm.meshagent.speed = esm.patrolspeed;
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
                esm.meshagent.SetDestination(esm.patrolposi);
                esm.ChangeAnimationState(patrolstate);
                esm.state = Enemymovement.State.patrol;
            }
            else
            {
                esm.meshagent.SetDestination(esm.patrolposi);
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
            esm.meshagent.ResetPath();
            esm.state = Enemymovement.State.waitfornextpatrolpoint;
        }
        else
        {
            esm.patrolposi.y = esm.transform.position.y;
            esm.meshagent.SetDestination(esm.patrolposi);
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
                if (Physics.Linecast(esm.transform.position + Vector3.up, LoadCharmanager.Overallmainchar.transform.position + Vector3.up, esm.checkforplayerlayer, QueryTriggerInteraction.Ignore) == false)
                {
                    esm.meshagent.CalculatePath(LoadCharmanager.Overallmainchar.transform.position, esm.path);
                    if (esm.path.status == NavMeshPathStatus.PathComplete)
                    {       
                        if (checkdistance() == true)
                        {
                            cantriggerenemy();
                        }
                    }
                }
            }
        }
    }
    private bool checkdistance()
    {
        distance = 0;
        Vector3[] corners = esm.path.corners;

        if (corners.Length > 2)
        {
            for (int i = 1; i < corners.Length; i++)
            {
                Vector2 previous = new Vector2(corners[i - 1].x, corners[i - 1].z);
                Vector2 current = new Vector2(corners[i].x, corners[i].z);

                distance += Vector2.Distance(previous, current);
                if (distance > maxenemytriggerdistance) return false;
            }
            return true;
        }
        else
        {
            return true;
        }
    }
    private void cantriggerenemy()
    {
        if (!Infightcontroller.infightenemylists.Contains(esm.transform.gameObject))
        {
            if (Musiccontroller.instance != null)
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
        esm.enemyhp.fightstartsettraget();
        esm.meshagent.speed = esm.normalnavspeed;
        esm.normalattacktimer = esm.normalattackcd / 2;
        esm.state = Enemymovement.State.followplayerthenwaitforattackcd;
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
        esm.enemyhp.fightstartsettraget();
        esm.meshagent.speed = esm.normalnavspeed;
        esm.normalattacktimer = esm.normalattackcd / 2;
        esm.state = Enemymovement.State.followplayerthenwaitforattackcd;
    }
}
