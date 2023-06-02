using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerattack
{
    public Movescript psm;

    public void attackmovement()
    {
        if (Movescript.lockontarget != null)
        {
            Vector3 endposi = Movescript.lockontarget.transform.position + psm.transform.forward * -1.5f;
            Vector3 distancetomove = endposi - psm.transform.position;
            Vector3 movement = distancetomove.normalized * 0.5f;
            psm.velocity = movement;
        }
        else
        {
            psm.velocity = Vector3.zero;
        }
    }
    public void inputattackmovement()
    {
        float h = psm.move.x;                                                                         // Move Script
        float v = psm.move.y;

        psm.moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(psm.moveDirection.magnitude) * psm.attackmovementspeed;
        psm.moveDirection.Normalize();

        psm.moveDirection = Quaternion.AngleAxis(psm.CamTransform.rotation.eulerAngles.y, Vector3.up) * psm.moveDirection;                     //Kamera dreht sich mit dem Char

        if (psm.moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(psm.moveDirection, Vector3.up);
            psm.transform.rotation = Quaternion.RotateTowards(psm.transform.rotation, toRotation, psm.attackrotationspeed * Time.deltaTime);
        }
        psm.velocity = psm.moveDirection * magnitude;
    }
    public void finalairmovement()
    {
        psm.velocity.y = psm.graviti;
        psm.charactercontroller.Move(psm.velocity * Time.deltaTime);
    }
}


