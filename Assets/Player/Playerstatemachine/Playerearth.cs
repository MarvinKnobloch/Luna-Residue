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
        psm.eleAbilities.overlapssphereeledmg(psm.transform.gameObject, 2f, 15);
    }
}
