using System.Collections;
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using TMPro;
using System;

public class Movescript : MonoBehaviour
{
    //Bugs:
    //Stormlightning flug animation hat sich am ende nicht verändert (zu der Zeit war der Char Toggle Active State = true)
    [SerializeField] internal Swordattack Weaponslot1script;
    [SerializeField] internal Bowattack Weaponslot2script;
    [SerializeField] internal AimScript aimscript;

    [NonSerialized] public CharacterController charactercontroller;
    [NonSerialized] public SpielerSteu controlls;
    private Animator animator;

    public Transform CamTransform;
    public CinemachineFreeLook Cam1;
    public CinemachineVirtualCamera Cam2;

    public GameObject Charprefabarrow;
    public GameObject dazeimage;

    [NonSerialized] public Vector2 move;
    public float movementspeed;
    [NonSerialized] public Vector3 moveDirection;
    public float rotationspeed;
    [NonSerialized] public Vector3 velocity;
    public float jumpheight;
    public float gravitation;
    private float normalgravition = 3.5f;
    public float graviti;
    private float originalStepOffSet;

    private Vector3 Slidedownwalls;
    private float slowmovement = 6;

    //attack abfragen
    public float movementspeedattack;
    private float attackrotationspeed = 100f;

    [NonSerialized] public bool amBoden;
    [NonSerialized] public bool inderluft;
    [NonSerialized] public bool airattackminheight;
    [NonSerialized] public bool attackonceair;
    [NonSerialized] public bool bowair3intoground;                                 // für lockon
    private float jumpcdafterland;
    private float jumpcd = 0.2f;
    [NonSerialized] public bool hook;
    [NonSerialized] public bool attack3intoair;
    [NonSerialized] public bool fullcharge;

    private Vector3 hitnormal;

    //Characterrig
    public MonoBehaviour Charrig;
    public bool activaterig;
    [SerializeField] private GameObject head;

    //StatemachineScripts
    private Playermovement playermovement;

    //animationstate
    public string currentstate;
    const string jumpstate = "Jump";
    const string idlestate = "Idle";
    const string runstate = "Running";
    const string fallstate = "Fall";
    const string healstart = "Healstart";
    const string swimstate = "Swim";
    const string swimidlestate = "Swimidle";
    const string dazestate = "Daze";
    const string hookshotstate = "Hookshot";
    const string chargestate = "Chargearrow";
    const string aimholdstate = "Aimhold";
    const string releasearrowstate = "Releasearrow";
    const string firedashstate = "Firedash";
    const string waterintoairstate = "Waterintoair";
    const string waterkickstate = "Waterkick";
    const string icelancebackflipstate = "Icelancebackflip";
    const string darkportalendstate = "Darkportalend";
    const string earthslidereleasestate = "Earthsliderelease";

    //Inventory;
    public Inventorycontroller matsinventory;
    public Inventorycontroller swordinventory;
    public Inventorycontroller bowinventory;
    public Inventorycontroller fistinventory;
    public Inventorycontroller headinventory;
    public Inventorycontroller chestinventory;
    public Inventorycontroller glovesinventory;
    public Inventorycontroller shoesinventory;
    public Inventorycontroller beltinventory;
    public Inventorycontroller necklessinventory;
    public Inventorycontroller ringinventory;


    //Lockon
    public LayerMask Lockonlayer;
    private float lockonrange;
    public static bool lockoncheck;
    [NonSerialized] public bool Checkforenemy;
    public static Transform lockontarget;
    [NonSerialized] public GameObject HealUI;
    [NonSerialized] public Enemylockon Enemylistcollider;
    public static List<Enemylockon> availabletargets = new List<Enemylockon>();
    public bool cancellockon;
    private Transform targetbeforeswap;

    //Spells
    private Healingscript healingscript;
    public LayerMask spellsdmglayer;
    public GameObject damagetext;
    private bool chainligthningenemys;
    private Vector3 startpos;
    private float starttime;
    private float naturespeed = 2;
    private float naturetraveltime = 1;
    private float lightningspeed = 30;
    [NonSerialized] public Transform lightningfirsttarget;
    [NonSerialized] public Transform ligthningsecondtarget;
    [NonSerialized] public Transform lightningthirdtarget;
    public LayerMask lightninglayer;
    private float earthslidespeed = 20;

    public State state;
    public enum State
    {
        Ground,
        Air,
        Jump,
        Slidedownwall,
        Swim,
        Heal,
        Slow,
        Slowjump,
        Addgravity,
        Daze,
        Stun,
        Bowcharge,
        Bowischarged,
        Airintoground,
        Actionintoair,
        Groundattack,
        Airattack,
        BowGroundattack,
        BowAirattack,
        Bowweaponswitch,
        Bowhookshot,
        Beforedash,
        DashKick,
        Firedashstart,
        Firedash,
        Waterpushback,
        Waterintoair,
        Waterkickend,
        Naturethendril,
        Naturethendrilgettotarget,
        Icelancestart,
        Icelancefly,
        Icelanceend,
        Stormchainligthning,
        Secondlightning,
        Thirdlightning,
        Endlightning,
        Darkportalend,
        Earthslide,
        Abilitiesempty,
    }


    void Awake()
    {
        lockonrange = Statics.playerlockonrange;
        Charrig.enabled = false;
        aimscript.enabled = false;
        lockoncheck = false;
        controlls = Keybindinputmanager.inputActions;
        controlls.Player.Movement.performed += Context => move = Context.ReadValue<Vector2>();
        charactercontroller = GetComponent<CharacterController>();
        originalStepOffSet = charactercontroller.stepOffset;
        animator = GetComponent<Animator>();
        healingscript = GetComponent<Healingscript>();
        state = State.Air;
        starttime = Time.time;
        Statics.normalgamespeed = 1;
        Statics.normaltimedelta = Time.fixedDeltaTime;

        playermovement = new Playermovement();
        playermovement.psm = this;

    }
    private void OnEnable()
    {
        Cam2.gameObject.SetActive(false);
        controlls.Enable();
        graviti = -0.5f;
        gravitation = normalgravition;
        cancellockon = false;
        Charprefabarrow.SetActive(false);
        currentstate = null;
    }

    private void OnDisable()
    {
        //Steuerung.Disable();
    }
    private void Update()
    {
        switch (state)
        {
            default:
            case State.Ground:
                playermovement.movement();
                playermovement.jump();
                playermovement.groundcheck();
                //Grounded();
                //Startjump();
                Charlockon();
                break;
            case State.Air:
                InAir();
                Movement();
                Charlockon();
                Minhighforairattack();
                break;
            case State.Jump:                 //kurzer übergang von ground to air
                Jumpmovement();
                break;
            case State.Heal:
                healingscript.heal();
                starthealjump();
                break;
            case State.Slidedownwall:
                slidewalls();
                Charlockon();
                break;
            case State.Swim:
                swim();
                Startjump();
                Charlockon();
                break;
            case State.Addgravity:
                gravity();
                Charlockon();
                break;
            case State.Slow:
                slow();
                Startslowjump();
                Charlockon();
                break;
            case State.Slowjump:
                slowjumpmovement();
                Charlockon();
                break;
            case State.Daze:
                daze();
                break;
            case State.Stun:
                stun();
                break;
            case State.Bowcharge:
                chargearrow();
                break;
            case State.Bowischarged:
                Aimmovement();
                Grounded();
                break;
            case State.Airintoground:
                airintoground();
                break;
            case State.Actionintoair:
                intoair();
                break;
            case State.Groundattack:
                Attackmovement();
                Charlockon();
                meleelockonrotation();
                Groundedattack();
                break;
            case State.Airattack:
                Attackmovement();
                Charlockon();
                meleelockonrotation();
                break;
            case State.BowGroundattack:
                Bowgroundmovement();
                Charlockon();
                lockonbowrotation();
                Groundedattack();
                break;
            case State.BowAirattack:
                Charlockon();
                Bowgroundmovement();
                break;
            case State.Bowweaponswitch:
                Charlockon();
                bowswitch();
                break;
            case State.Bowhookshot:
                Charlockon();
                Bowhookshot();
                Minhighforairattack();
                break;
            case State.Beforedash:           //damit man beim angreifen noch die Richtung bestimmen kann
                beforedashmovement();
                break;
            case State.DashKick:
                Attackmovement();  // wegen Kick nicht wegen dash
                break;
            case State.Firedashstart:
                Charlockon();
                firedashstartmovement();
                break;
            case State.Firedash:
                Charlockon();
                firedash();
                break;
            case State.Waterpushback:
                Charlockon();
                waterpushback();
                break;
            case State.Waterintoair:
                Charlockon();
                waterintoair();
                break;
            case State.Waterkickend:
                Charlockon();
                waterkickend();
                break;
            case State.Naturethendril:
                Charlockon();
                naturethendrilstart();
                break;
            case State.Naturethendrilgettotarget:
                Charlockon();
                naturethendrilgettotarget();
                break;
            case State.Icelancestart:
                Charlockon();
                icelanceplayermovement();
                break;
            case State.Icelancefly:
                Charlockon();
                icelanceplayertotarget();
                break;
            case State.Icelanceend:
                Charlockon();
                break;
            case State.Stormchainligthning:
                Charlockon();
                stormchainligthning();
                break;
            case State.Secondlightning:
                Charlockon();
                stormchainlightningsecondtarget();
                break;
            case State.Thirdlightning:
                Charlockon();
                stormchainlightningthirdtarget();
                break;
            case State.Endlightning:
                Charlockon();
                stormlightningbacktomain();
                break;
            case State.Darkportalend:
                Charlockon();
                darkportalending();
                break;
            case State.Earthslide:
                Charlockon();
                earthslidestart();
                break;
            case State.Abilitiesempty:
                Charlockon();
                break;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)                // produziert 80B garbage bei jedem call
    {
        hitnormal = hit.normal;
    }
    private void Movement()
    {
        float h = move.x;                                                                         // Move Script
        float v = move.y;

        moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * movementspeed;
        moveDirection.Normalize();

        moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;                     //Kamera dreht sich mit dem Char

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);                                              //Char dreht sich
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationspeed * Time.deltaTime);
        }
        velocity = moveDirection * magnitude;
        if (charactercontroller.isGrounded)
        {
            velocity = VelocityUneben(velocity);
        }
        velocity.y += graviti;

        if (charactercontroller.isGrounded && Statics.otheraction == false)
        {
            jumpcdafterland += Time.deltaTime;
            velocity.y = -0.5f;
            if (moveDirection != Vector3.zero)
            {
                ChangeAnimationState(runstate);
            }
            else
            {
                ChangeAnimationState(idlestate);
            }
        }
        charactercontroller.Move(velocity * Time.deltaTime);

        if (Statics.healcdbool == false && LoadCharmanager.disableattackbuttons == false)
        {
            if (amBoden == true)
            {
                if (controlls.Player.Heal.IsPressed() && Statics.otheraction == false)
                {
                    state = State.Heal;
                    healingscript.strgpressed();
                    ChangeAnimationStateInstant(healstart);
                }
            }
        }
    }
    private void Grounded()
    {
        if (charactercontroller.isGrounded)
        {
            //jumpcdafterland += Time.deltaTime;
            graviti = -0.5f;
        }
        else
        {
            float gravity = Physics.gravity.y * gravitation;
            graviti += gravity * Time.deltaTime;
        }
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.3f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.5f) == false && graviti < -5f)
        {
            state = State.Actionintoair;
        }
    }
    private void Startjump()
    {
        jumpcdafterland += Time.deltaTime;
        if (LoadCharmanager.disableattackbuttons == false || LoadCharmanager.gameispaused == false)
        {
            if (controlls.Player.Jump.WasPressedThisFrame() && jumpcdafterland > jumpcd)
            {
                state = State.Jump;
                amBoden = false;
                ChangeAnimationState(jumpstate);
                float gravity = Physics.gravity.y * gravitation;
                graviti = Mathf.Sqrt(jumpheight * -3 * gravity);
                graviti = jumpheight;
            }
        }
    }
    public void jumppad(float jumpheight)
    {
        state = State.Jump;
        amBoden = false;
        ChangeAnimationState(jumpstate);
        float gravity = Physics.gravity.y * gravitation;
        graviti = Mathf.Sqrt(jumpheight * -3 * gravity);
        graviti = jumpheight;
    }
    private void starthealjump()
    {
        jumpcdafterland += Time.deltaTime;
        if (LoadCharmanager.disableattackbuttons == false || LoadCharmanager.gameispaused == false)
        {
            if (controlls.Player.Jump.WasPressedThisFrame() && jumpcdafterland > jumpcd)
            {
                GetComponent<Healingscript>().jumpwhileheal();
                state = State.Jump;
                amBoden = false;
                ChangeAnimationState(jumpstate);
                float gravity = Physics.gravity.y * gravitation;
                graviti = Mathf.Sqrt(jumpheight * -3 * gravity);
                graviti = jumpheight;
            }
        }
    }

    private void Jumpmovement()
    {

        float h = move.x;
        float v = move.y;

        moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * movementspeed;
        moveDirection.Normalize();

        moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;

        float gravity = Physics.gravity.y * gravitation;
        graviti += gravity * Time.deltaTime;

        velocity = moveDirection * magnitude;
        velocity.y += graviti;

        charactercontroller.Move(velocity * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationspeed * Time.deltaTime);
        }
        if (charactercontroller.isGrounded == false)
        {
            state = State.Actionintoair;
        }
    }
    private void swim()
    {
        {
            float h = move.x;                                                                        
            float v = move.y;

            moveDirection = new Vector3(h, 0, v);
            float magnitude = Mathf.Clamp01(moveDirection.magnitude) * movementspeed;
            moveDirection.Normalize();

            moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;                     //Kamera dreht sich mit dem Char

            charactercontroller.Move(velocity * Time.deltaTime);

            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);                                              //Char dreht sich
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationspeed * Time.deltaTime);
            }
            velocity = moveDirection * magnitude;
            velocity.y = 0;
            //jumpcdafterland += Time.deltaTime;

            if (moveDirection.x != 0 && moveDirection.z != 0)
            {
                ChangeAnimationState(swimstate);
            }
            else
            {
                ChangeAnimationState(swimidlestate);
                {
                    RaycastHit hit;
                    Ray nachunten = new Ray(head.transform.position, Vector3.down);
                    if (Physics.Raycast(nachunten, out hit))
                    {
                        float distance = hit.distance;
                        if (distance > 0.86)
                        {
                            velocity.y -= 0.5f;
                        }
                        else if (distance < 0.82)
                        {
                            velocity.y += 0.5f;
                        }
                        else
                        {
                            velocity.y = 0;
                        }
                    }
                }
            }

        }
    }
    private void gravity()
    {
        gravitation = normalgravition;
        if (charactercontroller.isGrounded)
        {
            graviti = -0.5f;
        }
        else
        {
            float gravity = Physics.gravity.y * gravitation;
            graviti += gravity * Time.deltaTime;
        }
        velocity = new Vector3(0, 0, 0);
        velocity.y += graviti;
        charactercontroller.Move(velocity * Time.deltaTime);
    }
    private void dazetoslow()
    {
        state = State.Slowjump;
    }
    
    private void slow()
    {
        {
            float h = move.x;                                                                         // Move Script
            float v = move.y;

            moveDirection = new Vector3(h, 0, v);
            float magnitude = Mathf.Clamp01(moveDirection.magnitude) * slowmovement;
            moveDirection.Normalize();

            moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;                     //Kamera dreht sich mit dem Char

            charactercontroller.Move(velocity * Time.deltaTime);

            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);                                              //Char dreht sich
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationspeed * Time.deltaTime);
            }
            velocity = moveDirection * magnitude;
            if (charactercontroller.isGrounded)
            {
                velocity = VelocityUneben(velocity);
            }
            velocity.y += graviti;
            if (charactercontroller.isGrounded)
            {
                //jumpcdafterland += Time.deltaTime;
                graviti = -0.5f;
                if (moveDirection != Vector3.zero)
                {
                    ChangeAnimationState(runstate);
                }
                else
                {
                    ChangeAnimationState(idlestate);
                }
            }
            if (Statics.slow == false)
            {
                Statics.otheraction = false;
                Statics.dash = false;
                state = State.Airintoground;
            }
        }
    }
    private void Startslowjump()
    {
        jumpcdafterland += Time.deltaTime;
        if (LoadCharmanager.disableattackbuttons == false || LoadCharmanager.gameispaused == false)
        {
            if (controlls.Player.Jump.WasPressedThisFrame() && jumpcdafterland > jumpcd)
            {
                amBoden = false;
                state = State.Slowjump;
                ChangeAnimationState(jumpstate);
                float gravity = Physics.gravity.y * gravitation;
                graviti = Mathf.Sqrt(jumpheight * -3 * gravity);
                graviti = jumpheight;
            }
        }
    }
    private void slowjumpmovement()
    {

        float h = move.x;
        float v = move.y;

        moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * slowmovement;
        moveDirection.Normalize();

        moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;

        gravitation = normalgravition;
        float gravity = Physics.gravity.y * gravitation;
        graviti += gravity * Time.deltaTime;

        velocity = moveDirection * magnitude;
        velocity.y += graviti;

        charactercontroller.Move(velocity * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationspeed * Time.deltaTime);
        }
        if (graviti < -2)
        {
            ChangeAnimationState(fallstate);
            if (charactercontroller.isGrounded == true)
            {
                state = State.Slow;
            }
        }
    }
    private void beforedashmovement()
    {
        float h = move.x;                                                                         // Move Script
        float v = move.y;

        moveDirection = new Vector3(h, 0, v);
        //float magnitude = Mathf.Clamp01(moveDirection.magnitude) * slowmovement;
        moveDirection.Normalize();

        moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;                     //Kamera dreht sich mit dem Char

        //controller.Move(velocity * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);                                              //Char dreht sich
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 5000 * Time.deltaTime);
        }
    }
    private void daze()
    {
        // die ganzen parameter werden im attackscript zurückgesetzt
        ChangeAnimationState(dazestate);
        gravitation = normalgravition;
        if (charactercontroller.isGrounded)
        {
            graviti = -0.5f;
        }
        else
        {
            float gravity = Physics.gravity.y * gravitation;
            graviti += gravity * Time.deltaTime;
        }
        velocity = new Vector3(0, 0, 0);
        velocity.y += graviti;
        charactercontroller.Move(velocity * Time.deltaTime);

        if (controlls.Player.Attack3.WasPerformedThisFrame())
        {
            Statics.dazecounter += 1;
        }
        if (Statics.dazecounter >= Statics.dazekicksneeded)
        {
            dazeimage.SetActive(false);
            Statics.dash = false;
            Statics.otheraction = false;
            state = State.Airintoground;
        }
    }
    private void stun()
    {
        ChangeAnimationStateInstant(dazestate);
        gravitation = normalgravition;
        if (charactercontroller.isGrounded)
        {
            graviti = -0.5f;
        }
        else
        {
            float gravity = Physics.gravity.y * gravitation;
            graviti += gravity * Time.deltaTime;
        }
        velocity = new Vector3(0, 0, 0);
        velocity.y += graviti;
        charactercontroller.Move(velocity * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position + Vector3.up * 0.3f, transform.forward * 1f + Vector3.down * 1f);
        //Gizmos.DrawSphere(transform.position - Vector3.down * 0.2f, 0.4f);
        //Gizmos.DrawRay(head.transform.position, Vector3.down);
    }

    private Vector3 VelocityUneben(Vector3 velocity)
    {
        Ray nachunten = new Ray(transform.position + Vector3.up * 0.3f, Vector3.down);          // sonst geht der ray durch den boden
        if (Physics.Raycast(nachunten, out RaycastHit nachunteninfo, 0.8f))
        {
            Slidedownwalls = nachunteninfo.normal;
            if (Vector3.Angle(Slidedownwalls, Vector3.up) > charactercontroller.slopeLimit + 2 && Vector3.Angle(Slidedownwalls, Vector3.up) < 89f)                 // wenn ich jump kann ich noch auf bis zu 45winkel stehen
            {
                Charrig.enabled = false;
                aimscript.virtualcam = false;
                aimscript.aimend();
                Statics.otheraction = false;
                ChangeAnimationState(idlestate);
                state = State.Slidedownwall;
            }
        }
        else
        {
            if (charactercontroller.isGrounded)
            {
                bool isGrounded = Vector3.Angle(Vector3.up, hitnormal) <= 55 && Vector3.Angle(Vector3.up, hitnormal) < 89f;
                if (isGrounded == false)
                {
                    Charrig.enabled = false;
                    aimscript.virtualcam = false;
                    aimscript.aimend();
                    Statics.otheraction = false;
                    ChangeAnimationState(idlestate);
                    state = State.Slidedownwall;
                }
            }
        }
        Ray uneben = new Ray(this.transform.position + Vector3.up * 0.3f, Vector3.down);
        if (Physics.Raycast(uneben, out RaycastHit unebeninfo, 1f))
        {
            Quaternion bodensteigung = Quaternion.FromToRotation(Vector3.up, unebeninfo.normal);
            Vector3 Velocitysteigung = bodensteigung * velocity;
            if (Velocitysteigung.y < 0)
            {
                return Velocitysteigung;
            }
        }
        return velocity;
    }
    private void slidewalls()
    {
        bool checkforobject = Physics.CheckSphere(transform.position - Vector3.down * 0.2f, 0.4f);
        if (checkforobject == false)
        {
            charactercontroller.Move(Vector3.zero);
            state = State.Air;
        }
        float gravity = Physics.gravity.y * gravitation;
        graviti += gravity * Time.deltaTime;
        float angle = Vector3.Angle(Vector3.up, hitnormal);
        //Debug.Log(angle);
        float math = 90 - angle;                         // wie weit der spieler von der wand weggedrückt wird, um so größer der winkel um so weniger muss der spieler von der wand gegedrückt werden
        if (angle > 89.9)
        {
            Debug.Log("angletohigh");
            state = State.Air;
        }
        else if (angle > 80)
        {
            ChangeAnimationState(fallstate);
            moveDirection = new Vector3(hitnormal.x * (1.5f / angle * math), -hitnormal.y * (angle / math), hitnormal.z * (1.5f / angle * math)) * 20;
        }
        else if (angle > 70)
        {
            ChangeAnimationState(fallstate);
            moveDirection = new Vector3(hitnormal.x * (1.5f / angle * math), -hitnormal.y * 10, hitnormal.z * (1.5f / angle * math)) * 10;
        }
        else if (angle > charactercontroller.slopeLimit)
        {
            ChangeAnimationState(fallstate);
            moveDirection = new Vector3(hitnormal.x * (1.5f / angle * math), -hitnormal.y * 5, hitnormal.z * (1.5f / angle * math)) * 9;
        }
        else
        {
            state = State.Ground;
        }
        charactercontroller.Move(moveDirection * Time.deltaTime);
    }

    private void Groundedattack()
    {
        if (charactercontroller.isGrounded)
        {
            graviti = -0.5f;
        }
        else
        {
            float gravity = Physics.gravity.y * gravitation;
            graviti += gravity * Time.deltaTime;
        }
    }
    private void Attackmovement()
    {
        float h = move.x;                                                                         // Move Script
        float v = move.y;

        moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * movementspeed;
        moveDirection.Normalize();

        moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;                     //Kamera dreht sich mit dem Char

        velocity = moveDirection * magnitude;
        /*if (controller.isGrounded)
        {
            velocity = VelocityUneben(velocity);
        }*/
        velocity.y += graviti;

        charactercontroller.Move(velocity / movementspeedattack * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, attackrotationspeed * Time.deltaTime);
        }
    }
    private void Bowgroundmovement()
    {
        float h = move.x;                                                                         // Move Script
        float v = move.y;

        moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * movementspeed;
        moveDirection.Normalize();

        moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;                     //Kamera dreht sich mit dem Char

        velocity = moveDirection * magnitude;
        if (charactercontroller.isGrounded)
        {
            velocity = VelocityUneben(velocity);
        }
        velocity.y += graviti;

        charactercontroller.Move(velocity / movementspeedattack * Time.deltaTime);

    }
    private void InAir()
    {
        float gravity = Physics.gravity.y * gravitation;
        graviti += gravity * Time.deltaTime;
        if (graviti < -3f)
        {
            ChangeAnimationState(fallstate);
        }
        if (charactercontroller.isGrounded == true)
        {
            state = State.Airintoground;
        }
    }
    private void Minhighforairattack()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.3f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.8f))
        {
            airattackminheight = false;
        }
        else
        {
            airattackminheight = true;
        }
    }
    private void bowswitch()
    {
        float gravity = Physics.gravity.y * gravitation;
        graviti += gravity * Time.deltaTime;
        float h = move.x;                                                                         // Move Script
        float v = move.y;

        moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * movementspeed;
        moveDirection.Normalize();

        moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;                     //Kamera dreht sich mit dem Char

        velocity = moveDirection * magnitude;
        if (charactercontroller.isGrounded)
        {
            velocity = VelocityUneben(velocity);
        }
        velocity.y += graviti;

        charactercontroller.Move(velocity / movementspeedattack * Time.deltaTime);
    }
    private void airintoground()
    {
        charactercontroller.stepOffset = originalStepOffSet;
        amBoden = true;
        inderluft = false;
        attack3intoair = false;
        attackonceair = true;
        jumpcdafterland = 0f;
        state = State.Ground;
    }
    private void intoair()
    {
        Statics.otheraction = false;
        charactercontroller.stepOffset = 0;
        amBoden = false;
        inderluft = true;
        gravitation = normalgravition;
        state = State.Air;
    }
    private void chargearrow()
    {
        if (controlls.Player.Attack4.IsPressed())
        {
            if(activaterig == true)
            {
                Charrig.enabled = true;
                activaterig = false;
            }
        }
        else
        {
            activaterig = false;
            Charrig.enabled = false;
            aimscript.virtualcam = false;
            aimscript.aimend();
            Statics.otheraction = false;
            Charprefabarrow.SetActive(false);
            state = State.Ground;
        }
        if (fullcharge == true)
        {
            activaterig = false;
            Charrig.enabled = true;
            fullcharge = false;
            ChangeAnimationState(aimholdstate);
            state = State.Bowischarged;
        }
        float h = move.x;                                                                         // Move Script
        float v = move.y;

        moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * movementspeed;
        moveDirection.Normalize();

        moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;                     //Kamera dreht sich mit dem Char
        velocity = moveDirection * magnitude;

        animator.SetFloat("AimX", move.x, 0.05f, Time.deltaTime);
        animator.SetFloat("AimZ", move.y, 0.05f, Time.deltaTime);
        if (charactercontroller.isGrounded)
        {
            velocity = VelocityUneben(velocity);
        }
        else
        {
        }
        velocity.x = velocity.x / movementspeedattack;
        velocity.z = velocity.z / movementspeedattack;
        velocity.y += graviti;
        charactercontroller.Move(velocity * Time.deltaTime);
    }
    private void charrigenable()
    {
        activaterig = true;
    }
    private void Aimmovement()
    {
        if (controlls.Player.Attack4.IsPressed())
        {          
        }
        else
        {
            Charrig.enabled = false;
            ChangeAnimationState(releasearrowstate);
            state = State.Abilitiesempty;
        }
        float h = move.x;                                                                         // Move Script
        float v = move.y;

        moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * movementspeed;
        moveDirection.Normalize();

        moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;                     //Kamera dreht sich mit dem Char
        velocity = moveDirection * magnitude;

        animator.SetFloat("AimX", move.x, 0.05f, Time.deltaTime);
        animator.SetFloat("AimZ", move.y, 0.05f, Time.deltaTime);
        if (charactercontroller.isGrounded)
        {
            velocity = VelocityUneben(velocity);
        }
        else
        {
            /*Charrig.enabled = false;    //Grounded händelt den wechsel
            aimscript.virtualcam = false;
            aimscript.aimend();
            Statics.otheraction = false;
            state = State.Air;*/
        }
        velocity.x = velocity.x / movementspeedattack;
        velocity.z = velocity.z / movementspeedattack;
        velocity.y += graviti;
        charactercontroller.Move(velocity * Time.deltaTime);
    }
    private void arrowfullcharge()
    {
        fullcharge = true;
    }
    private void arrowreleased()
    {
        if (controlls.Player.Attack4.IsPressed())
        {
            state = State.Bowcharge;
            ChangeAnimationState(chargestate);
        }
        else
        {
            aimscript.virtualcam = false;
            aimscript.aimend();
            Statics.otheraction = false;
            Charprefabarrow.SetActive(false);
            state = State.Ground;
        }
    }
    private void Bowhookshot()
    {
        ChangeAnimationState(hookshotstate);
        amBoden = false;
        if (lockontarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, lockontarget.position, 25 * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(lockontarget.transform.position - transform.position, Vector3.up);
            if (Vector3.Distance(transform.position, lockontarget.position) < 2f)
            {
                graviti = 0.5f;
                gravitation = normalgravition;
                state = State.Air;
                ChangeAnimationState(fallstate);
                Statics.otheraction = false;
            }
        }
        else
        {
            graviti = 0.5f;
            gravitation = normalgravition;
            state = State.Air;
            Statics.otheraction = false;
        }
    }
    public void ChangeAnimationState(string newstate)
    {
        if (currentstate == newstate) return;
        animator.CrossFadeInFixedTime(newstate, 0.1f);
        currentstate = newstate;
    }
    public void ChangeAnimationStateInstant(string newstate)
    {
        if (currentstate == newstate) return;
        animator.Play(newstate);
        currentstate = newstate;
    }
    public void Charlockon()
    {
        if (LoadCharmanager.disableattackbuttons == false || LoadCharmanager.gameispaused == false)
        {
            if (controlls.Player.Lockon.WasPerformedThisFrame() && lockoncheck == false || EnemyHP.switchtargetafterdeath == true && lockoncheck == true || bowair3intoground == true)
            {
                EnemyHP.switchtargetafterdeath = false;
                bowair3intoground = false;
                Checkforenemy = Physics.CheckSphere(transform.position, lockonrange, Lockonlayer);
                if (Checkforenemy == true)
                {
                    if (lockontarget != null)
                    {
                        lockontarget.GetComponent<EnemyHP>().focustargetuiend();
                        lockontarget.GetComponent<EnemyHP>().unmarktarget();
                    }
                    float shortestDistance = 100f;                                                          
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
                            lockontarget = availabletargets[t].lockontransform;
                            shortestDistance = distancefromtarget;
                        }
                    }
                    lockontarget.GetComponent<EnemyHP>().focustargetuistart();
                    lockontarget.GetComponent<EnemyHP>().marktarget();
                    //LockonUI.SetActive(true);
                    lockoncheck = true;
                    Invoke("changelockoncancel", 0.1f);
                }
                else
                {
                    lockoncheck = false;
                    cancellockon = false;
                    lockontarget = null;
                }
            }
            if (controlls.Player.Lockonchange.WasPerformedThisFrame() && lockoncheck == true)          //target wechsel per button
            {
                Checkforenemy = Physics.CheckSphere(transform.position, lockonrange, Lockonlayer);
                if (Checkforenemy == true)
                {
                    float shortestDistance = 100f;                                                          
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
                    if (lockontarget != null)
                    {
                        targetbeforeswap = lockontarget;
                        lockontarget.GetComponent<EnemyHP>().unmarktarget();
                        lockontarget.GetComponent<EnemyHP>().focustargetuiend();
                    }
                    for (int t = 0; t < availabletargets.Count; t++)
                    {
                        float distancefromtarget = Vector3.Distance(transform.position, availabletargets[t].transform.position);
                        if (distancefromtarget < shortestDistance)
                        {
                            if (targetbeforeswap == availabletargets[t].lockontransform)
                            {

                            }
                            else
                            {
                                lockontarget = availabletargets[t].lockontransform;
                                shortestDistance = distancefromtarget;
                            }
                        }
                    }
                    lockontarget.GetComponent<EnemyHP>().focustargetuistart();
                    lockontarget.GetComponent<EnemyHP>().marktarget();
                    //LockonUI.SetActive(true);
                    EnemyHP.switchtargetafterdeath = false;
                    Invoke("changelockoncancel", 0.1f);
                }
                else
                {
                    //LockonUI.SetActive(false);
                    lockoncheck = false;
                    availabletargets.Clear();
                    cancellockon = false;
                    lockontarget = null;
                }
            }
            if (controlls.Player.Lockon.WasPerformedThisFrame() && cancellockon == true)            //beendet lockon durch buttonpress
            {
                if (lockontarget != null)
                {
                    lockontarget.GetComponent<EnemyHP>().focustargetuiend();
                    lockontarget.GetComponent<EnemyHP>().unmarktarget();
                }
                //LockonUI.SetActive(false);
                lockoncheck = false;
                availabletargets.Clear();
                cancellockon = false;
                lockontarget = null;
            }
            if (lockontarget != null && cancellockon == true)                                                                     //sucht neues target wenn momentanes target außer lockonrange ist
            {
                if (Vector3.Distance(transform.position, lockontarget.transform.position) > lockonrange)
                {
                    
                    lockontarget.GetComponent<EnemyHP>().focustargetuiend();
                    lockontarget.GetComponent<EnemyHP>().unmarktarget();
                    availabletargets.Clear();

                    float shortestDistance = 100f;
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
                    if (availabletargets.Count > 1)
                    {
                        for (int t = 0; t < availabletargets.Count; t++)
                        {
                            float distancefromtarget = Vector3.Distance(transform.position, availabletargets[t].transform.position);
                            if (distancefromtarget < shortestDistance)
                            {
                                lockontarget = availabletargets[t].lockontransform;
                                shortestDistance = distancefromtarget;
                            }
                        }
                        lockontarget.GetComponent<EnemyHP>().focustargetuistart();
                        lockontarget.GetComponent<EnemyHP>().marktarget();
                        //LockonUI.SetActive(true);
                        lockoncheck = true;
                        Invoke("changelockoncancel", 0.1f);
                    }
                    else
                    {
                        lockoncheck = false;
                        availabletargets.Clear();
                        cancellockon = false;
                        lockontarget = null;
                    }
                }
            }
        }
    }
    private void changelockoncancel()
    {
        cancellockon = true;
    }
    private void meleelockonrotation()
    {

        if (lockontarget != null && lockoncheck == true)
        {
            Vector3 lookPos = lockontarget.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);
        }

    }
    private void lockonbowrotation()
    {
        if (lockontarget != null && lockoncheck == true)
        {
            transform.rotation = Quaternion.LookRotation(lockontarget.transform.position - transform.position, Vector3.up);
        }
    }
    public void Abilitiesend()
    {
        state = State.Air;
        Statics.otheraction = false;
        Physics.IgnoreLayerCollision(9, 6, false);
        Physics.IgnoreLayerCollision(11, 6, false);
    }
    private void firedashstartmovement()
    {
        float h = move.x;                                                                         
        float v = move.y;

        moveDirection = new Vector3(h, 0, v);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * movementspeed;
        moveDirection.Normalize();

        moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;                     

        if (moveDirection != Vector3.zero)           
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);                                              
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationspeed * Time.deltaTime);
        }
    }
        private void firedashstart()
    {
        ChangeAnimationState(firedashstate);
        Physics.IgnoreLayerCollision(8, 6);
        Physics.IgnoreLayerCollision(11, 6);
        state = State.Firedash;
    }
    private void firedash()
    {
        Vector3 endposi = transform.position + (transform.forward * 20);
        Vector3 distancetomove = endposi - transform.position;
        Vector3 move = distancetomove.normalized * 70 * Time.deltaTime;
        charactercontroller.Move(move);
    }
    private void firedashdmg()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 2f, spellsdmglayer);
        foreach (Collider Enemyhit in cols)

            if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
            {
                int dmgdealed = 5;
                Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                showtext.GetComponent<TextMeshPro>().color = Color.red;
            }
    }

    private void waterpushback()
    {
        if (lockontarget != null)
        {
            float h = this.move.x;                                                                         

            moveDirection = new Vector3(h, 0, 0);
            float magnitude = Mathf.Clamp01(moveDirection.magnitude) * 10;
            moveDirection.Normalize();
            moveDirection = Quaternion.AngleAxis(CamTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;                     

            velocity = moveDirection * magnitude;
            velocity.y += graviti;
            charactercontroller.Move(velocity * Time.deltaTime);

            Transform target = lockontarget;
            Vector3 newtransformposi = transform.position;
            newtransformposi.y = transform.position.y;
            Vector3 newlockonposi = target.position;
            newlockonposi.y = transform.position.y;
            Vector3 endposi = newlockonposi + (transform.forward * -15);
            Vector3 distancetomove = endposi - newtransformposi;
            Vector3 move = distancetomove.normalized * 17 * Time.deltaTime;
            charactercontroller.Move(move);
            //transform.position = Vector3.MoveTowards(newtransformposi, newlockonposi, -17 * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.position) > 13f)
            {
                Abilitiesend();
            }
        }
        else
        {
            Abilitiesend();
        }

    }
    private void waterpushbackdmg()
    {
        if (lockontarget != null)
        {
            Collider[] cols = Physics.OverlapSphere(lockontarget.position, 2f, spellsdmglayer);
            foreach (Collider Enemyhit in cols)

                if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
                {
                    int dmgdealed = 7;
                    Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                    var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                    showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                    showtext.GetComponent<TextMeshPro>().color = Color.red;
                }
        }
    }
    private void waterintoair()
    {
        if (lockontarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, 15 * Time.deltaTime);
        }
        else
        {
            Abilitiesend();
        }
    }
    private void waterintoairdmg()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 4f, spellsdmglayer);
        foreach (Collider Enemyhit in cols)

            if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
            {
                int dmgdealed = 7;
                Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                showtext.GetComponent<TextMeshPro>().color = Color.red;
            }
    }
    private void startwaterkick()
    {
        ChangeAnimationState(waterkickstate);
        state = State.Waterkickend;
    }
    private void waterkickend()
    {
        if (lockontarget != null)
        {
            Vector3 distancetomove = lockontarget.position - transform.position;
            Vector3 move = distancetomove.normalized * 25 * Time.deltaTime;
            charactercontroller.Move(move);
            //transform.position = Vector3.MoveTowards(transform.position, lockontarget.position, 25 * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(lockontarget.transform.position - transform.position, Vector3.up);
            if (Vector3.Distance(transform.position, lockontarget.position) < 3f)
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, 4f, spellsdmglayer);
                foreach (Collider Enemyhit in cols)

                    if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
                    {
                        int dmgdealed = 10;
                        Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                        var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                        showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                        showtext.GetComponent<TextMeshPro>().color = Color.red;
                    }
                Vector3 lookPos = lockontarget.transform.position - transform.position;
                lookPos.y = 0;
                transform.rotation = Quaternion.LookRotation(lookPos);
                Abilitiesend();
            }
        }
        else Abilitiesend();
    }
    private void naturethendrilstart()
    {
        if (lockontarget != null)
        {      
            Vector3 endposi = lockontarget.transform.position + (transform.right * -8 + transform.up * 2);
            Vector3 test = endposi - transform.position;
            Vector3 move = test.normalized * 15 * Time.deltaTime;
            charactercontroller.Move(move);
            //transform.position = Vector3.MoveTowards(transform.position, endposi, 25 * Time.deltaTime);

            if (Vector3.Distance(transform.position, endposi) < 1)
            {
                startpos = transform.position;
                state = State.Naturethendrilgettotarget;
                starttime = Time.time;
            }
        }
        else
        {
            Abilitiesend();
        }
    }
    private void naturethendrilgettotarget()
    {
        Vector3 endposi = lockontarget.transform.position + (transform.forward * 3 + transform.right * 1);
        Vector3 center = (startpos + lockontarget.position) * 0.5f;

        center -= new Vector3(1, 0, 0);

        Vector3 startRelcenter = startpos - center;
        Vector3 endRelcenter = endposi - center;

        float fracComplete = (Time.time - starttime) / naturetraveltime * naturespeed;

        transform.position = Vector3.Slerp(startRelcenter, endRelcenter, fracComplete);
        transform.position += center;

        if (Vector3.Distance(transform.position, endposi)< 2)
        {
            Vector3 lookPos = lockontarget.transform.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
            Collider[] cols = Physics.OverlapSphere(lockontarget.position, 3f, spellsdmglayer);
            foreach (Collider Enemyhit in cols)

                if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
                {
                    int dmgdealed = 10;
                    Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                    var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                    showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                    showtext.GetComponent<TextMeshPro>().color = Color.red;
                }
            Abilitiesend();
        }
    }

    public void icelanceplayermovement()
    {
        if (lockontarget != null)
        {
            graviti = 0f;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, 1 * Time.deltaTime);
        }
        else
        {
            Abilitiesend();
        }
    }
    public void icelanceplayertotarget()
    {
        if (lockontarget != null)
        {
            state = State.Icelancefly;
            if (Vector3.Distance(transform.position, lockontarget.position) > 5f)
            {
                ChangeAnimationState(icelancebackflipstate);
            }
            if (Vector3.Distance(transform.position, lockontarget.position) > 3f)
            {
                transform.position = Vector3.MoveTowards(transform.position, lockontarget.position, 35 * Time.deltaTime);
            }
            else
            {
                ChangeAnimationState(icelancebackflipstate);
                state = State.Icelanceend;
            }
        }
        else
        {
            Abilitiesend();
        }
    }
    private void stormchainligthning()
    {
        if (lightningfirsttarget == null)
        {
            lightningfirsttarget = lockontarget;
        }
        if (lockontarget != null && lockontarget == lightningfirsttarget)
        { 
            Vector3 distancetomove = lightningfirsttarget.position - transform.position;
            Vector3 move = distancetomove.normalized * lightningspeed * Time.deltaTime;
            charactercontroller.Move(move);
            //transform.position = Vector3.MoveTowards(transform.position, lockontarget.position, lightningspeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, lightningfirsttarget.position) < 2f)
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, 2f, spellsdmglayer);
                foreach (Collider Enemyhit in cols)

                    if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
                    {
                        int dmgdealed = 7;
                        Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                        var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                        showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                        showtext.GetComponent<TextMeshPro>().color = Color.red;
                    }
                chainligthningenemys = Physics.CheckSphere(transform.position, 10f, lightninglayer);
                if (chainligthningenemys == true)
                {
                    Collider[] colliders = Physics.OverlapSphere(transform.position, 10, lightninglayer);

                    for (int i = 0; i < colliders.Length; i++)
                    {
                        Collider hitCollider = colliders[i];
                        if (hitCollider.gameObject != lightningfirsttarget.gameObject)
                        {
                            ligthningsecondtarget = hitCollider.transform;
                            state = State.Secondlightning;
                        }
                        if (i == colliders.Length - 1 && ligthningsecondtarget == null)
                        {
                            Abilitiesend();
                        }
                    }
                }
                else
                {
                    Abilitiesend();
                }
            }
        }
        else
        {
            Abilitiesend();
        }
    }
    private void stormchainlightningsecondtarget()
    {
        if (lockontarget != null)
        {
            Vector3 distancetomove = ligthningsecondtarget.position - transform.position;
            Vector3 move = distancetomove.normalized * lightningspeed * Time.deltaTime;
            charactercontroller.Move(move);
            //transform.position = Vector3.MoveTowards(transform.position, ligthningsecondtarget.position, lightningspeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(ligthningsecondtarget.transform.position - transform.position, Vector3.up);
            if (Vector3.Distance(transform.position, ligthningsecondtarget.position) < 2f)
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, 2f, spellsdmglayer);
                foreach (Collider Enemyhit in cols)

                    if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
                    {
                        int dmgdealed = 7;
                        Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                        var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                        showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                        showtext.GetComponent<TextMeshPro>().color = Color.red;
                    }

                chainligthningenemys = Physics.CheckSphere(transform.position, 10f, lightninglayer);
                if (chainligthningenemys == true)
                {
                    Collider[] colliders = Physics.OverlapSphere(transform.position, 10, lightninglayer);

                    for (int i = 0; i < colliders.Length; i++)
                    {
                        Collider hitCollider = colliders[i];
                        if (lightningfirsttarget != null)
                        {
                            if (hitCollider.gameObject != lightningfirsttarget.gameObject && hitCollider.gameObject != ligthningsecondtarget.gameObject)
                            {
                                lightningthirdtarget = hitCollider.transform;
                                state = State.Thirdlightning;
                            }

                            if (i == colliders.Length - 1 && lightningthirdtarget == null)
                            {
                                state = State.Endlightning;
                            }
                        }
                        else
                        {
                            if (hitCollider.gameObject != ligthningsecondtarget.gameObject)
                            {
                                lightningthirdtarget = hitCollider.transform;
                                state = State.Thirdlightning;
                            }

                            if (i == colliders.Length - 1 && lightningthirdtarget == null)
                            {
                                state = State.Endlightning;
                            }
                        }
                    }
                }
                else
                {
                    state = State.Endlightning;
                }
            }   
        }
        else
        {
            Abilitiesend();
        }
    }

    private void stormchainlightningthirdtarget()
    {
        if (lockontarget != null)
        {
            Vector3 distancetomove = lightningthirdtarget.position - transform.position;
            Vector3 move = distancetomove.normalized * lightningspeed * Time.deltaTime;
            charactercontroller.Move(move);
            //transform.position = Vector3.MoveTowards(transform.position, lightningthirdtarget.position, lightningspeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(lightningthirdtarget.transform.position - transform.position, Vector3.up);
            if (Vector3.Distance(transform.position, lightningthirdtarget.position) < 1f)
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, 2f, spellsdmglayer);
                foreach (Collider Enemyhit in cols)

                    if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
                    {
                        int dmgdealed = 7;
                        Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                        var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                        showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                        showtext.GetComponent<TextMeshPro>().color = Color.red;
                    }
                state = State.Endlightning;
            }
        }
        else
        {
            Abilitiesend();
        }
    }
    private void stormlightningbacktomain()
    {
        if (lockontarget != null && lockontarget == lightningfirsttarget)
        {
            Vector3 distancetomove = lockontarget.position - transform.position;
            Vector3 move = distancetomove.normalized * lightningspeed * Time.deltaTime;
            charactercontroller.Move(move);
            //transform.position = Vector3.MoveTowards(transform.position, lockontarget.position, lightningspeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(lockontarget.transform.position - transform.position, Vector3.up);
            if (Vector3.Distance(transform.position, lockontarget.position) < 1f)
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, 2f, spellsdmglayer);
                foreach (Collider Enemyhit in cols)

                    if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
                    {
                        int dmgdealed = 7;
                        Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                        var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                        showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                        showtext.GetComponent<TextMeshPro>().color = Color.red;
                    }
                Abilitiesend();
            }
        }
        else
        {
            Abilitiesend();
        }
    }
    private void usedarkportal()
    {
        if (lockontarget != null)
        {
            transform.position = lockontarget.position + new Vector3(0, 10, 0) + (transform.forward * -2);
            ChangeAnimationState(darkportalendstate);
            airattackminheight = true;
        }
        else
        {
            Abilitiesend();
        }
    }

    public void darkportalending()
    {
        state = State.Darkportalend;
        graviti = -17;
        velocity = new Vector3(0, 0, 0);
        velocity.y += graviti;
        charactercontroller.Move(velocity * Time.deltaTime);

        Ray ray = new Ray(this.transform.position + Vector3.up * 0.3f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.4f))
        {
            airattackminheight = false;
        }
        if (airattackminheight == false)
        {
            state = State.Airintoground;
            Statics.otheraction = false;
            Collider[] cols = Physics.OverlapSphere(transform.position, 2f, spellsdmglayer);
            foreach (Collider Enemyhit in cols)

                if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
                {
                    int dmgdealed = 10;
                    Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                    var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                    showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                    showtext.GetComponent<TextMeshPro>().color = Color.red;
                }
        }
    }
    private void earthslidestart()
    {
        if (lockontarget != null)
        {
            state = State.Earthslide;
            if (Vector3.Distance(transform.position, lockontarget.position) > 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, lockontarget.position, earthslidespeed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(lockontarget.transform.position - transform.position, Vector3.up);
            }          
            if (Vector3.Distance(transform.position, lockontarget.position) < 9f)
            {
                ChangeAnimationState(earthslidereleasestate);
            }
        }
        else
        {
            Abilitiesend();
        }
    }
    private void earthslidedmg()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 2f, spellsdmglayer);
        foreach (Collider Enemyhit in cols)

            if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
            {
                int dmgdealed = 15;
                Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                showtext.GetComponent<TextMeshPro>().color = Color.red;
                state = State.Abilitiesempty;
            }
    }
}


/*
private void waterpushback()
{
    if (lockontarget != null)
    {
        Transform target = lockontarget;
        Vector3 newtransformposi = transform.position;
        newtransformposi.y = transform.position.y;
        Vector3 newlockonposi = target.position;
        newlockonposi.y = transform.position.y;
        Vector3 endposi = newlockonposi + (transform.forward * -15);
        Vector3 distancetomove = endposi - newtransformposi;
        Vector3 move = distancetomove.normalized * 17 * Time.deltaTime;
        controller.Move(move);
        //transform.position = Vector3.MoveTowards(newtransformposi, newlockonposi, -17 * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) > 12f)
        {
            ChangeAnimationState(waterintoairstate);
            state = State.Waterintoair;
        }
    }
    else
    {
        Abilitiesend();
    }

}
private void waterpushbackdmg()
{
    if (lockontarget != null)
    {
        Collider[] cols = Physics.OverlapSphere(lockontarget.position, 2f, Dmglayer);
        foreach (Collider Enemyhit in cols)

            if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
            {
                int dmgdealed = 7;
                Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                showtext.GetComponent<TextMeshPro>().color = Color.red;
            }
    }
}

/*private void waterintoair()
{
    if (lockontarget != null)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, 15 * Time.deltaTime);
    }
    else
    {
        Abilitiesend();
    }
}
private void waterintoairdmg()
{
    Collider[] cols = Physics.OverlapSphere(transform.position, 4f, Dmglayer);
    foreach (Collider Enemyhit in cols)

        if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
        {
            int dmgdealed = 7;
            Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
            var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
            showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
            showtext.GetComponent<TextMeshPro>().color = Color.red;
        }
}
private void startwaterkick()
{
    ChangeAnimationState(waterkickstate);
    state = State.Waterkickend;
}
private void waterkickend()
{
    if (lockontarget != null)
    {
        Vector3 distancetomove = lockontarget.position - transform.position;
        Vector3 move = distancetomove.normalized * 25 * Time.deltaTime;
        controller.Move(move);
        //transform.position = Vector3.MoveTowards(transform.position, lockontarget.position, 25 * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(lockontarget.transform.position - transform.position, Vector3.up);
        if (Vector3.Distance(transform.position, lockontarget.position) < 3f)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, 4f, Dmglayer);
            foreach (Collider Enemyhit in cols)

                if (Enemyhit.gameObject.GetComponent<Checkforhitbox>())
                {
                    int dmgdealed = 10;
                    Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().TakeDamage(dmgdealed);
                    var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                    showtext.GetComponent<TextMeshPro>().text = dmgdealed.ToString();
                    showtext.GetComponent<TextMeshPro>().color = Color.red;
                }
            Vector3 lookPos = lockontarget.transform.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
            Abilitiesend();
        }
    }
    else Abilitiesend();
}*/

/*private Vector3 VelocityUneben(Vector3 velocity)                     // alte slidewallfunktion
{
    Ray nachunten = new Ray(transform.position + Vector3.up * 0.3f, Vector3.down);          // sonst geht der ray durch den boden
    if (Physics.Raycast(nachunten, out RaycastHit nachunteninfo, 1.3f))
    {
        Slidedownwalls = nachunteninfo.normal;
        if (Vector3.Angle(Slidedownwalls, Vector3.up) > controller.slopeLimit + 5 && Vector3.Angle(Slidedownwalls, Vector3.up) < 89.9f)                 // wenn ich jump kann ich noch auf bis zu 45winkel stehen
        {
            ChangeAnimationState(idlestate);
            state = State.Slidedownwall;
        }
    }
    else
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.3f, transform.forward + Vector3.down);          // sonst geht der ray durch den boden
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1f))
        {
            Slidedownwalls = hitInfo.normal;
            if (Vector3.Angle(Slidedownwalls, Vector3.up) > controller.slopeLimit + 5 && Vector3.Angle(Slidedownwalls, Vector3.up) < 89.9f)                 // wenn ich jump kann ich noch auf bis zu 45winkel stehen
            {
                ChangeAnimationState(idlestate);
                state = State.Slidedownwall;
            }
        }
    }
    Ray uneben = new Ray(this.transform.position + Vector3.up * 0.3f, Vector3.down);
    if (Physics.Raycast(uneben, out RaycastHit unebeninfo, 1f))
    {
        Quaternion bodensteigung = Quaternion.FromToRotation(Vector3.up, unebeninfo.normal);
        Vector3 Velocitysteigung = bodensteigung * velocity;
        if (Velocitysteigung.y < 0)
        {
            return Velocitysteigung;
        }

    }
    return velocity;
}
    /*private void slidedownwalls()
    {
        ChangeAnimationState(idlestate);
        //Ray nachunten = new Ray(transform.position, Vector3.down);
        //if (Physics.Raycast(nachunten, out RaycastHit hitInfo, 0.2f))
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.3f, transform.forward + Vector3.down);          // sonst geht der ray durch den boden
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.8f))
        {
            float angle = Vector3.Angle(Slidedownwalls, Vector3.up);
            float math = 90 - angle;
            if (angle > 70)
            {
                Slidedownwalls = hitInfo.normal;
           
                moveDirection = new Vector3(Slidedownwalls.x * (1.5f / angle * math), -Slidedownwalls.y * angle, Slidedownwalls.z * (1.5f / angle * math)) * 15;
            }
            else if (angle > controller.slopeLimit)
            {
                Slidedownwalls = hitInfo.normal;

                moveDirection = new Vector3(Slidedownwalls.x * (1.5f / angle * math), -Slidedownwalls.y * angle, Slidedownwalls.z * (1.5f / angle * math)) * 10;
            }
            else
            {
                state = State.Ground;
            }
        }
        else
        {
            state = State.Ground;
        }
        controller.Move(moveDirection * Time.deltaTime);
    }*/

/*if (LoadCharmanager.disableattackbuttons == false || LoadCharmanager.gameispaused == false)
{
        Checkforenemy = Physics.CheckSphere(transform.position, lockonrange, Lockonlayer);
    if (Checkforenemy == true)
    {
        float shortestDistance = 100f;                                                          // Value um später if (distancefromtarget) für einen frame zu resten, der wert muss immer höher sein als distanceformtarget, deswegen infinity
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
                lockontarget = availabletargets[t].lockontransform;
                shortestDistance = distancefromtarget;
                if (Steuerung.Player.Lockon.WasPressedThisFrame() && lockoncheck == false)
                {
                    LockonUI.SetActive(true);
                    shortestDistance = distancefromtarget;
                    lockontarget = availabletargets[t].lockontransform;
                    lockoncheck = true;
                    Invoke("changelockoncancel", 0.1f);
                }
                if (Steuerung.Player.Lockonchange.WasReleasedThisFrame() && lockoncheck == true || EnemyHP.switchtargetafterdeath == true && lockoncheck == true || bowair3intoground == true)
                {
                    LockonUI.SetActive(true);
                    EnemyHP.switchtargetafterdeath = false;
                    shortestDistance = distancefromtarget;
                    lockontarget = availabletargets[t].lockontransform;
                    Invoke("changelockoncancel", 0.1f);
                }
            }
        }
    }*/


