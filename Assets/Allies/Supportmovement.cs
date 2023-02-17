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
    private int basicpotionheal = 12;

    public string currentstate;
    const string idlestate = "Idle";

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
        waitforrangeattack,
        attackstate,
        changeposiafterattack,
        castheal,
        reset,
    }

    private void Awake()
    {
        hitbox = LayerMask.GetMask("Meleehitbox");
        Meshagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        supportchoosetarget.ssm = this;
        supportheal.ssm = this;
        supportmeleeattack.ssm = this;
        supportrangeattack.ssm = this;
        supportutilityfunctions.ssm = this;
    }
    private void OnEnable()
    {
        EnemyHP.supporttargetdied += enemyhasdied;
        Infightcontroller.setsupporttarget += switchtarget;
        currentstate = null;
        currenttarget = null;
        attacktimer = attackcd;
        switchtarget();
        float healamount = Mathf.Round(basicpotionheal + (Statics.groupstonehealbonus + GetComponent<Attributecontroller>().stoneclassbonusheal) * 0.01f * basicpotionheal * Statics.charcurrentlvl);
        healpotion.GetComponent<Alliesbottlecontroller>().potionheal = healamount;
    }
    private void OnDisable()
    {
        currenttarget = null;
        EnemyHP.supporttargetdied -= enemyhasdied;
        Infightcontroller.setsupporttarget -= switchtarget;
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
                supportheal.supporthealing();
                break;
            case State.waitformeleeattack:
                supportmeleeattack.waitingformeleeattack();
                supportheal.supporthealing();
                break;
            case State.waitforrangeattackcd:
                supportrangeattack.waitforrangeattackcd();
                supportheal.supporthealing();
                break;
            case State.waitforrangeattack:
                supportrangeattack.waitingforrangeattack();
                supportheal.supporthealing();
                break;
            case State.attackstate:
                supportutilityfunctions.attackstate();
                break;
            case State.changeposiafterattack:
                supportutilityfunctions.repositionafterattack();
                break;
            case State.castheal:
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
        state = State.empty;
        supportchoosetarget.settarget();
    }
    public void switchtoweaponstate()
    {
        ChangeAnimationState(idlestate);
        if (rangeweaponequiped == false) state = State.waitforattackcd;
        else state = State.waitforrangeattackcd;
    }
    public void supportreset() => supportutilityfunctions.supportreset();
    public void dying() => supportutilityfunctions.dying();
    public void supportresurrected() => supportutilityfunctions.supportresurrected();
    public void matecastheal() => supportheal.matecastheal();

    private void meleeattack1end() => supportmeleeattack.meleeattack1end();                 //wird bei der animation gecalled
    private void meleeattack2end() => supportmeleeattack.meleeattack2end();                 //wird bei der animation gecalled
    private void meleeattack3end() => supportmeleeattack.meleeattack3end();                 //wird bei der animation gecalled

    private void rangeattack1end() => supportrangeattack.rangeattack1end();                 //wird bei der animation gecalled
    private void rangeattack2end() => supportrangeattack.rangeattack2end();                 //wird bei der animation gecalled
    private void rangeattack3end() => supportrangeattack.rangeattack3end();                 //wird bei der animation gecalled

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
