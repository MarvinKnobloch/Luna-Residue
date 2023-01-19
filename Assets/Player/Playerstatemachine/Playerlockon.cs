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
            if(Movescript.lockontarget == null)
            {
                if (psm.controlls.Player.Lockon.WasPerformedThisFrame()) lookfortarget();
            }
            else
            {
                if (psm.controlls.Player.Lockonchange.WasPerformedThisFrame()) changetarget();
                else if(psm.controlls.Player.Lockon.WasPerformedThisFrame()) endlockon();
                else currentlockonoutofrange();
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
            Movescript.availabletargets.Clear();
            addenemystolist();
            findclosestenemy();

        }
        else
        {
            if(Movescript.lockontarget != null)
            {
                endlockon();
            }
        }
    }
    private void changetarget()
    {
        psm.Checkforenemy = Physics.CheckSphere(psm.transform.position, psm.lockonrange, psm.Lockonlayer);            //macht nicht unbedint sinn, aber zur sicherheit
        if (psm.Checkforenemy == true)
        {
            psm.targetbeforeswap = Movescript.lockontarget;
            Movescript.lockontarget.GetComponent<EnemyHP>().unmarktarget();
            Movescript.lockontarget.GetComponent<EnemyHP>().focustargetuiend();
            Movescript.availabletargets.Clear();
            addenemystolist();
            float shortestDistance = 100f;
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
            endlockon();
        }
    }
    private void currentlockonoutofrange()
    {
        if (Vector3.Distance(psm.transform.position, Movescript.lockontarget.transform.position) > psm.lockonrange)
        {
            psm.Checkforenemy = Physics.CheckSphere(psm.transform.position, psm.lockonrange, psm.Lockonlayer);
            if (psm.Checkforenemy == true)
            {
                Movescript.lockontarget.GetComponent<EnemyHP>().focustargetuiend();
                Movescript.lockontarget.GetComponent<EnemyHP>().unmarktarget();
                Movescript.availabletargets.Clear();                               //damit alle enemies aus der liste raus sind, vll sind ja die anderem auch out of range geloffen

                addenemystolist();
                findclosestenemy();
            }
            else
            {
                endlockon();
            }
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
    private void findclosestenemy()
    {
        float shortestDistance = 100f;
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
    }
    private void endlockon()
    {
        Movescript.lockontarget.GetComponent<EnemyHP>().focustargetuiend();
        Movescript.lockontarget.GetComponent<EnemyHP>().unmarktarget();
        Movescript.availabletargets.Clear();
        Movescript.lockontarget = null;
    }
    public void attacklockonrotation()
    {
        if (Movescript.lockontarget != null)
        {
            Vector3 lookPos = Movescript.lockontarget.transform.position - psm.transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            psm.transform.rotation = Quaternion.Slerp(psm.transform.rotation, rotation, psm.lockonrotationspeed  * Time.deltaTime);
        }
    }

}
