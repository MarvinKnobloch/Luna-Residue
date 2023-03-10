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
}
