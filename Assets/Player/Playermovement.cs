using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement
{
    public Movescript psm;

    const string idlestate = "Idle";
    const string runstate = "Running";
    const string jumpstate = "Jump";
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
    }
    public void jump()
    {
        if (LoadCharmanager.disableattackbuttons == false || LoadCharmanager.gameispaused == false)
        {
            if (psm.controlls.Player.Jump.WasPressedThisFrame())
            {
                psm.graviti = psm.jumpheight;
                psm.switchtoairstate();
                psm.ChangeAnimationState(jumpstate);
            }
        }
    }
    public void groundcheck()
    {
        if (Physics.SphereCast(psm.spherecastcollider.bounds.center, psm.spherecastcollider.radius, Vector3.down, out RaycastHit groundhit, 1.1f, psm.groundchecklayer))
        {
            psm.onground = true;
            float angle = Vector3.Angle(Vector3.up, groundhit.normal);
            if(angle > psm.charactercontroller.slopeLimit +6)          // +3 sonst die ganze zeit state wechsel wenn man gegen ein schräge läuft
            {
                Statics.otheraction = false;
                psm.Charrig.enabled = false;
                psm.disableaimcam();
                psm.graviti = -2;
                psm.state = Movescript.State.Slidedownwall;
            }
            else
            {
                psm.velocity = checkgroundangle(psm.velocity);
                psm.velocity.y += psm.graviti;
                psm.charactercontroller.Move(psm.velocity * Time.deltaTime);
            }
        }
        else
        {
            psm.switchtoairstate();
            Debug.Log("cant hit");
        }
    }
    public void groundanimations()
    {
        if (psm.moveDirection != Vector3.zero)
        {
            psm.ChangeAnimationState(runstate);
        }
        else
        {
            psm.ChangeAnimationState(idlestate);
        }
    }
    private Vector3 checkgroundangle(Vector3 velocity)
    {
        Ray groundray = new Ray(psm.transform.position + Vector3.up * 0.3f, Vector3.down);
        if (Physics.Raycast(groundray, out RaycastHit groundrayinfo, 1f))
        {
            Quaternion groundangle = Quaternion.FromToRotation(Vector3.up, groundrayinfo.normal);
            Vector3 velocityangle = groundangle * velocity;
            if (velocityangle.y < 0)
            {
                return velocityangle;
            }
        }
        return velocity;
    }
    public void beforedashmovement()
    {
        float h = psm.move.x;
        float v = psm.move.y;

        psm.moveDirection = new Vector3(h, 0, v);
        psm.moveDirection = Quaternion.AngleAxis(psm.CamTransform.rotation.eulerAngles.y, Vector3.up) * psm.moveDirection;
        if (psm.moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(psm.moveDirection, Vector3.up);
            psm.transform.rotation = Quaternion.RotateTowards(psm.transform.rotation, toRotation, 5000 * Time.deltaTime);
        }
    }
}
