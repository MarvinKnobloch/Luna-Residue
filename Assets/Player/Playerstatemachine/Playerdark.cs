using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdark
{
    public Movescript psm;

    const string darkportalendstate = "Darkportalend";
    public void usedarkportal()
    {
        if (Movescript.lockontarget != null)
        {
            psm.transform.position = Movescript.lockontarget.position + new Vector3(0, 10, 0) + (psm.transform.forward * -2);
            psm.ChangeAnimationState(darkportalendstate);
            psm.graviti = -17;
            psm.state = Movescript.State.Darkportalend;
        }
        else
        {
            psm.Abilitiesend();
        }
    }

    public void darkportalending()
    {
        psm.velocity = new Vector3(0, psm.graviti, 0);
        psm.charactercontroller.Move(psm.velocity * Time.deltaTime);

        if (Physics.SphereCast(psm.spherecastcollider.bounds.center, psm.spherecastcollider.radius, Vector3.down, out RaycastHit groundhit, 1.1f))
        {
            psm.eleAbilities.overlapssphereeledmg(psm.transform.gameObject, 2f, 13);
            psm.state = Movescript.State.Ground;
            Statics.otheraction = false;
        }
    }
}

