using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlockon
{
    public Movescript psm;

    //wenn man aus der range der healthbar läuft wird sie deaktiviert obwohl man noch infight ist, danach ist die focus anzeige + mark/unmark target verbugt
    public void whilelockon()
    {
        if (Statics.infight == true)
        {
            if (Movescript.lockontarget != null)
            {
                if (psm.controlls.Player.Lockonchange.WasPerformedThisFrame()) changetarget();
                if (psm.controlls.Player.Setsupporttarget.WasPerformedThisFrame()) setsupporttargets();
            }
        }
    }
    public void autolockon()
    {
        lockonfindclostesttarget();
        psm.focustargetui.SetActive(true);
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
                    Movescript.lockontarget = Infightcontroller.infightenemylists[i].gameObject.transform;
                    shortestDistance = distancefromtarget;
                }
            }
            Movescript.lockontarget.GetComponent<EnemyHP>().enemyisfocustarget();
            Movescript.lockontarget.GetComponent<EnemyHP>().marktarget();
        }
    }

    private void changetarget()
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
}
