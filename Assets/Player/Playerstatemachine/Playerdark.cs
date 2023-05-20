using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdark
{
    public Movescript psm;

    const string darkportalendstate = "Darkportalend";
    public void usedarkportal()
    {
        if (psm.state != Movescript.State.Empty) return;
        if (Movescript.lockontarget != null)
        {
            psm.transform.position = Movescript.lockontarget.position + new Vector3(0, 6, 0) + (psm.transform.forward * -2);
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
        psm.velocity = new Vector3(0, psm.graviti, 0);
        psm.charactercontroller.Move(psm.velocity * Time.deltaTime);

        if (Physics.SphereCast(psm.spherecastcollider.bounds.center, psm.spherecastcollider.radius, Vector3.down, out RaycastHit groundhit, 1.2f))
        {
            psm.eleAbilities.overlapssphereeledmg(psm.transform.gameObject, 3, 13);
            Physics.IgnoreLayerCollision(8, 6, false);
            Physics.IgnoreLayerCollision(11, 6, false);
            psm.state = Movescript.State.Ground;
            Statics.otheraction = false;
        }
    }
    public void darkportalend()
    {
        if (psm.state != Movescript.State.Darkportalend) return;
        psm.Abilitiesend();
    }
}

