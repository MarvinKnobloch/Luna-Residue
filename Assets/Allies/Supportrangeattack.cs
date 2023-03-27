using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Supportrangeattack
{
    public Supportmovement ssm;

    private Vector3 rangenewposi;

    const string idlestate = "Idle";
    const string runstate = "Run";
    const string attack1state = "Attack1";
    const string attack2state = "Attack2";
    const string attack3state = "Attack3";

    public void waitforrangeattackcd()
    {
        if (ssm.currenttarget != null)
        {
            ssm.supportreset();
            ssm.attacktimer += Time.deltaTime;
            if (ssm.attacktimer > ssm.attackcd)
            {
                if (Vector3.Distance(ssm.transform.position, ssm.currenttarget.transform.position) > ssm.attackrangecheck + ssm.addedrangeattackrange)
                {
                    ssm.ChangeAnimationState(runstate);
                    ssm.Meshagent.SetDestination(ssm.currenttarget.transform.position);
                }
                else
                {
                    ssm.state = Supportmovement.State.attackstate;
                    ssm.ChangeAnimationState(attack1state);
                    ssm.Meshagent.ResetPath();
                }
            }
            else
            {
                ssm.FaceTraget();
            }
        }
        else
        {
            ssm.switchtarget();
        }
    }

    public void rangeattack1end()
    {
        attackrangecheck(attack2state);
    }
    public void rangeattack2end()
    {
        attackrangecheck(attack3state);
    }
    public void rangeattack3end()
    {
        afterattackaction();
    }
    private void afterattackaction()
    {
        ssm.attacktimer = 0f;
        if (ssm.currenttarget != null && ssm.playerhp.playerisdead == false)
        {
            int newposi = Random.Range(0, 100);
            if (newposi < ssm.chancetochangeposi)
            {
                ssm.posiafterattack = ssm.transform.position + Random.insideUnitSphere * 5;
                ssm.posiafterattack.y = ssm.transform.position.y;
                NavMeshHit hit;
                bool blocked;
                blocked = NavMesh.Raycast(ssm.transform.position, ssm.posiafterattack, out hit, NavMesh.AllAreas);
                if (blocked == true)
                {
                    ssm.posiafterattack = hit.position;
                    ssm.Meshagent.SetDestination(ssm.posiafterattack);
                    ssm.ChangeAnimationState(runstate);
                    ssm.state = Supportmovement.State.changeposiafterattack;                       //wenn nach dem attacken eine neue posi gesucht wird bleibt der char an der posi stehen bis er attacken kann
                }
                else
                {
                    ssm.Meshagent.SetDestination(ssm.posiafterattack);
                    ssm.ChangeAnimationState(runstate);
                    ssm.state = Supportmovement.State.changeposiafterattack;
                }
            }
            else
            {
                movebehindtartget();
            }
        }
    }
    public void movebehindtartget()
    {
        if (ssm.currenttarget != null)
        {
            ssm.supportreset();
            if (ssm.gameObject != ssm.currenttarget.GetComponentInParent<Enemymovement>().currenttarget)                          //wenn der rangesupport das target vom target ist, bewegt er sich nicht
            {
                rangenewposi = ssm.currenttarget.transform.position + ssm.currenttarget.transform.forward * -10;
                rangenewposi.y = ssm.transform.position.y;
                NavMeshHit hit;
                bool blocked;
                blocked = NavMesh.Raycast(ssm.transform.position, rangenewposi, out hit, NavMesh.AllAreas);
                if (blocked == true)
                {
                    rangenewposi = hit.position;
                    ssm.Meshagent.SetDestination(rangenewposi);
                    ssm.ChangeAnimationState(runstate);
                    ssm.state = Supportmovement.State.changeposiafterrangeattack;
                }
                else
                {
                    ssm.Meshagent.SetDestination(rangenewposi);
                    ssm.ChangeAnimationState(runstate);
                    ssm.state = Supportmovement.State.changeposiafterrangeattack;
                }
            }
            else
            {
                ssm.Meshagent.ResetPath();
                ssm.ChangeAnimationState(idlestate);
                ssm.state = Supportmovement.State.waitforrangeattackcd;
            }
        }
        else
        {
            ssm.switchtarget();
        }
    }
    public void posiafterrangeattack()
    {
        if (ssm.currenttarget != null)
        {
            ssm.supportreset();
            ssm.attacktimer += Time.deltaTime;
            if (ssm.attacktimer > ssm.attackcd)
            {
                ssm.state = Supportmovement.State.attackstate;
                ssm.ChangeAnimationState(attack1state);
                ssm.Meshagent.ResetPath();
            }
            else if (Vector3.Distance(ssm.transform.position, rangenewposi) < 4)
            {
                ssm.Meshagent.ResetPath();
                ssm.ChangeAnimationState(idlestate);
                ssm.state = Supportmovement.State.waitforrangeattackcd;
            }
        }
        else
        {
            ssm.switchtarget();
        }
    }
    private void attackrangecheck(string state)
    {
        if (ssm.currenttarget != null && ssm.playerhp.playerisdead == false)
        {
            if (Vector3.Distance(ssm.transform.position, ssm.currenttarget.transform.position) > ssm.attackrangecheck + ssm.addedrangeattackrange)
            {
                afterattackaction();
            }
            else
            {
                ssm.ChangeAnimationState(state);
            }
        }
    }
}
