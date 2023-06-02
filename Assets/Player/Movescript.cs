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
    //kamera nach dem lightport in spieler guck richtung?

    //Stormlightning flug animation hat sich am ende nicht verändert (zu der Zeit war der Char Toggle Active State = true)

    [NonSerialized] public CharacterController charactercontroller;
    [NonSerialized] public SpielerSteu controlls;
    private InputAction buttonmashhotkey;

    [NonSerialized] public Animator animator;
    [NonSerialized] public Playerhp playerhp;
    [NonSerialized] public Attributecontroller attributecontroller;

    public Transform CamTransform;
    public CinemachineFreeLook Cam1;
    public CinemachineVirtualCamera Cam2;

    public GameObject Charprefabarrow;
    public GameObject dazeimage;

    [NonSerialized] public Vector2 move;
    [NonSerialized] public Vector3 moveDirection;
    [NonSerialized] public Vector3 velocity;
    public float movementspeed = Statics.playermovementspeed;
    public float rotationspeed = Statics.playerroationspeed;
    public float jumpheight = Statics.playerjumpheight;
    public float gravitation;
    public float normalgravition = Statics.playergravity;
    public float graviti;
    [NonSerialized] public float maxgravity = -35;

    public SphereCollider spherecastcollider;
    public LayerMask groundchecklayer;

    //attack abfragen
    [NonSerialized] public float attackmovementspeed = 2;
    [NonSerialized] public float attackrotationspeed = 100;
    [NonSerialized] public float lockonrotationspeed = 10;
    public int attackcombochain;
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

    //attributebonus
    public TextMeshProUGUI bonuscritstackstext;

    //Characterrig
    public MonoBehaviour Charrig;

    //StatemachineScripts
    public Playermovement playermovement = new Playermovement();
    public Playerair playerair = new Playerair();
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
    private Playerstorm playerstorm = new Playerstorm();
    private Playerdark playerdark = new Playerdark();
    private Playerearth playerearth = new Playerearth();
    private Playerutility playerutility = new Playerutility();

    //animationstate
    public string currentstate;
    const string idlestate = "Idle";
    const string dazestate = "Daze";

    //audio
    public Playersounds playersounds;

    //Lockon
    public static Transform lockontarget;
     public GameObject focustargetui;
    [NonSerialized] public Transform targetbeforeswap;

    //Spells
    public Healingscript healingscript;
    public EleAbilities eleAbilities;
    public LayerMask spellsdmglayer;
    public GameObject damagetext;
    [NonSerialized] public float spellmaxtime = 2;
    [NonSerialized] public float spelltimer;
    [NonSerialized] public float starttime;
    [NonSerialized] public float waterpushbacktime;
    [NonSerialized] public Vector3 nature1startpos;
    [NonSerialized] public Vector3 nature1endpos;
    [NonSerialized] public float nature1speed = 1.9f;
    [NonSerialized] public float nature1traveltime = 1;
    [NonSerialized] public float icelancespeed = 25;
    [NonSerialized] public float maxlightningspeed = 30;
    [NonSerialized] public float lightningspeed;
    [NonSerialized] public Transform currentlightningtarget;
    [NonSerialized] public Transform lightningfirsttarget;
    [NonSerialized] public Transform ligthningsecondtarget;
    [NonSerialized] public Transform lightningthirdtarget;
    [NonSerialized] public float darkspeedmultipler;
    [NonSerialized] public float darkportalspeed = 15;
    [NonSerialized] public float earthslidespeed = 20;

    //puzzle
    [NonSerialized] public Vector3 movetowardsposition;
    [NonSerialized] public float movetowardsspeed;

    public State state;
    public enum State
    {
        Ground,
        Movetowards,
        Upwards,
        Air,
        Slidedownwall,
        Swim,
        Heal,
        Stun,
        Dead,
        Buttonmashstun,
        Outofcombarbowcharge,
        Outofcombatbowischarged,
        Outofcombatbowwaitfornewcharge,
        Meleegroundattack,
        Meleeairattack,
        Rangegroundattack,
        Attackweaponaim,
        Bowweaponswitch,
        Bowhookshot,
        Beforedash,
        Firedashstart,
        Firedash,
        Waterpushback,
        Waterkick,
        Naturethendril,
        Naturethendrilgettotarget,
        Naturethendrilfinalmovement,
        Icelancestart,
        Icelancefly,
        Stormchainligthning,
        Secondlightning,
        Thirdlightning,
        Endlightning,
        Darkportalstart,
        Darkportalend,
        Earthslide,
        Gatheritem,
        Empty,
    }

    void Awake()
    {
        Charrig.enabled = false;
        controlls = Keybindinputmanager.inputActions;
        controlls.Player.Movement.performed += Context => move = Context.ReadValue<Vector2>();
        buttonmashhotkey = controlls.Player.Attack3;
        charactercontroller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        healingscript = GetComponent<Healingscript>();
        playerhp = GetComponent<Playerhp>();
        eleAbilities = GetComponent<EleAbilities>();
        attributecontroller = GetComponent<Attributecontroller>();
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
        playerstorm.psm = this;
        playerdark.psm = this;
        playerearth.psm = this;
        playeraim.psm = this;
        playerbow.psm = this;
        playerutility.psm = this;
    }
    private void OnEnable()
    {
        Cam2.gameObject.SetActive(false);
        controlls.Enable();
        gravitation = normalgravition;
        attackonceair = true;
        Charprefabarrow.SetActive(false);
        currentstate = null;
    }
    private void Update()
    {
        if (LoadCharmanager.interaction == false)
        {
            playerlockon.whilelockon();
            switch (state)
            {
                default:
                case State.Ground:
                    switchmovespeed();
                    playermovement.movement();
                    playermovement.groundcheck();
                    playermovement.groundanimations();
                    playerair.jump();
                    playerheal.starthealing();
                    break;
                case State.Movetowards:
                    playermovement.movetowardsposi();
                    break;
                case State.Upwards:
                    playermovement.movement();
                    playerair.airgravity();
                    playerair.minheightforairattack();
                    playerair.switchformupwardstoair();
                    break;
                case State.Air:
                    playermovement.movement();
                    playerair.airgravity();
                    playerair.minheightforairattack();
                    playerair.airdownwards();
                    break;
                case State.Slidedownwall:
                    playerslidewalls.slidewalls();
                    break;
                case State.Heal:
                    healingscript.heal();
                    playermovement.healgroundcheck();
                    break;
                case State.Swim:
                    playermovement.movement();
                    playerswim.swim();
                    playerair.jump();
                    break;
                case State.Stun:
                    playerstun.stun();
                    break;
                case State.Dead:
                    playermovement.deadmovement();
                    break;
                case State.Buttonmashstun:
                    playerstun.stun();
                    playerstun.breakstunwithbuttonmash();
                    break;
                case State.Meleegroundattack:
                    playerattack.attackmovement();
                    playermovement.groundcheck();
                    playerlockon.attacklockonrotation();
                    break;
                case State.Meleeairattack:
                    playerattack.attackmovement();
                    playerlockon.attacklockonrotation();
                    playerattack.finalairmovement();
                    break;
                case State.Rangegroundattack:
                    playerattack.inputattackmovement();
                    playermovement.groundcheck();
                    playerlockon.attacklockonrotation();
                    break;
                case State.Attackweaponaim:
                    playeraim.aimplayerrotation();
                    playerattack.inputattackmovement();
                    playerattack.finalairmovement();
                    break;
                case State.Bowweaponswitch:
                    playerattack.inputattackmovement();
                    playerattack.finalairmovement();
                    break;
                case State.Bowhookshot:
                    playerbow.bowhookshot();
                    break;
                case State.Outofcombarbowcharge:
                    playeraim.aimplayerrotation();
                    playerbow.chargearrow();
                    playerbow.bowoutofcombataimmovement();
                    playermovement.groundcheck();
                    break;
                case State.Outofcombatbowischarged:
                    playeraim.aimplayerrotation();
                    playerbow.shootarrow();
                    playerbow.bowoutofcombataimmovement();
                    playermovement.groundcheck();
                    break;
                case State.Outofcombatbowwaitfornewcharge:
                    playerbow.bowoutofcombataimmovement();
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
                case State.Waterkick:
                    playerwater.waterkickend();
                    playerutility.checkspellmaxtime();
                    break;
                case State.Naturethendril:
                    playernature.naturethendrilstart();
                    playerutility.checkspellmaxtime();
                    break;
                case State.Naturethendrilgettotarget:
                    playernature.naturethendrilgettotarget();
                    playerutility.checkspellmaxtime();
                    break;
                case State.Naturethendrilfinalmovement:
                    playernature.naturethendrilfinalmovement();
                    break;
                case State.Icelancestart:
                    playerice.icelanceplayermovement();
                    break;
                case State.Icelancefly:
                    playerice.icelanceplayertotarget();
                    playerutility.checkspellmaxtime();
                    break;
                case State.Stormchainligthning:
                    playerstorm.stormchainlightning();
                    playerutility.checkspellmaxtime();
                    break;
                case State.Endlightning:
                    playerstorm.stormlightningbacktomain();
                    playerutility.checkspellmaxtime();
                    break;
                case State.Darkportalstart:
                    playerdark.darkportalstart();
                    break;
                case State.Darkportalend:
                    playerdark.darkportalending();
                    break;
                case State.Earthslide:
                    playerearth.earthslidemovement();
                    playerutility.checkspellmaxtime();
                    break;
                case State.Gatheritem:
                    break;
                case State.Empty:
                    break;
            }
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
    public void autolockon() => playerlockon.autolockon();
    public void lockonchangeafterdeath() => playerlockon.lockonfindclostesttarget();
    public void endlockon() => playerlockon.endlockon();
    public void switchtogroundstate()
    {
        if(playerhp.playerisdead == false)
        {
            Physics.IgnoreLayerCollision(11, 6, false);
            Physics.IgnoreLayerCollision(8, 6, false);
            ChangeAnimationState(idlestate);
            attackonceair = true;
            graviti = -0.5f;
            state = State.Ground;
        }
    }
    public void switchtoairstate()
    {
        Physics.IgnoreLayerCollision(11, 6);
        Physics.IgnoreLayerCollision(8, 6);
        state = State.Air;
    }
    public void switchtoemptystate()
    {
        ChangeAnimationState(idlestate);
        state = State.Empty;
    }
    public void switchtomovetowards(Vector3 newposi, float speed)
    {
        movetowardsposition = newposi;
        movetowardsspeed = speed;
        state = State.Movetowards;
    }

    public void slowplayer(float slowmovementspeed)
    {
        movementspeed = slowmovementspeed;
        //state = State.Ground;
    }
    public void switchtostun()
    {
        if (playerhp.playerisdead == false)
        {
            ChangeAnimationStateInstant(dazestate);
            state = State.Stun;
            Statics.otheraction = true;
            Statics.dash = true;
            Statics.resetvaluesondeathorstun = true;
        }
    }
    public void switchtobuttonmashstun(int buttonmashcount)
    {
        if (playerhp.playerisdead == false)
        {
            Statics.dash = true;
            Statics.resetvaluesondeathorstun = true;
            Statics.dazecounter = 0;
            Statics.dazekicksneeded = buttonmashcount;
            ChangeAnimationStateInstant(dazestate);
            state = State.Buttonmashstun;
            dazeimage.SetActive(true);
            dazeimage.GetComponentInChildren<Text>().text = "Mash " + buttonmashhotkey.GetBindingDisplayString();
        }
    }
    public void switchtodead()
    {
        state = State.Dead;
        velocity = new Vector3(0, velocity.y, 0);
    }
    public void resurrected() => playerheal.resurrected();
    private void playerstandup() => playerheal.playerstandup();
    public void switchtoattackaimstate()
    {
        setaimcam();
        state = State.Attackweaponaim;
    }
    public void switchtooutofcombataim()
    {
        setaimcam();
        state = State.Outofcombarbowcharge;
    }
    private void setaimcam()
    {
        CinemachinePOV Cam2pov = Cam2.GetCinemachineComponent<CinemachinePOV>();
        Cam2pov.m_VerticalAxis.Value = 0;
        Cam2pov.m_HorizontalRecentering.m_enabled = true;
        playeraim.activateaimcam();
        StartCoroutine(cam2recenteringdisable());
    }

    IEnumerator cam2recenteringdisable()                     //damit die cam2 bei aktivierung mit cam1 gleich gesetzt wird. Wenn Recentering aktiviert wird passt sich die 2. Cam sofort der rotation des spielers an.
    {                                                        //die rotation des spielers ist beim wechsel der Kamera die richtung des Kamerabrains(der Char dreht sich richtung Kamera)
        yield return new WaitForSeconds(0.2f);               //wenn Recentering nicht aktviert ist dreht die der Char nach dem wechsel sofort richtung 2. Cam die noch nicht die richtig position hat 
        CinemachinePOV Cam2pov = Cam2.GetCinemachineComponent<CinemachinePOV>();             //sprich die 2. Cam recentert sich schneller als die Char rotation
        Cam2pov.m_HorizontalRecentering.m_enabled = false;
        if(Cam2.gameObject.activeSelf == true)
        {
            Charrig.enabled = true;                     //wird erst hier gecalled weil die bowintoair animation probleme macht wenn der rig zu früh aktiviert wird
        }
    }
    public void disableaimcam()
    {
        Charprefabarrow.SetActive(false);
        Charrig.enabled = false;
        mousetarget.SetActive(false);
        Cam2.gameObject.SetActive(false);
    }
    public void checkforcamstate()
    {
        if (Cam2.gameObject.activeSelf == true)
        {
            disableaimcam();
        }
    }
    private void bowoutofcombatfullcharged() => playerbow.arrowfullcharge();
    private void bowoutofcombatnextarrow() => playerbow.nextarrow();

    public void activatedmgtext(GameObject enemyhit, float dmg)
    {
        var showtext = Instantiate(damagetext, enemyhit.transform.position, Quaternion.identity);
        showtext.GetComponent<TextMeshPro>().text = dmg.ToString();
        showtext.GetComponent<TextMeshPro>().color = Color.red;
    }
    public void bonusdashexplosion() => playerutility.bonusdashexplosiondmg();                      //wird mit der dashani getriggert
    public void bonuscalculatedashdmg() => playerutility.calculatedashdmg();

    public void elefiredashstart() => playerfire.firedashstart();
    public void elefiredashdmg() => playerfire.firedashdmg();
    public void elefiredashend() => playerfire.firedashend();
    public void elewaterpushbackdmg() => playerwater.waterpushbackdmg();
    public void elestarticelancefly() => playerice.starticelancefly();
    public void eleicelanceend() => playerice.icelanceend();
    public void elelightend()
    {
        if (state != Movescript.State.Empty) return;
        Abilitiesend();
    }
    public void elestartstormchainlightning() => playerstorm.startstormchainlightning();
    public void eleusedarkportal() => playerdark.usedarkportal();
    public void eledarkportalend() => playerdark.darkportalend();
    public void eleearthslidestart() => playerearth.earthslidestart();
    public void eleearthslidedmg() => playerearth.earthslidedmg();
    public void eleearthslideend() => playerearth.earthslideend();


    public void Abilitiesend()
    {
        if(lockontarget != null)
        {
            Vector3 lookPos = lockontarget.transform.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
        }
        else
        {
            Vector3 lookPos = transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
        }
        Statics.otheraction = false;
        Physics.IgnoreLayerCollision(15, 6, false);
        switchtoairstate();
    }
    public void pushplayerup(float amount) => playerair.pushplayerupwards(amount);
    public void gahteritem(GameObject gatherobject) => playerutility.gatheritem(gatherobject);
    public void spawnallies() => playerutility.spawnallies();

    private void playfootstep1() => playersounds.playfootstep1();
    private void playfootstep2() => playersounds.playfootstep2();
    private void playdashsound() => playersounds.playdash();

    private void switchmovespeed()
    {
        if (controlls.Menusteuerung.Switchmovespeed.WasPerformedThisFrame())
        {
            if (movementspeed > 15) movementspeed = 10;
            else movementspeed = 20;
        }
    }
        
}


