using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerutility
{
    public Movescript psm;

    private float dashdmg;
    private float finaldashdmg;
    const string gatherstate = "Gatherobject";

    public void gatheritem(GameObject gatherobject)
    {
        Vector3 neededrotation = gatherobject.transform.position - psm.transform.position;
        neededrotation.y = 0;
        Quaternion rotation = Quaternion.LookRotation(neededrotation);
        psm.transform.rotation = Quaternion.Slerp(psm.transform.rotation, rotation, 100 * Time.deltaTime);
        psm.ChangeAnimationState(gatherstate);
        psm.state = Movescript.State.Gatheritem;
    }
    public void spawnallies()
    {
        if (LoadCharmanager.Overallthirdchar != null)
        {
            if (LoadCharmanager.Overallthirdchar.activeSelf == false) spawnteammates(LoadCharmanager.Overallthirdchar);
            else LoadCharmanager.Overallthirdchar.GetComponent<Supportmovement>().checkforpath();
        }
        if (LoadCharmanager.Overallforthchar != null)
        {
            if (LoadCharmanager.Overallforthchar.activeSelf == false) spawnteammates(LoadCharmanager.Overallforthchar);
            else LoadCharmanager.Overallforthchar.GetComponent<Supportmovement>().checkforpath();
        }
    }
    public void spawnteammates(GameObject teammate)
    {
        teammate.SetActive(false);                   //checkforpath()
        if (Physics.Raycast(psm.transform.position, psm.transform.forward * -1 + psm.transform.right * 1, out RaycastHit hit, 1, psm.groundchecklayer, QueryTriggerInteraction.Ignore))
        {
            teammate.transform.position = hit.point;
            teammate.SetActive(true);
        }
        else
        {
            teammate.transform.position = psm.transform.position + psm.transform.forward * -1 + psm.transform.right * 1;
            teammate.SetActive(true);
        }
    }
    public void checkspellmaxtime()
    {
        psm.spelltimer += Time.deltaTime;
        if(psm.spelltimer > psm.spellmaxtime)
        {
            psm.Abilitiesend();
        }
    }
    public void bonusdashexplosiondmg()
    {
        if(Statics.bonuscritstacksbool == true && Statics.bonuscritstacks > 0)
        {
            finaldashdmg = dashdmg * Statics.bonuscritstacks;
            Statics.bonuscritstacks = 0;
            psm.bonuscritstackstext.text = Statics.bonuscritstacks.ToString();
            Collider[] cols = Physics.OverlapSphere(psm.transform.position, 15, psm.spellsdmglayer);
            foreach (Collider enemyhit in cols)
            {
                if (enemyhit.isTrigger)               //damit nur die meleehitbox getriggered wird
                {
                    if (enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
                    {
                        enemyscript.takeplayerdamage(finaldashdmg, 0, false);
                    }
                }
            }
        }
    }
    public void calculatedashdmg()
    {
        dashdmg = Globalplayercalculations.dashexplosion(psm.attributecontroller.critdmg, psm.attributecontroller.critchance);
    }
}
