using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerearth
{
    public Movescript psm;

    const string earthslidereleasestate = "Earthsliderelease";
    public void earthslidestart()
    {
        if (Movescript.lockontarget != null)
        {
            psm.state = Movescript.State.Earthslide;
            if (Vector3.Distance(psm.transform.position, Movescript.lockontarget.position) > 2f)
            {
                psm.transform.position = Vector3.MoveTowards(psm.transform.position, Movescript.lockontarget.position, psm.earthslidespeed * Time.deltaTime);
                psm.transform.rotation = Quaternion.LookRotation(Movescript.lockontarget.transform.position - psm.transform.position, Vector3.up);
            }
            if (Vector3.Distance(psm.transform.position, Movescript.lockontarget.position) < 9f)
            {
                psm.ChangeAnimationState(earthslidereleasestate);
            }
        }
        else
        {
            psm.Abilitiesend();
        }
    }
    public void earthslidedmg()
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
                    float dmg = 15;
                    enemyscript.dmgonce = true;
                    enemyscript.TakeDamage(dmg, 0, false);
                    //psm.activatedmgtext(Enemyhit.gameObject, dmg);
                }

            }
        }
    }
}
