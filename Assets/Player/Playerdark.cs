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
            Collider[] cols = Physics.OverlapSphere(psm.transform.position, 2f, psm.spellsdmglayer);
            foreach (Collider Enemyhit in cols)
            {
                if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
                {
                    enemyscript.dmgonce = false;
                }
            }
            foreach (Collider Enemyhit in cols)
            {
                if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
                {
                    if (enemyscript.dmgonce == false)
                    {
                        float dmg = 13;
                        enemyscript.dmgonce = true;
                        enemyscript.TakeDamage(dmg);
                        psm.activatedmgtext(Enemyhit.gameObject, dmg);
                    }
                }
            }
            psm.state = Movescript.State.Ground;
            Statics.otheraction = false;
        }
    }
}

