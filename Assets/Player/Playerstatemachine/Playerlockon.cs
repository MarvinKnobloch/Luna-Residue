using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlockon
{
    public Movescript psm;
    //private int targetnumber;

    public void whilelockon()
    {
        if (Statics.infight == true)
        {
            if (Movescript.lockontarget != null)
            {
                if (psm.controlls.Player.Lockonchange.WasPerformedThisFrame()) changetarget();
                if (psm.controlls.Player.Setalliestarget.WasPerformedThisFrame()) setsupporttargets();
                if (psm.controlls.Player.Character3target.WasPerformedThisFrame()) setthirdchartarget();
                if (psm.controlls.Player.Character4target.WasPerformedThisFrame()) setforthchartarget();
            }
        }
    }
    public void autolockon()
    {
        settarget();
        psm.focustargetui.SetActive(true);
    }
    public void settarget()
    {
        if (Infightcontroller.infightenemylists.Count != 0)
        {
            Infightcontroller.instance.currenttargetnumber = 0;
            Movescript.lockontarget = Infightcontroller.infightenemylists[Infightcontroller.instance.currenttargetnumber].gameObject.transform;
            Movescript.lockontarget.GetComponent<EnemyHP>().enemyisfocustarget();
            Movescript.lockontarget.GetComponent<EnemyHP>().marktarget();
        }
    }
    public void changetarget()
    {
        if (Infightcontroller.infightenemylists.Count > 1)
        {
            Movescript.lockontarget.GetComponent<EnemyHP>().unmarktarget();
            Movescript.lockontarget.GetComponent<EnemyHP>().enemyisnotfocustarget();

            if (Infightcontroller.instance.currenttargetnumber >= Infightcontroller.infightenemylists.Count - 1) Infightcontroller.instance.currenttargetnumber = 0;
            else Infightcontroller.instance.currenttargetnumber++;
            Movescript.lockontarget = Infightcontroller.infightenemylists[Infightcontroller.instance.currenttargetnumber].gameObject.transform;

            Movescript.lockontarget.GetComponent<EnemyHP>().enemyisfocustarget();
            Movescript.lockontarget.GetComponent<EnemyHP>().marktarget();
        }
    }
    public void lockonfindclostesttarget()
    {
        if (Infightcontroller.infightenemylists.Count != 0)
        {
            float shortestDistance = 100f;
            for (int i = 0; i < Infightcontroller.infightenemylists.Count; i++)
            {
                float distancefromtarget = Vector3.Distance(psm.transform.position, Infightcontroller.infightenemylists[i].transform.position);
                if (distancefromtarget < shortestDistance)
                {
                    Infightcontroller.instance.currenttargetnumber = i;
                    Movescript.lockontarget = Infightcontroller.infightenemylists[Infightcontroller.instance.currenttargetnumber].gameObject.transform;
                    shortestDistance = distancefromtarget;
                }
            }
            Movescript.lockontarget.GetComponent<EnemyHP>().enemyisfocustarget();
            Movescript.lockontarget.GetComponent<EnemyHP>().marktarget();
        }
    }

    public void endlockon()
    {
        if(Movescript.lockontarget != null)
        {
            Movescript.lockontarget.GetComponent<EnemyHP>().enemyisnotfocustarget();
            Movescript.lockontarget.GetComponent<EnemyHP>().unmarktarget();
            Movescript.lockontarget = null;
        }
        psm.focustargetui.SetActive(false);
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
    private void setsupporttargets()
    {
        if (LoadCharmanager.Overallthirdchar != null)
        {
            LoadCharmanager.Overallthirdchar.GetComponent<Supportmovement>().playerfocustarget();
        }
        if (LoadCharmanager.Overallforthchar != null)
        {
            LoadCharmanager.Overallforthchar.GetComponent<Supportmovement>().playerfocustarget();
        }      
    }
    private void setthirdchartarget()
    {
        if (LoadCharmanager.Overallthirdchar != null)
        {
            LoadCharmanager.Overallthirdchar.GetComponent<Supportmovement>().playerfocustarget();
        }
    }
    private void setforthchartarget()
    {
        if (LoadCharmanager.Overallforthchar != null)
        {
            LoadCharmanager.Overallforthchar.GetComponent<Supportmovement>().playerfocustarget();
        }
    }
}

/*private void changetarget()
{
    if (Infightcontroller.infightenemylists.Count > 1)
    {
        psm.targetbeforeswap = Movescript.lockontarget;
        Movescript.lockontarget.GetComponent<EnemyHP>().unmarktarget();
        Movescript.lockontarget.GetComponent<EnemyHP>().enemyisnotfocustarget();

        float shortestDistance = 100f;
        for (int i = 0; i < Infightcontroller.infightenemylists.Count; i++)
        {
            float distancefromtarget = Vector3.Distance(psm.transform.position, Infightcontroller.infightenemylists[i].transform.position);
            if (distancefromtarget < shortestDistance)
            {
                if (psm.targetbeforeswap.gameObject != Infightcontroller.infightenemylists[i])
                {
                    Movescript.lockontarget = Infightcontroller.infightenemylists[i].gameObject.transform;
                    shortestDistance = distancefromtarget;
                }
            }
        }
        Movescript.lockontarget.GetComponent<EnemyHP>().enemyisfocustarget();
        Movescript.lockontarget.GetComponent<EnemyHP>().marktarget();
    }
}*/