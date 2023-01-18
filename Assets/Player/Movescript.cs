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
    //bei jeglichen spells, wenn das target stirbt, ist das target dann nicht gleich null, weil eine neues traget gesucht wird, wenn eins vorhanden ist
    //playerhits im enemyscript muss upgedatet werden, wenn man support chars angewählt/habgewählt werden        ??? muss playerhit überhaupt abgewählt werden wenn die chars deaktiviert werden
    //kamera nach dem lightport in spieler guck richtung?

    //Stormlightning flug animation hat sich am ende nicht verändert (zu der Zeit war der Char Toggle Active State = true)

    [NonSerialized] public CharacterController charactercontroller;
    [NonSerialized] public SpielerSteu controlls;
    private InputAction buttonmashhotkey;

    [NonSerialized] public Animator animator;

    public Transform CamTransform;
    public CinemachineFreeLook Cam1;
    public CinemachineVirtualCamera Cam2;

    public GameObject Charprefabarrow;
    public GameObject dazeimage;

    [NonSerialized] public Vector2 move;
    [NonSerialized] public Vector3 moveDirection;
    [NonSerialized] public Vector3 velocity;
    public float movementspeed;
    public float rotationspeed;
    public float jumpheight;
    public float gravitation;
    public float normalgravition = 3.5f;
    public float graviti;
    [NonSerialized] public float maxgravity = -15;

    public SphereCollider spherecastcollider;
    public LayerMask groundchecklayer;

    //attack abfragen
    [NonSerialized] public float attackmovementspeed = 2;
    [NonSerialized] public float attackrotationspeed = 100;
    [NonSerialized] public float lockonrotationspeed = 10;
    [NonSerialized] public bool airattackminheight;
    [NonSerialized] public bool attackonceair;
    [NonSerialized] public bool hook;
    [NonSerialized] public bool fullcharge;

    //swim
    public GameObject spine;
    public LayerMask swimlayer;

    //playeraim
    [NonSerialized] public float xrotation = 0f;
    [NonSerialized] public float aimrotationspeed = 10;
    public GameObject mousetarget;

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
    private Playerbow playerbow = new Playerbow();
    public Playeraim playeraim = new Playeraim();
    public Playerlockon playerlockon = new Playerlockon();
    private Playerfire playerfire = new Playerfire();
    private Playerwater playerwater = new Playerwater();
    private Playernature playernature = new Playernature();
    private Playerice playerice = new Playerice();
    private Playerlightning playerlightning = new Playerlightning();
    private Playerdark playerdark = new Playerdark();
    private Playerearth playerearth = new Playerearth();

    //animationstate
    public string currentstate;
    const string idlestate = "Idle";
    const string dazestate = "Daze";

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
    [NonSerialized] public Transform targetbeforeswap;

    //Spells
    public Healingscript healingscript;
    public LayerMask spellsdmglayer;
    public GameObject damagetext;
    [NonSerialized] public Vector3 startpos;
    [NonSerialized] public float starttime;
    [NonSerialized] public float nature1speed = 2;
    [NonSerialized] public float nature1traveltime = 1;
    [NonSerialized] public float icelancespeed = 30;
    [NonSerialized] public float lightningspeed = 10;
    [NonSerialized] public Transform currentlightningtraget;
    [NonSerialized] public Transform lightningfirsttarget;
    [NonSerialized] public Transform ligthningsecondtarget;
    [NonSerialized] public Transform lightningthirdtarget;
    [NonSerialized] public float earthslidespeed = 20;

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
        Bowwaitfornewcharge,
        Groundattack,
        Airattack,
        Attackweaponaim,
        Bowweaponswitch,
        Bowhookshot,
        Beforedash,
        Firedashstart,
        Firedash,
        Waterpushback,
        Waterintoair,
        Waterkickend,
        Naturethendril,
        Naturethendrilgettotarget,
        Icelancestart,
        Icelancefly,
        Stormchainligthning,
        Secondlightning,
        Thirdlightning,
        Endlightning,
        Darkportalend,
        Earthslide,
        Empty,
    }

    void Awake()
    {
        lockonrange = Statics.playerlockonrange;
        Charrig.enabled = false;
        lockoncheck = false;
        controlls = Keybindinputmanager.inputActions;
        controlls.Player.Movement.performed += Context => move = Context.ReadValue<Vector2>();
        buttonmashhotkey = controlls.Player.Attack3;
        charactercontroller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        healingscript = GetComponent<Healingscript>();
        state = State.Air;
        starttime = Time.time;
        Statics.normalgamespeed = 1;
        Statics.normaltimedelta = Time.fixedDeltaTime;

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
        playernature.psm = this;
        playerice.psm = this;
        playerlightning.psm = this;
        playerdark.psm = this;
        playerearth.psm = this;
        playeraim.psm = this;
        playerbow.psm = this;
    }
    private void OnEnable()
    {
        Cam2.gameObject.SetActive(false);
        controlls.Enable();
        graviti = -0.5f;
        gravitation = normalgravition;
        attackonceair = true;
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
            case State.Groundattack:
                playerattack.attackmovement();
                playermovement.groundcheck();
                playerlockon.attacklockonrotation();
                break;
            case State.Airattack:
                playerattack.attackmovement();
                playerlockon.attacklockonrotation();
                playerattack.finalairmovement();
                break;
            case State.Bowcharge:
                playeraim.aimplayerrotation();
                playerbow.chargearrow();
                playerbow.bowoutofcombataimmovement();
                playermovement.groundcheck();
                break;
            case State.Bowischarged:
                playeraim.aimplayerrotation();
                playerbow.shootarrow();
                playerbow.bowoutofcombataimmovement();
                playermovement.groundcheck();
                break;
            case State.Bowwaitfornewcharge:
                playerbow.bowoutofcombataimmovement();
                break;
            case State.Attackweaponaim:
                playeraim.aimplayerrotation();
                playerattack.attackmovement();
                playerattack.finalairmovement();
                break;
            case State.Bowweaponswitch:
                playerattack.attackmovement();
                playerattack.finalairmovement();
                break;
            case State.Bowhookshot:
                playerbow.bowhookshot();
                break;
            case State.Beforedash:           //damit man beim angreifen noch die Richtung bestimmen kann
                playermovement.beforedashmovement();
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
                playernature.naturethendrilstart();
                break;
            case State.Naturethendrilgettotarget:
                playernature.naturethendrilgettotarget();
                break;
            case State.Icelancestart:
                playerice.icelanceplayermovement();
                break;
            case State.Icelancefly:
                playerice.icelanceplayertotarget();
                break;
            case State.Stormchainligthning:
                playerlightning.stormchainligthning();
                break;
            case State.Endlightning:
                playerlightning.stormlightningbacktomain();
                break;
            case State.Darkportalend:
                playerdark.darkportalending();
                break;
            case State.Earthslide:
                playerearth.earthslidestart();
                break;
            case State.Empty:
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
    public void lockontargetswitch() => playerlockon.lookfortarget();
    public void switchtogroundstate()
    {
        ChangeAnimationState(idlestate);
        attackonceair = true;
        graviti = -0.5f;
        state = State.Ground;
    }
    public void switchtoairstate()
    {
        gravitation = normalgravition;
        state = State.Air;
    }
    public void switchtoattackaimstate()
    {
        CinemachinePOV Cam2pov = Cam2.GetCinemachineComponent<CinemachinePOV>();
        Cam2pov.m_HorizontalRecentering.m_enabled = true;
        playeraim.activateaimcam();
        state = State.Attackweaponaim;
        StartCoroutine(cam2recenteringdisable());
    }
    IEnumerator cam2recenteringdisable()                     //damit die cam2 bei aktivierung mit cam1 gleich gesetzt wird. Wenn Recentering aktiviert wird passt sich die 2. Cam sofort der rotation des spielers an.
    {                                                        //die rotation des spielers ist beim wechsel der Kamera die richtung des Kamerabrains(der Char dreht sich richtung Kamera)
        yield return new WaitForSeconds(0.2f);               //wenn Recentering nicht aktviert ist dreht die der Char nach dem wechsel sofort richtung 2. Cam die noch nicht die richtig position hat 
        CinemachinePOV Cam2pov = Cam2.GetCinemachineComponent<CinemachinePOV>();             //sprich die 2. Cam recentert sich schneller als die Char rotation
        Cam2pov.m_HorizontalRecentering.m_enabled = false;
    }
    public void disableaimcam()
    {
        Charprefabarrow.SetActive(false);
        playeraim.aimend();
        switchtoairstate();
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
    public void switchtooutofcombataim()
    {
        CinemachinePOV Cam2pov = Cam2.GetCinemachineComponent<CinemachinePOV>();
        Cam2pov.m_HorizontalRecentering.m_enabled = true;
        playeraim.activateaimcam();
        StartCoroutine(cam2recenteringdisable());
        Charrig.enabled = true;
        state = State.Bowcharge;
    }
    private void bowoutofcombatfullcharged() => playerbow.arrowfullcharge();
    private void bowoutofcombatnextarrow() => playerbow.nextarrow();
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
    public void elewaterpushbackdmg() => playerwater.waterpushbackdmg();
    public void elewaterintoairdmg() => playerwater.waterintoairdmg();
    public void elestarticelancefly() => playerice.starticelancefly();
    public void eleusedarkportal() => playerdark.usedarkportal();
    public void eleearthslidestart() => playerearth.earthslidestart();
    public void eleearthslidedmg() => playerearth.earthslidedmg();
    public void Abilitiesend()
    {
        state = State.Air;
        Statics.otheraction = false;
        Physics.IgnoreLayerCollision(9, 6, false);
        Physics.IgnoreLayerCollision(11, 6, false);
    }
    public void pushplayerup(float amount) => playermovement.pushplayerupwards(amount);
}


