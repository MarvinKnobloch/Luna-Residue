using System.Collections;
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Movescript : MonoBehaviour
{
    //Bugs:
    //Stormlightning flug animation hat sich am ende nicht verändert (zu der Zeit war der Char Toggle Active State = true)
    [SerializeField] internal Swordattack Weaponslot1script;
    [SerializeField] internal Bowattack Weaponslot2script;
    [SerializeField] internal AimScript aimscript;

    [NonSerialized] public CharacterController charactercontroller;
    [NonSerialized] public SpielerSteu controlls;
    private InputAction buttonmashhotkey;

    private Animator animator;

    public Transform CamTransform;
    public CinemachineFreeLook Cam1;
    public CinemachineVirtualCamera Cam2;

    public GameObject Charprefabarrow;
    public GameObject dazeimage;

    [NonSerialized] public Vector2 move;
    [NonSerialized] public Vector3 moveDirection;
    [NonSerialized] public Vector3 velocity;
    public float movementspeed;
    [NonSerialized] public float normalmovementspeed;
    public float rotationspeed;
    public float jumpheight;
    public float gravitation;
    public float normalgravition = 3.5f;
    public float graviti;
    [NonSerialized] public float maxgravity = -15;
    private float originalStepOffSet;

    public SphereCollider spherecastcollider;
    public LayerMask groundchecklayer;

    //attack abfragen
    [NonSerialized] public float attackmovementspeed = 1;
    [NonSerialized] public float attackrotationspeed = 100;

    [NonSerialized] public bool amBoden;
    [NonSerialized] public bool inderluft;
    [NonSerialized] public bool airattackminheight;
    [NonSerialized] public bool attackonceair;
    [NonSerialized] public bool bowair3intoground;                                 // für lockon
    //private float jumpcdafterland;
    //private float jumpcd = 0.2f;
    [NonSerialized] public bool hook;
    [NonSerialized] public bool attack3intoair;
    [NonSerialized] public bool fullcharge;

    //swim
    public GameObject spine;
    public LayerMask swimlayer;

    //Characterrig
    public MonoBehaviour Charrig;
    public bool activaterig;

    //StatemachineScripts
    public Playermovement playermovement = new Playermovement();
    private Playerair playerair = new Playerair();
    private Playerheal playerheal = new Playerheal();
    private Playerslidewalls playerslidewalls = new Playerslidewalls();
    private Playerswim playerswim = new Playerswim();
    private Playerstun playerstun = new Playerstun();
    private Playerattack playerattack = new Playerattack();
    private Playerlockon playerlockon = new Playerlockon();
    private Playerfire playerfire = new Playerfire();
    private Playerwater playerwater = new Playerwater();

    //animationstate
    public string currentstate;
    const string jumpstate = "Jump";
    const string idlestate = "Idle";
    const string runstate = "Running";
    const string fallstate = "Fall";
    const string healstart = "Healstart";
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
    public float lockonrange;
    public static bool lockoncheck;
    [NonSerialized] public bool Checkforenemy;
    public static Transform lockontarget;
    [NonSerialized] public GameObject HealUI;
    [NonSerialized] public Enemylockon Enemylistcollider;
    public static List<Enemylockon> availabletargets = new List<Enemylockon>();
    public bool cancellockon;
    [NonSerialized] public Transform targetbeforeswap;

    //Spells
    public Healingscript healingscript;
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
        Slidedownwall,
        Swim,
        Heal,
        Stun,
        Buttonmashstun,
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
        Dash,
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
        buttonmashhotkey = controlls.Player.Attack3;
        charactercontroller = GetComponent<CharacterController>();
        originalStepOffSet = charactercontroller.stepOffset;
        animator = GetComponent<Animator>();
        healingscript = GetComponent<Healingscript>();
        state = State.Air;
        starttime = Time.time;
        Statics.normalgamespeed = 1;
        Statics.normaltimedelta = Time.fixedDeltaTime;
        normalmovementspeed = movementspeed;

        playermovement.psm = this;
        playerair.psm = this;
        playerheal.psm = this;
        playerslidewalls.psm = this;
        playerswim.psm = this;
        playerstun.psm = this;
        playerattack.psm = this;
        playerlockon.psm = this;
        playerfire.psm = this;
        playerwater.psm = this;
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

    private void Update()
    {
        playerlockon.charlockon();
        switch (state)
        {
            default:
            case State.Ground:
                playermovement.movement();
                playermovement.groundcheck();
                playermovement.groundanimations();
                playermovement.jump();
                playerheal.starthealing();

                break;
            case State.Air:
                playermovement.movement();
                playerair.airgravity();
                playerair.minheightforairattack();
                break;
            case State.Slidedownwall:
                playerslidewalls.slidewalls();
                break;
            case State.Heal:
                healingscript.heal();
                break;
            case State.Swim:
                playermovement.movement();
                playerswim.swim();
                playermovement.jump();
                break;
            case State.Stun:
                playerstun.stun();
                break;
            case State.Buttonmashstun:
                playerstun.stun();
                playerstun.breakstunwithbuttonmash();
                break;
            case State.Dash:
                break;
            case State.Groundattack:
                playerattack.attackmovement();
                playermovement.groundcheck();
                playerlockon.attacklockonrotation();
                //meleelockonrotation();
                //Groundedattack();
                break;
            case State.Airattack:
                playerattack.attackmovement();
                playerlockon.attacklockonrotation();
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
            case State.BowGroundattack:
                Bowgroundmovement();
                lockonbowrotation();
                Groundedattack();
                break;
            case State.BowAirattack:
                Bowgroundmovement();
                break;
            case State.Bowweaponswitch:
                bowswitch();
                break;
            case State.Bowhookshot:
                Bowhookshot();
                //Minhighforairattack();
                break;
            case State.Beforedash:           //damit man beim angreifen noch die Richtung bestimmen kann
                beforedashmovement();
                break;
            case State.Firedashstart:
                playerfire.firedashstartmovement();
                break;
            case State.Firedash:
                playerfire.firedash();
                break;
            case State.Waterpushback:
                playerwater.waterpushback();
                break;
            case State.Waterintoair:
                playerwater.waterintoair();
                break;
            case State.Waterkickend:
                playerwater.waterkickend();
                break;
            case State.Naturethendril:
                naturethendrilstart();
                break;
            case State.Naturethendrilgettotarget:
                naturethendrilgettotarget();
                break;
            case State.Icelancestart:
                icelanceplayermovement();
                break;
            case State.Icelancefly:
                icelanceplayertotarget();
                break;
            case State.Icelanceend:
                break;
            case State.Stormchainligthning:
                stormchainligthning();
                break;
            case State.Secondlightning:
                stormchainlightningsecondtarget();
                break;
            case State.Thirdlightning:
                stormchainlightningthirdtarget();
                break;
            case State.Endlightning:
                stormlightningbacktomain();
                break;
            case State.Darkportalend:
                darkportalending();
                break;
            case State.Earthslide:
                earthslidestart();
                break;
            case State.Abilitiesempty:
                break;
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
    public void switchtogroundstate()
    {
        amBoden = true;
        graviti = -0.5f;
        state = State.Ground;
    }
    public void switchtoairstate()
    {
        amBoden = false;
        state = State.Air;
    }
    public void slowplayer(float slowmovementspeed)
    {
        movementspeed = slowmovementspeed;
        state = State.Ground;
    }
    public void switchtostun()
    {
        ChangeAnimationStateInstant(dazestate);
        state = State.Stun;
        Statics.dash = true;
        Statics.dazestunstart = true;
    }
    public void switchtobuttonmashstun(int buttonmashcount)
    {
        ChangeAnimationStateInstant(dazestate);
        state = State.Buttonmashstun;
        dazeimage.SetActive(true);
        dazeimage.GetComponentInChildren<Text>().text = "Spam " + buttonmashhotkey.GetBindingDisplayString();
        Statics.dazestunstart = true;
        Statics.dazecounter = 0;
        Statics.dazekicksneeded = buttonmashcount;
        Statics.dash = true;
    }
    public void activatedmgtext(GameObject enemyhit, float dmg)
    {
        var showtext = Instantiate(damagetext, enemyhit.transform.position, Quaternion.identity);
        showtext.GetComponent<TextMeshPro>().text = dmg.ToString();
        showtext.GetComponent<TextMeshPro>().color = Color.red;
    }

    public void elefiredashstart() => playerfire.firedashstart();
    public void elefiredashdmg() => playerfire.firedashdmg();

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
    public void jumppad(float jumpheight)
    {
        //state = State.Jump;
        amBoden = false;
        ChangeAnimationState(jumpstate);
        float gravity = Physics.gravity.y * gravitation;
        graviti = Mathf.Sqrt(jumpheight * -3 * gravity);
        graviti = jumpheight;
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
            //velocity = VelocityUneben(velocity);
        }
        velocity.y += graviti;

        charactercontroller.Move(velocity / attackmovementspeed * Time.deltaTime);

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
            //velocity = VelocityUneben(velocity);
        }
        velocity.y += graviti;

        charactercontroller.Move(velocity / attackmovementspeed * Time.deltaTime);
    }
    private void airintoground()
    {
        charactercontroller.stepOffset = originalStepOffSet;
        amBoden = true;
        inderluft = false;
        attack3intoair = false;
        attackonceair = true;
        //jumpcdafterland = 0f;
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
            //velocity = VelocityUneben(velocity);
        }
        else
        {
        }
        velocity.x = velocity.x / attackmovementspeed;
        velocity.z = velocity.z / attackmovementspeed;
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
            //velocity = VelocityUneben(velocity);
        }
        else
        {
            /*Charrig.enabled = false;    //Grounded händelt den wechsel
            aimscript.virtualcam = false;
            aimscript.aimend();
            Statics.otheraction = false;
            state = State.Air;*/
        }
        velocity.x = velocity.x / attackmovementspeed;
        velocity.z = velocity.z / attackmovementspeed;
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
        //values müssen noch zurückgesetzt werden?????????
        Statics.otheraction = false;
        Physics.IgnoreLayerCollision(9, 6, false);
        Physics.IgnoreLayerCollision(11, 6, false);
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


