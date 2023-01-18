using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlockon
{
    public Movescript psm;
    public void charlockon()
    {
        if (LoadCharmanager.disableattackbuttons == false || LoadCharmanager.gameispaused == false)
        {
            if (psm.controlls.Player.Lockon.WasPerformedThisFrame() && Movescript.lockoncheck == false) lookfortarget();
            else if (psm.controlls.Player.Lockon.WasPerformedThisFrame() && Movescript.lockoncheck == true)         //beendet lockon durch buttonpress
            {
                if (Movescript.lockontarget != null)
                {
                    Movescript.lockontarget.GetComponent<EnemyHP>().focustargetuiend();
                    Movescript.lockontarget.GetComponent<EnemyHP>().unmarktarget();
                }
                Movescript.lockoncheck = false;
                Movescript.availabletargets.Clear();
                //cancellockon = false;
                Movescript.lockontarget = null;
            }

            if (psm.controlls.Player.Lockonchange.WasPerformedThisFrame() && Movescript.lockoncheck == true)          //target wechsel per button
            {
                psm.Checkforenemy = Physics.CheckSphere(psm.transform.position, psm.lockonrange, psm.Lockonlayer);
                if (psm.Checkforenemy == true)
                {
                    float shortestDistance = 100f;
                    addenemystolist();
                    if (Movescript.lockontarget != null)
                    {
                        psm.targetbeforeswap = Movescript.lockontarget;
                        Movescript.lockontarget.GetComponent<EnemyHP>().unmarktarget();
                        Movescript.lockontarget.GetComponent<EnemyHP>().focustargetuiend();
                    }
                    for (int t = 0; t < Movescript.availabletargets.Count; t++)
                    {
                        float distancefromtarget = Vector3.Distance(psm.transform.position, Movescript.availabletargets[t].transform.position);
                        if (distancefromtarget < shortestDistance)
                        {
                            if (psm.targetbeforeswap == Movescript.availabletargets[t].lockontransform)
                            {
                                continue;
                            }
                            else
                            {
                                Movescript.lockontarget = Movescript.availabletargets[t].lockontransform;
                                shortestDistance = distancefromtarget;
                            }
                        }
                    }
                    Movescript.lockontarget.GetComponent<EnemyHP>().focustargetuistart();
                    Movescript.lockontarget.GetComponent<EnemyHP>().marktarget();
                }
                else
                {
                    Movescript.lockoncheck = false;                   //für autolockon nach death oder bowintogroundattack
                    Movescript.lockontarget = null;
                }
            }
            if (Movescript.lockontarget != null && Movescript.lockoncheck == true)                                                                     //sucht neues target wenn momentanes target außer lockonrange ist
            {
                if (Vector3.Distance(psm.transform.position, Movescript.lockontarget.transform.position) > psm.lockonrange)
                {
                    Movescript.lockontarget.GetComponent<EnemyHP>().focustargetuiend();
                    Movescript.lockontarget.GetComponent<EnemyHP>().unmarktarget();
                    Movescript.availabletargets.Clear();

                    float shortestDistance = 100f;
                    addenemystolist();
                    if (Movescript.availabletargets.Count >= 1)
                    {
                        for (int t = 0; t < Movescript.availabletargets.Count; t++)
                        {
                            float distancefromtarget = Vector3.Distance(psm.transform.position, Movescript.availabletargets[t].transform.position);
                            if (distancefromtarget < shortestDistance)
                            {
                                Movescript.lockontarget = Movescript.availabletargets[t].lockontransform;
                                shortestDistance = distancefromtarget;
                            }
                        }
                        Movescript.lockontarget.GetComponent<EnemyHP>().focustargetuistart();
                        Movescript.lockontarget.GetComponent<EnemyHP>().marktarget();
                        Movescript.lockoncheck = true;
                    }
                    else
                    {
                        Movescript.lockoncheck = false;
                        Movescript.availabletargets.Clear();
                        Movescript.lockontarget = null;
                    }
                }
            }
        }
    }
    public void lookfortarget()
    {
        psm.Checkforenemy = Physics.CheckSphere(psm.transform.position, psm.lockonrange, psm.Lockonlayer);
        if (psm.Checkforenemy == true)
        {
            if (Movescript.lockontarget != null)
            {
                Movescript.lockontarget.GetComponent<EnemyHP>().focustargetuiend();
                Movescript.lockontarget.GetComponent<EnemyHP>().unmarktarget();
            }
            float shortestDistance = 100f;
            addenemystolist();
            for (int t = 0; t < Movescript.availabletargets.Count; t++)
            {
                float distancefromtarget = Vector3.Distance(psm.transform.position, Movescript.availabletargets[t].transform.position);
                if (distancefromtarget < shortestDistance)
                {
                    Movescript.lockontarget = Movescript.availabletargets[t].lockontransform;
                    shortestDistance = distancefromtarget;
                }
            }
            Movescript.lockontarget.GetComponent<EnemyHP>().focustargetuistart();
            Movescript.lockontarget.GetComponent<EnemyHP>().marktarget();
            Movescript.lockoncheck = true;
        }
        else
        {
            Movescript.availabletargets.Clear();
            Movescript.lockoncheck = false;                   //für autolockon nach death oder bowintogroundattack
            Movescript.lockontarget = null;
        }
    }
    private void addenemystolist()
    {
        Collider[] colliders = Physics.OverlapSphere(psm.transform.position, psm.lockonrange);

        for (int i = 0; i < colliders.Length; i++)
        {
            psm.Enemylistcollider = colliders[i].GetComponent<Enemylockon>();
            if (psm.Enemylistcollider != null)
            {
                if (!Movescript.availabletargets.Contains(psm.Enemylistcollider))
                {
                    Movescript.availabletargets.Add(psm.Enemylistcollider);
                }
            }
        }
    }
    public void attacklockonrotation()
    {
        if (Movescript.lockontarget != null && Movescript.lockoncheck == true)
        {
            Vector3 lookPos = Movescript.lockontarget.transform.position - psm.transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            psm.transform.rotation = Quaternion.Slerp(psm.transform.rotation, rotation, psm.lockonrotationspeed  * Time.deltaTime);
        }
    }
}
