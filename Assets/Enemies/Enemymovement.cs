using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemymovement : MonoBehaviour
{
    public NavMeshAgent Meshagent;
    public LayerMask Player;
    private Animator animator;
    [SerializeField] Enemyvalues enemyvalues;
    [NonSerialized] public EnemyHP enemyhpscript;

    public GameObject currenttarget;

    [NonSerialized] public float enemyresetrange = 20;
    [NonSerialized] public float checkforplayertimer;
    [NonSerialized] public float aggrorangecheck = 20;
    [NonSerialized] public float attackrange = 3f;
    [NonSerialized] public float followplayerafterattack;
    [NonSerialized] public float chancetochangeposi = 33;
    [NonSerialized] public float checkforresettimer;
    [NonSerialized] public float healticksafterreset = 0.75f;
    [NonSerialized] public float healticktimer;
    [NonSerialized] public float healtickamount;

    [SerializeField] private int enemylvl;
    [SerializeField] private float basedmg;
    [NonSerialized] public float normalattackcd;
    [NonSerialized] public float normalattacktimer;
    public bool spezialattack;

    public bool gethit;

    [NonSerialized] public float patroltimer;
    [NonSerialized] public float patrolwaittimer = 5f;
    [NonSerialized] public float patrolspeed = 2f;

    [NonSerialized] public float normalnavspeed;

    [NonSerialized] public Vector3 spawnpostion;
    [NonSerialized] public Vector3 patrolposi;
    [NonSerialized] public Vector3 posiafterattack;

    private Enemypatrol enemypatrol = new Enemypatrol();
    private Enemyattack enemyattack = new Enemyattack();
    private Enemyreset enemyreset = new Enemyreset();

    public string currentstate;

    public State state;

    public enum State
    {
        empty,
        waitfornextpatrolpoint,
        patrol,
        gettomeleerange,
        waitforattacks,
        isattacking,
        spezialattack,
        changeposi,
        resetheal,
        idleheal,
    }

    private void Awake()
    {
        enemyhpscript = GetComponent<EnemyHP>();
        enemylvl = GetComponent<EnemyHP>()._enemylvl;   //weil ich noch zusätzliche lvl im enemyhp adden kann
        Meshagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        enemypatrol.esm = this;
        enemyattack.esm = this;
        enemyreset.esm = this;

        spawnpostion = transform.position;

        basedmg = enemyvalues.attackdmg;
        normalnavspeed = enemyvalues.movementspeed;
        normalattackcd = enemyvalues.attackspeed;

        healtickamount = enemyhpscript.maxhealth / 100;
    }
    private void OnEnable()
    {
        normalattacktimer = normalattackcd;
        currentstate = null;
        state = State.empty;
        currenttarget = LoadCharmanager.Overallmainchar;
        checkforresettimer = 0;
        checkforplayertimer = 0;
        followplayerafterattack = 0;
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.empty:
                break;
            case State.waitfornextpatrolpoint:
                enemypatrol.waitfornextpatrolpoint();
                break;
            case State.patrol:
                enemypatrol.patrol();
                break;
            case State.gettomeleerange:
                enemyattack.gettomeleerange();
                break;
            case State.waitforattacks:
                enemyattack.waitingforattacks();
                break;
            case State.changeposi:
                enemyattack.repositionafterattack();
                break;
            case State.isattacking:
                FaceTraget();
                break;
            case State.spezialattack:
                resetpath();
                Facemainchar();
                break;
            case State.resetheal:
                enemyreset.resetheal();
                break;
            case State.idleheal:
                enemyreset.healwhileidle();
                break;
        }
    }
    public void triggerenemy() => enemypatrol.triggerenemy();       //ontriggerenter
    public void patrolend() => enemypatrol.patrolend();        //ontriggerexit
    public void checkforplayerinrange() => enemypatrol.checkforplayerinrange();
    public void checkforspezialattack() => enemyattack.checkforspezialattack();
    public void checkforreset() => enemyreset.checkforreset();
    private void backtowaitforattack() => enemyattack.backtowaitforattack();         //wird mit der animation gecalled
    private void resetpath() => Meshagent.ResetPath();
    private void callemptystate() => state = State.empty;                          //wird mit der animation gecalled
    private void normalattackdmg() => currenttarget.GetComponent<Playerhp>().TakeDamage(basedmg);                    //wird mit der animation gecalled
    public void FaceTraget()
    {
        Vector3 direction = (currenttarget.transform.position - transform.position).normalized;                    // normalized wegen magnitude
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));               //LookRotation reicht um die rotation zu bestimmmen + extra schritt das sich das objekt nur in x und z dreht
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);              //Slerp wird benutzt damit das Objekt sich in einer bestimmen geschwindikeit dreht (sonst würde sich das objekt instant drehen)
    }
    private void Facemainchar()
    {
        Vector3 direction = (LoadCharmanager.Overallmainchar.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void ChangeAnimationState(string newstate)
    {
        if (currentstate == newstate) return;
        animator.CrossFadeInFixedTime(newstate, 0.1f);
        currentstate = newstate;
    }
}
