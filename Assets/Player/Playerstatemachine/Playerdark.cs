using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdark
{
    public Movescript psm;
    const string darkportalchargestate = "Darkportalcharge";
    const string darkportalendstate = "Darkportalend";
    public void darkportalstart()
    {
        if (Movescript.lockontarget != null)
        {
            Vector3 endposi = Movescript.lockontarget.position + new Vector3(0, 6, 0);
            psm.transform.position = Vector3.MoveTowards(psm.transform.position, endposi, psm.darkportalspeed * psm.darkspeedmultipler * Time.deltaTime);
            psm.transform.rotation = Quaternion.LookRotation(endposi - psm.transform.position);

            if (Vector3.Distance(psm.transform.position, endposi) < 3f)
            {
                Vector3 lookPos = Movescript.lockontarget.transform.position - psm.transform.position;
                lookPos.y = 0;
                psm.transform.rotation = Quaternion.LookRotation(lookPos);
                psm.ChangeAnimationState(darkportalchargestate);
                psm.state = Movescript.State.Empty;
            }
        }
        else psm.Abilitiesend();
    }
    public void usedarkportal()
    {
        if (psm.state != Movescript.State.Empty) return;
        if (Movescript.lockontarget != null)
        {
            psm.ChangeAnimationState(darkportalendstate);
            psm.graviti = -12;
            Physics.IgnoreLayerCollision(8, 6);
            Physics.IgnoreLayerCollision(11, 6);
            psm.state = Movescript.State.Darkportalend;
        }
        else
        {
            psm.Abilitiesend();
        }
    }

    public void darkportalending()
    {
        if (Movescript.lockontarget != null)
        {
            Vector3 endposi = Movescript.lockontarget.position + psm.transform.forward * -1;
            psm.transform.position = Vector3.MoveTowards(psm.transform.position, endposi, psm.darkportalspeed * Time.deltaTime);
            Vector3 lookPos = Movescript.lockontarget.transform.position - psm.transform.position;
            lookPos.y = 0;
            psm.transform.rotation = Quaternion.LookRotation(lookPos);

            if (Vector3.Distance(psm.transform.position, endposi) < 2f)
            {
                psm.eleAbilities.overlapssphereeledmg(psm.transform.gameObject, 4, 18);
                Physics.IgnoreLayerCollision(8, 6, false);
                Physics.IgnoreLayerCollision(11, 6, false);
                psm.switchtoairstate();
                Statics.otheraction = false;
            }
        }
        else psm.Abilitiesend();
    }
    public void darkportalend()
    {
        if (psm.state != Movescript.State.Darkportalend) return;
        psm.Abilitiesend();
    }
}

