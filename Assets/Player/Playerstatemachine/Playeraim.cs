using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Playeraim
{
    public Movescript psm;
    public void aimplayerrotation()
    {
        float camerarotationoffset = psm.CamTransform.eulerAngles.y + 10f;
        Quaternion camerarotation = Quaternion.Euler(0, camerarotationoffset, 0);
        psm.transform.rotation = Quaternion.Lerp(psm.transform.rotation, camerarotation, psm.aimrotationspeed * Time.deltaTime);
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
    }
}
