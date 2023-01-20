using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerair
{
    public Movescript psm;

    const string jumpstate = "Jump";
    const string fallstate = "Fall";
    public void airgravity()
    {
        float grav = Physics.gravity.y * psm.gravitation;
        psm.graviti += grav * Time.deltaTime;
        if (psm.graviti < psm.maxgravity)
        {
            psm.graviti = psm.maxgravity;
        }
        psm.velocity.y = psm.graviti;
        psm.charactercontroller.Move(psm.velocity * Time.deltaTime);
    }

    public void switchformupwardstoair()
    {
        if(psm.graviti <= 0) psm.switchtoairstate();
    }
    public void airdownwards()
    {
        if (psm.graviti < -3)
        {
            psm.ChangeAnimationState(fallstate);
        }
        if (psm.charactercontroller.isGrounded == true && psm.graviti < 0)
        {
            psm.switchtogroundstate();
        }
    }
    public void minheightforairattack()
    {
        Ray ray = new Ray(psm.transform.position + Vector3.up * 0.3f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.8f))
        {
            psm.airattackminheight = false;
        }
        else
        {
            psm.airattackminheight = true;
        }
    }
    public void jump()
    {
        if (LoadCharmanager.disableattackbuttons == false || LoadCharmanager.gameispaused == false)
        {
            if (psm.controlls.Player.Jump.WasPressedThisFrame())
            {
                pushplayerupwards(psm.jumpheight);
            }
        }
    }
    public void pushplayerupwards(float upwardsmomentum)
    {
        psm.graviti = upwardsmomentum;
        psm.state = Movescript.State.Upwards;
        psm.ChangeAnimationState(jumpstate);
    }
}
