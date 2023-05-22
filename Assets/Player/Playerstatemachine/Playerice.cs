using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerice
{
    public Movescript psm;

    const string icelancebackflipstate = "Icelancebackflip";
    public void icelanceplayermovement()
    {
        if (Movescript.lockontarget != null)
        {
            psm.graviti = 0f;
            psm.transform.position = Vector3.MoveTowards(psm.transform.position, psm.transform.position + Vector3.up, 1 * Time.deltaTime);
        }
        else
        {
            psm.Abilitiesend();
        }
    }
    public void starticelancefly()
    {
        if (psm.state != Movescript.State.Icelancestart) return;
        if (Movescript.lockontarget != null)
        {
            psm.state = Movescript.State.Icelancefly;
        }
        else
        {
            psm.Abilitiesend();
        }
    }
    public void icelanceplayertotarget()
    {
        if (Movescript.lockontarget != null)
        {
            if (Vector3.Distance(psm.transform.position, Movescript.lockontarget.position) > 5f)         //startet die animation und bewegt den player noch nach vorne
            {
                psm.ChangeAnimationState(icelancebackflipstate);
            }
            if (Vector3.Distance(psm.transform.position, Movescript.lockontarget.position) > 3f)
            {
                psm.transform.position = Vector3.MoveTowards(psm.transform.position, Movescript.lockontarget.position, psm.icelancespeed * Time.deltaTime);
            }
            else
            {
                psm.ChangeAnimationState(icelancebackflipstate);
                psm.state = Movescript.State.Empty;
            }
        }
        else
        {
            psm.Abilitiesend();
        }
    }
    public void icelanceend()
    {
        if (psm.state == Movescript.State.Empty || psm.state == Movescript.State.Icelancefly)
        {
            psm.Abilitiesend();
        }
    }
}
