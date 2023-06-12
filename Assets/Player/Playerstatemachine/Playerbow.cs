using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerbow
{
    public Movescript psm;

    const string chargestate = "Chargearrow";
    const string aimholdstate = "Aimhold";
    const string releasearrowstate = "Releasearrow";

    public void bowoutofcombataimmovement()
    {
        float h = psm.move.x;                                                                         // Move Script
        float v = psm.move.y;

        psm.moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(psm.moveDirection.magnitude) * psm.attackmovementspeed;
        psm.moveDirection.Normalize();

        psm.moveDirection = Quaternion.AngleAxis(psm.CamTransform.rotation.eulerAngles.y, Vector3.up) * psm.moveDirection;                     //Kamera dreht sich mit dem Char
        psm.animator.SetFloat("AimX", psm.move.x, 0.05f, Time.deltaTime);
        psm.animator.SetFloat("AimZ", psm.move.y, 0.05f, Time.deltaTime);
        if (psm.moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(psm.moveDirection, Vector3.up);
            psm.transform.rotation = Quaternion.RotateTowards(psm.transform.rotation, toRotation, psm.attackrotationspeed * Time.deltaTime);
        }
        psm.velocity = psm.moveDirection * magnitude;
    }
    public void chargearrow()
    {
        if (psm.controlls.Player.Attack4.WasReleasedThisFrame())
        {
            psm.disableaimcam();
            Statics.otheraction = false;
            psm.switchtogroundstate();
        }
    }
    public void arrowfullcharge()
    {
        if (psm.Cam2.gameObject.activeSelf == true)                 //Wenn man den Input im selben moment los lässt wie der call kommt wird die kamera deakiviert aber man komm trotzdem in die state
        {
            psm.playersounds.stopsound();
            psm.ChangeAnimationState(aimholdstate);
            psm.state = Movescript.State.Outofcombatbowischarged;
        }
        else
        {
            psm.disableaimcam();
            Statics.otheraction = false;
            psm.switchtogroundstate();
        }

    }


    public void shootarrow()
    {
        if (psm.controlls.Player.Attack4.WasReleasedThisFrame())
        {
            psm.ChangeAnimationState(releasearrowstate);
            psm.state = Movescript.State.Outofcombatbowwaitfornewcharge;
        }
    }
    public void nextarrow()
    {
        if (psm.controlls.Player.Attack4.IsPressed())
        {
            psm.state = Movescript.State.Outofcombarbowcharge;
            psm.ChangeAnimationState(chargestate);
        }
        else     
        {
            psm.disableaimcam();
            Statics.otheraction = false;
            psm.switchtogroundstate();
        }
    }
    public void bowhookshot()
    {
        if (Movescript.lockontarget != null)
        {
            psm.transform.position = Vector3.MoveTowards(psm.transform.position, Movescript.lockontarget.position, 25 * Time.deltaTime);
            psm.transform.rotation = Quaternion.LookRotation(Movescript.lockontarget.transform.position - psm.transform.position, Vector3.up);
            if (Vector3.Distance(psm.transform.position, Movescript.lockontarget.position) < 2f)
            {
                Vector3 lookposi = Movescript.lockontarget.transform.position - psm.transform.position;
                lookposi.y = 0;
                psm.transform.rotation = Quaternion.LookRotation(lookposi);
                psm.switchtoairstate();
                Statics.otheraction = false;
            }
        }
        else
        {
            psm.switchtoairstate();
            Statics.otheraction = false;
        }
    }
}
