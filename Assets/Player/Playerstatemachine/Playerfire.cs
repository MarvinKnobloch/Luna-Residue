using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Playerfire
{
    public Movescript psm;

    const string firedashstate = "Firedash";
    public void firedashstartmovement()
    {
        /*float h = psm.move.x;
        float v = psm.move.y;

        psm.moveDirection = new Vector3(h, 0, v);
        psm.moveDirection.Normalize();

        psm.moveDirection = Quaternion.AngleAxis(psm.CamTransform.rotation.eulerAngles.y, Vector3.up) * psm.moveDirection;

        if (psm.moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(psm.moveDirection, Vector3.up);
            psm.transform.rotation = Quaternion.RotateTowards(psm.transform.rotation, toRotation, psm.rotationspeed * Time.deltaTime);
        }*/
    }
    public void firedashstart()
    {
        if (Movescript.lockontarget != null)
        {
            Vector3 lookPos = Movescript.lockontarget.transform.position - psm.transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            psm.transform.rotation = Quaternion.Slerp(psm.transform.rotation, rotation, 100 * Time.deltaTime);
        }

        if (psm.state != Movescript.State.Firedashstart) return;
        psm.ChangeAnimationState(firedashstate);
        Physics.IgnoreLayerCollision(8, 6);
        Physics.IgnoreLayerCollision(11, 6);
        //Physics.IgnoreLayerCollision(15, 6);
        psm.state = Movescript.State.Firedash;
    }
    public void firedash()
    {
        Vector3 endposi = psm.transform.position + (psm.transform.forward * 20);
        Vector3 distancetomove = endposi - psm.transform.position;
        Vector3 move = distancetomove.normalized * 40 * Time.deltaTime;
        psm.charactercontroller.Move(move);
    }
    public void firedashdmg()
    {
        psm.eleAbilities.overlapssphereeledmg(psm.transform.gameObject, 2, 5);
    }
    public void firedashend()
    {
        if (psm.state != Movescript.State.Firedash) return;
        psm.Abilitiesend();
    }
}
