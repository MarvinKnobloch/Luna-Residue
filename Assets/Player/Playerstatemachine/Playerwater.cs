using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerwater
{
    public Movescript psm;
    private float waterpushbackmaxtime = 0.6f;

    const string waterkickstate = "Waterkick";
    public void waterpushback()
    {
        psm.waterpushbacktime += Time.deltaTime;
        if (Movescript.lockontarget != null)
        {
            if (psm.waterpushbacktime > waterpushbackmaxtime) psm.Abilitiesend();
            Vector3 newtransformposi = psm.transform.position;
            newtransformposi.y = psm.transform.position.y;
            Vector3 newlockonposi = Movescript.lockontarget.position;
            newlockonposi.y = psm.transform.position.y;
            Vector3 endposi = newlockonposi + (psm.transform.forward * -25);
            Vector3 distancetomove = endposi - newtransformposi;
            Vector3 move = distancetomove.normalized * 14 * Time.deltaTime;
            psm.charactercontroller.Move(move);
        }
        else
        {
            psm.Abilitiesend();
        }

    }
    public void waterpushbackdmg()
    {
        if (Movescript.lockontarget != null)
        {
            dealwaterdmg(Movescript.lockontarget.gameObject, 2, 10);
        }
    }
    /*public void startwaterkick()
    {
        psm.ChangeAnimationState(waterkickstate);
        psm.state = Movescript.State.Waterkick;
    }*/
    public void waterkickend()
    {
        if (Movescript.lockontarget != null)
        {
            Vector3 distancetomove = Movescript.lockontarget.position - psm.transform.position;
            Vector3 move = distancetomove.normalized * 16 * Time.deltaTime;
            psm.charactercontroller.Move(move);
            psm.transform.rotation = Quaternion.LookRotation(Movescript.lockontarget.transform.position - psm.transform.position, Vector3.up);
            if (Vector3.Distance(psm.transform.position, Movescript.lockontarget.position) < 3f)
            {
                dealwaterdmg(psm.transform.gameObject, 4, 10);
                if (Movescript.lockontarget != null)
                {
                    Vector3 lookPos = Movescript.lockontarget.transform.position - psm.transform.position;
                    lookPos.y = 0;
                    psm.transform.rotation = Quaternion.LookRotation(lookPos);
                    psm.Abilitiesend();
                }
            }
        }
        else psm.Abilitiesend();
    }
    public void dealwaterdmg(GameObject hitposi, float size, float dmg)
    {
        psm.eleAbilities.overlapssphereeledmg(hitposi, size, dmg);
    }

}
