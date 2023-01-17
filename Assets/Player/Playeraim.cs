using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Playeraim
{
    public Movescript psm;
    public void aimplayerrotation()
    {
        psm.Cam2.transform.rotation = psm.transform.rotation;
        float Kamerarotationoffset = psm.CamTransform.eulerAngles.y + 10f;
        Quaternion kamerarotation = Quaternion.Euler(0, Kamerarotationoffset, 0);
        psm.transform.rotation = Quaternion.Lerp(psm.transform.rotation, kamerarotation, psm.aimrotationspeed * Time.deltaTime);
        aimrotation(psm.controlls.Player.Mouse.ReadValue<Vector2>());
    }
    private void aimrotation(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        psm.xrotation -= (mouseY * Time.deltaTime);                                  // *y rotation geschwindigkeit, hat aber nicht funktioniert, habe den Wert bei der Kamera einstellung geändert
        psm.xrotation = Mathf.Clamp(psm.xrotation, -80f, 80f);
        psm.transform.Rotate(Vector3.up * (mouseX * Time.deltaTime));                // *x rotation geschwindigkeit, hat aber nicht funktioniert, habe den Wert bei der Kamera einstellung geändert
    }
    public void activateaimcam()
    {
        psm.mousetarget.SetActive(true);
        psm.Cam2.gameObject.SetActive(true);
        //CinemachinePOV Cam2pov = psm.Cam2.GetCinemachineComponent<CinemachinePOV>();
        //Cam2pov.m_HorizontalRecentering.m_enabled = true;
        //psm.Cam1.m_RecenterToTargetHeading.m_enabled = true;
        //Invoke("cam2recenter", 0.2f);
    }
    public void aimend()
    {
        psm.mousetarget.SetActive(false);
        //Cam1.m_RecenterToTargetHeading.m_enabled = false;
        psm.Cam2.gameObject.SetActive(false);
    }
    private void cam2recenter()
    {
        //CinemachinePOV Cam2pov = psm.Cam2.GetCinemachineComponent<CinemachinePOV>();
        //Cam2pov.m_HorizontalRecentering.m_enabled = false;
    }
}
