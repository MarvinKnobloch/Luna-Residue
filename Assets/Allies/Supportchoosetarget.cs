using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supportchoosetarget
{
    public Supportmovement ssm;

    const string idlestate = "Idle";
    public void settarget()
    {
        Collider[] colliders = Physics.OverlapSphere(LoadCharmanager.Overallmainchar.transform.position, ssm.lookfortargetrange, ssm.hitbox);
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
        ssm.ChangeAnimationState(idlestate);
        ssm.state = Supportmovement.State.reset;


    }
    public void playerfocustarget()
    {
        if (Movescript.lockontarget != null)
        {
            ssm.currenttarget = Movescript.lockontarget.gameObject;
            if (ssm.currenttarget.GetComponentInParent<CapsuleCollider>())                                            //gegner muss ein Capuslecollider haben
            {
                ssm.attackrangecheck = (float)(ssm.currenttarget.GetComponentInParent<CapsuleCollider>().radius + ssm.addedattackrangetocollider);
            }
        }
    }
}
