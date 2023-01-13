using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerair
{
    public Movescript psm;

    const string fallstate = "Fall";
    public void airgravity()
    {
        float grav = Physics.gravity.y * psm.gravitation;
        psm.graviti += grav * Time.deltaTime;
        if(psm.graviti < -3)
        {
            psm.ChangeAnimationState(fallstate);
            if (psm.graviti < -15)
            {
                psm.graviti = -15;
            }
        }
        if (psm.charactercontroller.isGrounded == true && psm.graviti < 0)
        {
            psm.graviti = -0.5f;
            psm.state = Movescript.State.Ground;
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
}
