using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supportutilityfunctions
{
    public Supportmovement ssm;

    const string idlestate = "Idle";
    const string runstate = "Run";

    public void attackstate()
    {
        if (ssm.currenttarget == null)
        {
            ssm.switchtarget();
        }
        else
        {
            supportreset();
            ssm.FaceTraget();
        }
    }
    public void repositionafterattack()
    {
        if (ssm.currenttarget != null)
        {
            supportreset();
            ssm.posiafterattack.y = ssm.transform.position.y;
            ssm.Meshagent.SetDestination(ssm.posiafterattack);
            if (Vector3.Distance(ssm.transform.position, ssm.posiafterattack) <= 2)
            {
                ssm.Meshagent.ResetPath();
                ssm.ChangeAnimationState(idlestate);
                ssm.switchtoweaponstate();
            }
        }
        else
        {
            ssm.switchtarget();
        }
    }
    public void supportreset()
    {
        ssm.resettimer += Time.deltaTime;
        if (ssm.resettimer > 0.4f)
        {
            if (Vector3.Distance(ssm.currenttarget.transform.position, LoadCharmanager.Overallmainchar.transform.position) > ssm.supportresetrange)
            {
                ssm.ChangeAnimationState(runstate);
                ssm.state = Supportmovement.State.reset;
            }
            ssm.resettimer = 0;
        }
    }
    public void resetcombat()
    {
        ssm.resetcombattimer += Time.deltaTime;
        if (ssm.resetcombattimer > 0.4f)
        {
            if (Vector3.Distance(ssm.transform.position, LoadCharmanager.Overallmainchar.transform.position) < 3)
            {
                ssm.ChangeAnimationState(idlestate);
                ssm.Meshagent.ResetPath();
                if (Statics.infight == true)
                {
                    Collider[] colliders = Physics.OverlapSphere(LoadCharmanager.Overallmainchar.transform.position, 5, ssm.hitbox);
                    foreach (Collider checkforenemys in colliders)
                    {
                        if (checkforenemys.GetComponentInChildren<Miniadd>())
                        {
                            continue;
                        }
                        else if (checkforenemys.GetComponentInParent<EnemyHP>())
                        {
                            ssm.currenttarget = checkforenemys.gameObject;
                            if (ssm.currenttarget.GetComponentInParent<CapsuleCollider>())                                            //gegner muss ein Capuslecollider haben
                            {
                                ssm.attackrangecheck = (float)(ssm.currenttarget.GetComponentInParent<CapsuleCollider>().radius + ssm.addedattackrangetocollider);
                                ssm.switchtoweaponstate();
                                return;
                            }
                        }
                    }
                    ssm.resetcombattimer = 0;
                }
            }
            else
            {
                ssm.Meshagent.SetDestination(LoadCharmanager.Overallmainchar.transform.position);
                ssm.ChangeAnimationState(runstate);
            }
            ssm.resetcombattimer = 0;
        }
    }
}
