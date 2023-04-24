using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Supportmovement : MonoBehaviour
{
    public NavMeshAgent Meshagent;
    private Animator animator;
    [NonSerialized] public LayerMask hitbox;
    [NonSerialized] public Playerhp playerhp;

    [SerializeField]
    public GameObject currenttarget;

    [NonSerialized] public float lookfortargetrange = 25f;
    [NonSerialized] public float attackrangecheck;
    [NonSerialized] public float addedattackrangetocollider = 2f;
    [NonSerialized] public float addedrangeattackrange = 15;
    public float attacktimer;
    [NonSerialized] public float attackcd = 2;

    [NonSerialized] public float followenemytimer;


    [NonSerialized] public float supportresetrange = 30;                  //muss kleiner als die enemybaractivate sein
    [NonSerialized] public float resettimer;
    [NonSerialized] public float resetcombattimer;

    [NonSerialized] public float chancetochangeposi = 33;
    [NonSerialized] public Vector3 posiafterattack;

    public bool rangeweaponequiped;
    public bool ishealer;
    [NonSerialized] public float healtimer;
    [NonSerialized] public float additverandomhealtimer;
    [SerializeField] public GameObject healpotion;
    private int basicpotionheal = 18;

    public GameObject resurrecttraget;

    public string currentstate;
    const string idlestate = "Idle";

    public Supportsounds supportsounds;

    private Supportchoosetarget supportchoosetarget = new Supportchoosetarget();
    private Supportheal supportheal = new Supportheal();
    private Supportmeleeattack supportmeleeattack = new Supportmeleeattack();
    private Supportrangeattack supportrangeattack = new Supportrangeattack();
    private Supportutilityfunctions supportutilityfunctions = new Supportutilityfunctions();

    public State state;
    public enum State
    {
        empty,
        waitforattackcd,
        waitforrangeattackcd,
        waitformeleeattack,
        attackstate,
        changeposiafterattack,
        changeposiafterrangeattack,
        castheal,
        resurrect,
        reset,
    }

    private void Awake()
    {
        hitbox = LayerMask.GetMask("Meleehitbox");
        Meshagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerhp = GetComponent<Playerhp>();

        supportchoosetarget.ssm = this;
        supportheal.ssm = this;
        supportmeleeattack.ssm = this;
        supportrangeattack.ssm = this;
        supportutilityfunctions.ssm = this;
    }
    private void OnEnable()
    {
        EnemyHP.supporttargetdied += enemyhasdied;
        currentstate = null;
        currenttarget = null;
        attacktimer = attackcd;
        switchtarget();
        float potionheal = Globalplayercalculations.calculatecasthealing(basicpotionheal, playerhp.maxhealth / 2, GetComponent<Attributecontroller>().stoneclassbonusheal);
        healpotion.GetComponent<Alliesbottlecontroller>().potionheal = potionheal;
    }
    private void OnDisable()
    {
        currenttarget = null;
        EnemyHP.supporttargetdied -= enemyhasdied;
    }
    void Update()
    {
        switch (state)
        {
            default:
            case State.empty:
                break;
            case State.waitforattackcd:
                supportmeleeattack.waitforattackcd();
                supportheal.checkforresurrect();
                supportheal.supporthealing();
                break;
            case State.waitformeleeattack:
                supportmeleeattack.waitingformeleeattack();
                supportheal.checkforresurrect();
                supportheal.supporthealing();
                break;
            case State.waitforrangeattackcd:
                supportrangeattack.waitforrangeattackcd();
                supportheal.checkforresurrect();
                supportheal.supporthealing();
                break;
            case State.attackstate:
                supportutilityfunctions.attackstate();
                break;
            case State.changeposiafterattack:
                supportmeleeattack.repositionafterattack();
                break;
            case State.changeposiafterrangeattack:
                supportrangeattack.posiafterrangeattack();
                supportheal.checkforresurrect();
                supportheal.supporthealing();
                break;
            case State.castheal:
                break;
            case State.resurrect:
                supportheal.resurrectplayer();
                break;
            case State.reset:
                supportutilityfunctions.resetcombat();
                break;
        }
    }
    public void enemyhasdied()
    {
        currenttarget = null;
        switchtarget();
    }
    public void playerfocustarget() => supportchoosetarget.playerfocustarget();

    public void switchtarget()
    {
        if(playerhp.playerisdead == false)
        {
            state = State.empty;
            supportchoosetarget.settarget();
        }
    }
    public void switchtoweaponstate()
    {
        ChangeAnimationState(idlestate);
        if (rangeweaponequiped == false) state = State.waitforattackcd;
        else state = State.waitforrangeattackcd;
    }
    public void supportreset() => supportutilityfunctions.supportreset();
    public void supportresurrected() => supportheal.supportresurrected();
    private void resurrect() => supportheal.resurrect();
    public void matecastheal() => supportheal.matecastheal();

    private void meleeattack1end() => supportmeleeattack.meleeattack1end();                 //wird bei der animation gecalled
    private void meleeattack2end() => supportmeleeattack.meleeattack2end();                 //wird bei der animation gecalled
    private void meleeattack3end() => supportmeleeattack.meleeattack3end();

    private void rangeattack1end() => supportrangeattack.rangeattack1end();                 //wird bei der animation gecalled
    private void rangeattack2end() => supportrangeattack.rangeattack2end();                 //wird bei der animation gecalled
    private void rangeattack3end() => supportrangeattack.rangeattack3end();

    public void FaceTraget()
    {
        if (currenttarget != null)
        {
            Vector3 direction = (currenttarget.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
}
