using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerswim
{
    public Movescript psm;

    const string swimstate = "Swim";
    const string swimidlestate = "Swimidle";
    public void swim()
    {
        if (psm.moveDirection.x != 0 && psm.moveDirection.z != 0)
        {
            psm.ChangeAnimationState(swimstate);
            Ray ray = new Ray(psm.spine.transform.position, Vector3.up);
            if (Physics.Raycast(ray, out RaycastHit hit, 5f, psm.swimlayer))
            {
                float distance = hit.distance;
                if (distance < 0.2)
                {
                    psm.velocity.y = 0;
                }
                else
                {
                    psm.velocity.y = 0.3f;
                }
            }
            else
            {
                psm.velocity.y = -0.3f;
            }
        }
        else
        {
            psm.ChangeAnimationState(swimidlestate);
            Ray ray = new Ray(psm.spine.transform.position, Vector3.up);
            if (Physics.Raycast(ray, out RaycastHit hit, 5f, psm.swimlayer))
            {
                float distance = hit.distance;
                if(distance < 0.2)
                {
                    psm.velocity.y = -0.4f;
                }
                else if(distance < 0.4)
                {
                    psm.velocity.y = 0;
                }
                else
                {
                    psm.velocity.y = 0.4f;
                }
            }
            else
            {
                psm.velocity.y = -0.4f;
            }
        }
        psm.charactercontroller.Move(psm.velocity * Time.deltaTime);
    }
}
