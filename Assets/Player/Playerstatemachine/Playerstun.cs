using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerstun
{
    public Movescript psm;
    
    public void stun()
    {
        psm.gravitation = psm.normalgravition;
        if (psm.charactercontroller.isGrounded)
        {
            psm.graviti = -0.5f;
        }
        else
        {
            float grav = Physics.gravity.y * psm.gravitation;
            psm.graviti += grav * Time.deltaTime;
        }
        psm.velocity = new Vector3(0, psm.graviti, 0);
        psm.charactercontroller.Move(psm.velocity * Time.deltaTime);
    }
    public void breakstunwithbuttonmash()
    {
        if (psm.controlls.Player.Attack3.WasPerformedThisFrame())
        {
            Statics.dazecounter += 1;
        }
        if (Statics.dazecounter >= Statics.dazekicksneeded)
        {
            psm.dazeimage.SetActive(false);
            Statics.dash = false;
            Statics.otheraction = false;
            psm.switchtogroundstate();
        }
    }
}
