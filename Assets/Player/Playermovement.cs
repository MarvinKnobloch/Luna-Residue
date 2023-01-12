using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement
{
    public Movescript psm;

    public void movement()
    {
        float h = psm.move.x;
        float v = psm.move.y;

        psm.moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(psm.moveDirection.magnitude) * psm.movementspeed;
        psm.moveDirection.Normalize();

        psm.moveDirection = Quaternion.AngleAxis(psm.CamTransform.rotation.eulerAngles.y, Vector3.up) * psm.moveDirection;

        if (psm.moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(psm.moveDirection, Vector3.up);
            psm.transform.rotation = Quaternion.RotateTowards(psm.transform.rotation, toRotation, psm.rotationspeed * Time.deltaTime);
        }
        psm.velocity = psm.moveDirection * magnitude;
        psm.velocity.y = psm.graviti;
        psm.charactercontroller.Move(psm.velocity * Time.deltaTime);
    }
    public void jump()
    {
        if (psm.controlls.Player.Jump.WasPressedThisFrame())
        {
            psm.graviti = psm.jumpheight;
            psm.state = Movescript.State.Air;
        }
    }
    public void airgravity()
    {
        float grav = Physics.gravity.y * psm.gravitation;
        psm.graviti += grav * Time.deltaTime;
        if (psm.graviti < -15)
        {
            psm.graviti = -15;
        }
        if (psm.charactercontroller.isGrounded == true && psm.graviti < 0)
        {
            psm.graviti = -0.5f;
            psm.state = Movescript.State.Ground;
        }
    }
    public void groundcheck()
    {

    }
}
