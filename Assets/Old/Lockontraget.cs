using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Lockontraget : MonoBehaviour
{
    /*private SpielerSteu Steuerung;

    public static bool lockoncheck;
    private bool Checkforenemy;
    [SerializeField] private float lockonrange;
    [SerializeField] private LayerMask Lockonlayer;
    //public static Transform lockontarget;
    public Transform Lockontargetobject;
    public GameObject LockonUI;
    public Enemylockon Enemylistcollider;
    public static List<Enemylockon> availabletargets = new List<Enemylockon>();
    public CinemachineFreeLook Cam1;
    public Transform Spielertransform;
    public float rotationgesch;
    public bool cancellockon;
    void Awake()
    {
        LockonUI.SetActive(false);
        Steuerung = new SpielerSteu();
    }
    private void OnEnable()
    {
        Steuerung.Enable();
    }


    void Update()
    {
        Checkforenemy = Physics.CheckSphere(transform.position, lockonrange, Lockonlayer);
        if (Checkforenemy == true)
        {
            float shortestDistance = Mathf.Infinity;                                                          // Value um später if (distancefromtarget) für einen frame zu resten, der wert muss immer höher sein als distanceformtarget, deswegen infinity
            Collider[] colliders = Physics.OverlapSphere(transform.position, lockonrange);

            for (int i = 0; i < colliders.Length; i++)
            {
                Enemylistcollider = colliders[i].GetComponent<Enemylockon>();
                if (Enemylistcollider != null)
                {
                    if (!availabletargets.Contains(Enemylistcollider))
                    {
                        availabletargets.Add(Enemylistcollider);
                    }
                }
            }
            for (int t = 0; t < availabletargets.Count; t++)
            {
                float distancefromtarget = Vector3.Distance(transform.position, availabletargets[t].transform.position);
                if (distancefromtarget < shortestDistance)
                {
                    if (Steuerung.Spielerboden.Lockon.WasPressedThisFrame() && lockoncheck == false)
                    {
                        LockonUI.SetActive(true);
                        shortestDistance = distancefromtarget;
                        lockontarget = availabletargets[t].lockontransform;
                        Lockonchange();
                        lockoncheck = true;
                    }
                    if (Steuerung.Spielerboden.Lockonwechsel.WasReleasedThisFrame() || EnemyHP.switchtargetafterdeath == true)
                    {
                        LockonUI.SetActive(true);
                        EnemyHP.switchtargetafterdeath = false;
                        Cam1.m_RecenterToTargetHeading.m_RecenteringTime = 0.2f;
                        shortestDistance = distancefromtarget;
                        lockontarget = availabletargets[t].lockontransform;
                        Invoke("Tabchange", 0.3f);
                    }
                }
            }
        }
        /*if (lockontarget != null && lockoncheck == true)
        {
            //Lockontargetobject.rotation = Quaternion.LookRotation(lockontarget.transform.position - transform.position, Vector3.up);
            Cam1.m_RecenterToTargetHeading.m_enabled = true;
            Cam1.LookAt = Lockontargetobject;
            Cam1.Follow = Lockontargetobject;

            
            if (Movescript.moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Movescript.moveDirection, Vector3.up);                                              //Char dreht sich
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationgesch * Time.deltaTime);
                Lockontargetobject.rotation = Quaternion.LookRotation(lockontarget.transform.position - transform.position, Vector3.up);                  //target - spielerposition  only target geht nicht. Die spielerpostion muss noch abgezogen werden um den richtigen winkel zu errechnen
                Cam1.m_RecenterToTargetHeading.m_enabled = true;
                Cam1.LookAt = Lockontargetobject;
                Cam1.Follow = Lockontargetobject;
            }
        }
        if (Steuerung.Spielerboden.Lockon.WasPerformedThisFrame() && cancellockon == true || Checkforenemy == false)
        {
            cancellockon = false;
            LockonUI.SetActive(false);
            lockonreset();
        }
    }
    private void Lockonchange()
    {
        lockoncheck = true;
        Invoke("recentercamtime", 0.3f);
    }
    private void recentercamtime()
    {
        cancellockon = true;
        Cam1.m_RecenterToTargetHeading.m_RecenteringTime = 0f;
    }
    private void Tabchange()
    {
        Cam1.m_RecenterToTargetHeading.m_RecenteringTime = 0f;
    }
    private void lockonreset()
    {
        LockonUI.SetActive(false);
        Cam1.m_RecenterToTargetHeading.m_RecenteringTime = 0.2f;
        lockoncheck = false;
        Cam1.m_RecenterToTargetHeading.m_enabled = false;
        Cam1.LookAt = Spielertransform.transform;
        Cam1.Follow = Spielertransform.transform;
        availabletargets.Clear();
        lockontarget = null;
    }*/
}
