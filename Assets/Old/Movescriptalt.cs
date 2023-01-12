using System.Collections;
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class Movescriptalt : MonoBehaviour
{
    /*[SerializeField] internal Schwertattack AttackScript;
    [SerializeField] internal Schwertattack Weaponslot1sript;
    [SerializeField] internal Bowattack Weaponslot2sript;


    public float rotationgesch;
    public float gesch;
    public float sprunghohe;
    public Transform Kamerarichtung;
    public GameObject Charwechsel1;
    public GameObject Charwechsel2;
    public CinemachineFreeLook Cam1;
    public CinemachineVirtualCamera Cam2;

    /*public LayerMask Lockonlayer;
    public float lockonrange;
    public bool lockoncheck;
    public bool Checkforenemy;
    public static Transform lockontarget;
    public Transform Lockontargetobject;
    public GameObject LockonUI;
    public Enemylockon Enemylistcollider;
    //public static List<Enemylockon> availabletargets = new List<Enemylockon>();

    public float movementspeedattack;
    public float gravitation;
    public float runter;
    public bool amBoden;
    public bool laufanimation;
    public bool idleanimation;
    public bool springt;
    public bool fallenanicheck;
    public bool attackabstandboden;
    public bool air1check;
    public bool attackonceperair;
    public CharacterController controller;

    private SpielerSteu Steuerung;
    private Vector2 laufen;
    private float originalStepOffSet;
    private Animator animator;                                    
    private float timercheck;

    //weaponchange
    private bool weapon1;

    void Awake()
    {
        weapon1 = true;
        Weaponslot2sript.enabled = false;
        Steuerung = new SpielerSteu();
        Steuerung.Spielerboden.Laufen.performed += Context => laufen = Context.ReadValue<Vector2>();
        controller = GetComponent<CharacterController>();
        originalStepOffSet = controller.stepOffset;
        animator = GetComponent<Animator>();
        //Charwechsel1.SetActive(false);
    }
    private void OnEnable()
    {
        Weaponslot1sript = GetComponent<Schwertattack>();
        Weaponslot2sript = GetComponent<Bowattack>();
        Cam2.gameObject.SetActive(false);
        Steuerung.Enable();
        laufanimation = false;
        runter = 0f;

    }

    private void OnDisable()
    {
        //Steuerung.Disable();
    }
    void Update()
    {
        float h = laufen.x;                                                                         // Move Script
        float v = laufen.y;

        Vector3 moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * gesch;
        moveDirection.Normalize();

        moveDirection = Quaternion.AngleAxis(Kamerarichtung.rotation.eulerAngles.y, Vector3.up) * moveDirection;                     //Kamera dreht sich mit dem Char
        float gravity = Physics.gravity.y * gravitation;

        runter += gravity * Time.deltaTime;

        if (controller.isGrounded)
        {
            runter = -0.5f;
            controller.stepOffset = originalStepOffSet;
            fallenanicheck = false;
            amBoden = true;
            attackonceperair = true;
            air1check = true;
            springt = false;
            timercheck += Time.deltaTime;

            if (moveDirection != Vector3.zero)
            {
                laufanimation = true;
                idleanimation = false;
            }
            else
            {
                laufanimation = false;
                idleanimation = true;
            }
            if (Steuerung.Spielerboden.Sprung.WasPressedThisFrame() && timercheck > 0.2f && AttackScript.cantjump == false)                               // Sprung
            {
                runter = Mathf.Sqrt(sprunghohe * -3 * gravity);
                springt = true;
                runter = sprunghohe;
                timercheck = 0f;
                Invoke("springenausschalten", 0.7f);
            }         
        }
        else
        {
            controller.stepOffset = 0;
            amBoden = false;

            if ((springt == true && runter < -0.7f) || runter < -3f)
            {
                fallenanicheck = true;
                springt = false;

            }
            else
            {
                fallenanicheck = false;
            }
        }

        Vector3 velocity = moveDirection * magnitude;
        velocity = VelocityUneben(velocity);
        velocity.y += runter;
        if (AttackScript.moveattackcheck == false)
        {
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            controller.Move(velocity / movementspeedattack * Time.deltaTime);
        }
        if (moveDirection != Vector3.zero && Lockontraget.lockoncheck == false)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);                                              //Char dreht sich
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationgesch * Time.deltaTime);
        }
        /*Checkforenemy = Physics.CheckSphere(transform.position, lockonrange, Lockonlayer);
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
                        Lockon();
                        shortestDistance = distancefromtarget;
                        lockontarget = availabletargets[t].lockontransform;
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
        if (lockontarget != null && lockoncheck == true)
        {
            Lockontargetobject.rotation = Quaternion.LookRotation(lockontarget.transform.position - transform.position, Vector3.up);
            Cam1.m_RecenterToTargetHeading.m_enabled = true;
            Cam1.LookAt = Lockontargetobject;
            Cam1.Follow = Lockontargetobject;

            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);                                              //Char dreht sich
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationgesch * Time.deltaTime);
                Lockontargetobject.rotation = Quaternion.LookRotation(lockontarget.transform.position - transform.position, Vector3.up);                  //target - spielerposition  only target geht nicht. Die spielerpostion muss noch abgezogen werden um den richtigen winkel zu errechnen
                Cam1.m_RecenterToTargetHeading.m_enabled = true;
                Cam1.LookAt = Lockontargetobject;
                Cam1.Follow = Lockontargetobject;
            }
        }
            if (Steuerung.Spielerboden.Lockon.WasPerformedThisFrame() && lockoncheck == true || Checkforenemy == false)
        {
            LockonUI.SetActive(false);
            lockonreset();
        }
        if (Steuerung.Spielerboden.Wechsel.WasPerformedThisFrame())
        {
            //LockonUI.SetActive(false);
            Cam1.m_RecenterToTargetHeading.m_RecenteringTime = 0.2f;
            //lockoncheck = false;
            Cam1.m_RecenterToTargetHeading.m_enabled = false;
            //availabletargets.Clear();
            animator.Play("Idle", 0, 0f);
            Charwechsel2.transform.position = Charwechsel1.transform.position;
            Charwechsel2.transform.rotation = Charwechsel1.transform.rotation;
            Charwechsel1.SetActive(false);
            Charwechsel2.SetActive(true);
            Cam1.LookAt = Charwechsel2.transform;
            Cam1.Follow = Charwechsel2.transform;
        }
        if (Steuerung.Spielerboden.Waffenwechsel.WasPerformedThisFrame())
        {
            if (weapon1 == true)
            {
                Weaponslot1sript.enabled = false;
                Weaponslot2sript.enabled = true;
                weapon1 = false;
            }
            else
            {
                Weaponslot1sript.enabled = true;
                Weaponslot2sript.enabled = false;
                weapon1 = true;
            }
        }
        if (minhighforairattack())
        {
            attackabstandboden = false;
        }
        else
        {
            attackabstandboden = true;
        }
        //Charwechsel1.GetComponent<test1>().enabled = false;
    }
    private Vector3 VelocityUneben(Vector3 velocity)
    {
        Ray nachunten = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(nachunten, out RaycastHit hitInfo, 0.2f))
        {
            Quaternion bodensteigung = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            Vector3 Velocitysteigung = bodensteigung * velocity;

            if (Velocitysteigung.y < 0)
            {
                return Velocitysteigung;
            }
        }

        return velocity;
    }
    private bool minhighforairattack()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.3f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.8f))
            return true;
        else return false;
    }
    private void springenausschalten()
    {
        springt = false;
    }
    /*private void Lockon()
    {
        Invoke("Lockonchange", 0.1f);
    }
    private void Lockonchange()
    {
        lockoncheck = true;
        Invoke("recentercamtime", 0.3f);
    }
    private void recentercamtime()
    {
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
        Cam1.LookAt = Charwechsel1.transform;
        Cam1.Follow = Charwechsel1.transform;
        availabletargets.Clear();
        lockontarget = null;
    }*/
}

/*}

LockonUI.SetActive(true);
//Enemytransform.AddRange(GameObject.FindGameObjectsWithTag("Gegner"));
Lockontargetobject.rotation = Quaternion.LookRotation(lockontarget.position - transform.position, Vector3.up);                   //target - spielerposition  only target geht nicht. Die spielerpostion muss noch abgezogen werden um den richtigen winkel zu errechnen
Cam1.m_RecenterToTargetHeading.m_enabled = true;
Cam1.LookAt = Lockontargetobject;
Cam1.Follow = Lockontargetobject;

if (moveDirection != Vector3.zero)
{
    Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);                                              //Char dreht sich
    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationgesch * Time.deltaTime);
    Lockontargetobject.rotation = Quaternion.LookRotation(lockontarget.position - transform.position, Vector3.up);                   //target - spielerposition  only target geht nicht. Die spielerpostion muss noch abgezogen werden um den richtigen winkel zu errechnen
    Cam1.m_RecenterToTargetHeading.m_enabled = true;
    Cam1.LookAt = Lockontargetobject;
    Cam1.Follow = Lockontargetobject;*/

//LockonUI.SetActive(true);
/*Lockontargetobject.rotation = Quaternion.LookRotation(character.transform.position - transform.position, Vector3.up);
Cam1.m_RecenterToTargetHeading.m_enabled = true;
Cam1.LookAt = Lockontargetobject;
Cam1.Follow = Lockontargetobject;

if (moveDirection != Vector3.zero)
{
    Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);                                              //Char dreht sich
    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationgesch * Time.deltaTime);
    Lockontargetobject.rotation = Quaternion.LookRotation(character.transform.position - transform.position, Vector3.up);                  //target - spielerposition  only target geht nicht. Die spielerpostion muss noch abgezogen werden um den richtigen winkel zu errechnen
    Cam1.m_RecenterToTargetHeading.m_enabled = true;
    Cam1.LookAt = Lockontargetobject;
    Cam1.Follow = Lockontargetobject;
}*/



