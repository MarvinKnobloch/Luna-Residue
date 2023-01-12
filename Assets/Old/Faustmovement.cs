using System.Collections;
using UnityEngine;
using Cinemachine;

public class Faustmovement : MonoBehaviour
{/*
    [SerializeField]
    internal Faustattack AttackScript;

    public float rotationgesch;
    public float gesch;
    public float sprunghohe;
    public Transform Kamerarichtung;
    public GameObject Charwechsel1;
    public GameObject Charwechsel2;
    public CinemachineFreeLook Cam1;

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

    void Awake()
    {
        Steuerung = new SpielerSteu();
        Steuerung.Spielerboden.Laufen.performed += Context => laufen = Context.ReadValue<Vector2>();
        controller = GetComponent<CharacterController>();
        originalStepOffSet = controller.stepOffset;
        animator = GetComponent<Animator>();
        Charwechsel1.SetActive(false);
    }
    private void OnEnable()
    {
        Steuerung.Enable();
        laufanimation = false;
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
            if (Steuerung.Spielerboden.Sprung.WasPressedThisFrame() && timercheck > 0.2f)                               // Sprung
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

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);                                              //Char dreht sich
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationgesch * Time.deltaTime);
        }

        if (Steuerung.Spielerboden.Wechsel.WasPerformedThisFrame())
        {
            animator.Play("Idle", 0, 0f);
            Charwechsel2.transform.position = Charwechsel1.transform.position;
            Charwechsel2.transform.rotation = Charwechsel1.transform.rotation;
            Charwechsel1.SetActive(false);
            Charwechsel2.SetActive(true);
            Cam1.LookAt = Charwechsel2.transform;
            Cam1.Follow = Charwechsel2.transform;
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
    }*/
}



