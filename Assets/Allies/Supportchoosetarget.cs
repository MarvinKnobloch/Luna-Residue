using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supportchoosetarget
{
    public Supportmovement ssm;

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
                    return;
                }
            }
        }
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
