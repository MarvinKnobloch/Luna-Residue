using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerslidewalls
{
    public Movescript psm;

    const string fallstate = "Fall";
    public void slidewalls()
    {
        if (Physics.SphereCast(psm.spherecastcollider.bounds.center, psm.spherecastcollider.radius, Vector3.down, out RaycastHit groundhit, 1.1f, psm.groundchecklayer))
        //if (Physics.BoxCast(boxCollider.bounds.center, boxCollider.bounds.extents, Vector3.down, out RaycastHit groundhit, Quaternion.identity, 1.1f, groundchecklayer))
        {
            float grav = Physics.gravity.y * psm.gravitation;
            psm.graviti += grav * Time.deltaTime;
            if (psm.graviti < psm.maxgravity)
            {
                psm.graviti = psm.maxgravity;
            }
            psm.ChangeAnimationState(fallstate);
            float angle = Vector3.Angle(Vector3.up, groundhit.normal);
            if (angle > 89.9)
            {
                psm.switchtoairstate();
            }
            else if (angle > psm.charactercontroller.slopeLimit)
            {
                psm.moveDirection = Vector3.ProjectOnPlane(new Vector3(0, psm.graviti, 0), groundhit.normal);
            }
            else
            {
                psm.switchtogroundstate();
            }
            psm.charactercontroller.Move(psm.moveDirection * Time.deltaTime);
        }
        else
        {
            psm.charactercontroller.Move(Vector3.zero);
            psm.switchtoairstate();
            Debug.Log("cant hit");
        }
    }
}
