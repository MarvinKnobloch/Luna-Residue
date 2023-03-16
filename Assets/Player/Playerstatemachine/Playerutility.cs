using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerutility
{
    public Movescript psm;

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
}
