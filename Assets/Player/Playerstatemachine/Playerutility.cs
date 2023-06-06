using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerutility
{
    public Movescript psm;

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
        if (LoadCharmanager.Overallthirdchar != null && LoadCharmanager.Overallthirdchar.activeSelf == false)
        {
            if(Physics.Raycast(psm.transform.position, psm.transform.forward * -1 + psm.transform.right * -1, out RaycastHit hit, 1, psm.groundchecklayer, QueryTriggerInteraction.Ignore))
            {
                LoadCharmanager.Overallthirdchar.transform.position = hit.point;
                LoadCharmanager.Overallthirdchar.SetActive(true);
            }
            else
            {
                LoadCharmanager.Overallthirdchar.transform.position = psm.transform.position + psm.transform.forward * -1 + psm.transform.right * -1;
                LoadCharmanager.Overallthirdchar.SetActive(true);
            }
        }
        if (LoadCharmanager.Overallforthchar != null && LoadCharmanager.Overallforthchar.activeSelf == false)
        {
            if (Physics.Raycast(psm.transform.position, psm.transform.forward * -1 + psm.transform.right * 1, out RaycastHit hit, 1, psm.groundchecklayer, QueryTriggerInteraction.Ignore))
            {
                LoadCharmanager.Overallforthchar.transform.position = hit.point;
                LoadCharmanager.Overallforthchar.SetActive(true);
            }
            else
            {
                LoadCharmanager.Overallforthchar.transform.position = psm.transform.position + psm.transform.forward * -1 + psm.transform.right * 1;
                LoadCharmanager.Overallforthchar.SetActive(true);
            }
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
        if(Statics.bonusdashcantrigger == true)
        {
            Statics.bonusdashcantrigger = false;
            Statics.bonuscritstacks = 0;
            psm.bonuscritstackstext.text = Statics.bonuscritstacks.ToString();
            Collider[] cols = Physics.OverlapSphere(psm.transform.position, 10, psm.spellsdmglayer);
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
        finaldashdmg = Globalplayercalculations.dashexplosion(psm.attributecontroller.critdmg, psm.attributecontroller.critchance);
    }
}
