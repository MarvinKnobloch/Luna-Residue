using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations;

public class AimScript : MonoBehaviour
{
    /*private SpielerSteu controlls;
    public Transform Kamerarichtung;

    //aim
    private float xrotation = 0f;
    public float aimrotationgesch;
    public CinemachineFreeLook Cam1;
    public CinemachineVirtualCamera Cam2;
    public bool virtualcam;

    public GameObject mousetarget;
    void Awake()
    {
        mousetarget.SetActive(false);
        controlls = new SpielerSteu();
    }
    private void OnEnable()
    {
        controlls.Enable();
    }
    void Update()
    {
        if (virtualcam == true)
        {
            Cam2.transform.rotation = transform.rotation;
            //float Kamerarotationoffset = Kamerarichtung.eulerAngles.y + 10f;
            //Quaternion kamerarotation = Quaternion.Euler(0, Kamerarotationoffset, 0);
            //transform.rotation = Quaternion.Lerp(transform.rotation, kamerarotation, aimrotationgesch * Time.deltaTime);
            aimrotation(controlls.Player.Mouse.ReadValue<Vector2>());
        }
    }
    public void activateaimcam()
    {
        mousetarget.SetActive(true);
        Cam2.gameObject.SetActive(true);
        CinemachinePOV Cam2pov = Cam2.GetCinemachineComponent<CinemachinePOV>();
        Cam2pov.m_HorizontalRecentering.m_enabled = true;
        Cam1.m_RecenterToTargetHeading.m_enabled = true;
        Invoke("cam2recenter", 0.2f);
        virtualcam = true;
    }
    private void aimrotation(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xrotation -= (mouseY * Time.deltaTime);                                  // *y rotation geschwindigkeit, hat aber nicht funktioniert, habe den Wert bei der Kamera einstellung geändert
        xrotation = Mathf.Clamp(xrotation, -80f, 80f);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime));                // *x rotation geschwindigkeit, hat aber nicht funktioniert, habe den Wert bei der Kamera einstellung geändert
    }
    public void aimend()
    {
        CancelInvoke();
        virtualcam = false;
        mousetarget.SetActive(false);
        Cam1.m_RecenterToTargetHeading.m_enabled = false;
        Cam2.gameObject.SetActive(false);
    }
    private void cam2recenter()
    {
        CinemachinePOV Cam2pov = Cam2.GetCinemachineComponent<CinemachinePOV>();
        Cam2pov.m_HorizontalRecentering.m_enabled = false;
    }*/
}
