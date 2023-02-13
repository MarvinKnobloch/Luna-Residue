using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemymovement : MonoBehaviour
{
    public static event Action infightlistupdate;

    public NavMeshAgent Meshagent;
    public LayerMask Player;
    private Animator animator;
    [SerializeField] Enemyvalues enemyvalues;
    private EnemyHP enemyhpscript;

    public GameObject currenttarget;

    public float enemyresetrange;
    private float aggrorangecheck = 20;
    private float attackrange = 3;
    private float chancetochangeposi = 33;
    private float healticksafterreset = 0.75f;
    private float healticktimer;
    private float healinstant;
    private float healtickamount;

    [SerializeField] private int enemylvl;
    [SerializeField] private float basedmg;
    [SerializeField] private float normalattackcd;
    private float normalattacktimer;
    public bool spezialattack;

    public bool gethit;

    private float gethittimer;
    private float patroltimer;
    private float patrolwaittimer = 5f;
    private float patrolspeed = 2f;

    private float normalnavspeed;

    private Vector3 spawnpostion;
    private Vector3 patrolposi;
    private Vector3 posiafterattack;

    private bool rootmotion;

    private bool hardattackcheck;

    public string currentstate;
    const string idlestate = "Idle";
    const string patrolstate = "Patrol";
    const string runstate = "Run";
    const string attack1state = "Attack1";
    const string spezialattackstate = "Spezial";
    const string stunnedstate = "Interrupted";
    const string standupstate = "Standup";

    public State state;

    public enum State
    {
        empty,
        idlestate,
        patrol,
        gettomeleerange,
        waitforattacks,
        isattacking,
        spezialattack,
        isstunned,
        changeposi,
        resetheal,
        idleheal,
        resetstopheal,
    }

    void Start()
    {
        enemyhpscript = GetComponent<EnemyHP>();
        enemylvl = GetComponent<EnemyHP>()._enemylvl;   //weil ich noch zusätzliche lvl im enemyhp adden kann
        normalattacktimer = normalattackcd;
        currentstate = null;
        state = State.empty;
        currenttarget = LoadCharmanager.Overallmainchar;
        spawnpostion = transform.position;
        rootmotion = false;
        Meshagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        basedmg = enemyvalues.attackdmg;
        normalnavspeed = enemyvalues.movementspeed;
        normalattackcd = enemyvalues.attackspeed;

        healinstant = enemyhpscript.maxhealth / 20;
        healtickamount = enemyhpscript.maxhealth / 50;

    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.empty:
                break;
            case State.idlestate:
                npcidle();
                break;
            case State.patrol:
                patroltonextpoint();
                break;
            case State.gettomeleerange:
                gettoplayer();
                break;
            case State.waitforattacks:
                waitingforattacks();
                break;
            case State.isattacking:
                FaceTraget();
                break;
            case State.spezialattack:
                resetpath();
                Facemainchar();
                break;
            case State.resetheal:
                enemyhealreset();
                break;
            case State.idleheal:
                healwhileidle();
                break;
            case State.resetstopheal:
                enemyhasreset();
                break;
            case State.isstunned:
                break;
            case State.changeposi:
                repositionafterattack();
                break;
        }
    }
    public void patrolstart()
    {
        Meshagent.speed = patrolspeed;
        patroltimer = 0f;
        patrolposi = spawnpostion + UnityEngine.Random.insideUnitSphere * 10;
        patrolposi.y = transform.position.y;
        NavMeshHit hit;
        bool blocked;
        blocked = NavMesh.Raycast(transform.position, patrolposi, out hit, NavMesh.AllAreas);
        if (blocked == true)
        {
            patrolposi = hit.position;
            Meshagent.SetDestination(patrolposi);
            ChangeAnimationState(patrolstate);
            state = State.patrol;
        }
        else
        {
            Meshagent.SetDestination(patrolposi);
            ChangeAnimationState(patrolstate);
            state = State.patrol;
        }
    }
    public void patrolend()
    {
        ChangeAnimationState(idlestate);
        state = State.empty;
    }
    private void npcidle()
    {
        //ChangeAnimationState(idlestate);
        if (Vector3.Distance(transform.position, LoadCharmanager.Overallmainchar.transform.position) < aggrorangecheck)
        {
            //GetComponentInChildren<EnemyHP>().addtocanvas();
            Meshagent.speed = normalnavspeed;
            state = State.gettomeleerange;
        }
        else
        {
            Meshagent.speed = patrolspeed;
            patroltimer += Time.deltaTime;
            if (patroltimer > patrolwaittimer)
            {
                patroltimer = 0f;
                patrolposi = spawnpostion + UnityEngine.Random.insideUnitSphere * 10;
                patrolposi.y = transform.position.y;
                NavMeshHit hit;
                bool blocked;
                blocked = NavMesh.Raycast(transform.position, patrolposi, out hit, NavMesh.AllAreas);
                if (blocked == true)
                {
                    patrolposi = hit.position;
                    Meshagent.SetDestination(patrolposi);
                    ChangeAnimationState(patrolstate);
                    state = State.patrol;
                }
                else
                {
                    Meshagent.SetDestination(patrolposi);
                    ChangeAnimationState(patrolstate);
                    state = State.patrol;
                }
            }
        }
    }
    private void patroltonextpoint()
    {
        if (Vector3.Distance(transform.position, LoadCharmanager.Overallmainchar.transform.position) < aggrorangecheck)
        {
            //GetComponentInChildren<EnemyHP>().addtocanvas();
            Meshagent.speed = normalnavspeed;
            state = State.gettomeleerange;
        }
        if (Vector3.Distance(transform.position, patrolposi) <= 2)
        {
            Meshagent.ResetPath();
            ChangeAnimationState(idlestate);
            patroltimer += Time.deltaTime;
            if (patroltimer > patrolwaittimer)
            {
                patroltimer = 0f;
                patrolposi = spawnpostion + UnityEngine.Random.insideUnitSphere * 10;
                patrolposi.y = transform.position.y;
                NavMeshHit hit;
                bool blocked;
                blocked = NavMesh.Raycast(transform.position, patrolposi, out hit, NavMesh.AllAreas);
                if (blocked == true)
                {
                    patrolposi = hit.position;
                    Meshagent.SetDestination(patrolposi);
                    ChangeAnimationState(patrolstate);
                    state = State.patrol;
                }
                else
                {
                    Meshagent.SetDestination(patrolposi);
                    ChangeAnimationState(patrolstate);
                    state = State.patrol;
                }
            }
        }
        else
        {
            patrolposi.y = transform.position.y;
            Meshagent.SetDestination(patrolposi);
        }
    }
    private void gettoplayer()
    {
        if (Vector3.Distance(transform.position, new Vector3(currenttarget.transform.position.x, transform.position.y, currenttarget.transform.position.z)) > attackrange)
        {
            ChangeAnimationState(runstate);
            Meshagent.SetDestination(currenttarget.transform.position);
        }
        else
        {
            Meshagent.ResetPath();
            ChangeAnimationState(idlestate);
            state = State.waitforattacks;
        }
        if (spezialattack == true)
        {
            ifspezial();
        }
        if (Vector3.Distance(spawnpostion, transform.position) > enemyresetrange)
        {
            ifreset();
        }
    }
    private void waitingforattacks()
    {
        FaceTraget();
        normalattacktimer += Time.deltaTime;

        if (normalattacktimer > normalattackcd)
        {
            normalattacktimer = 0;
            ChangeAnimationState(attack1state);
            state = State.isattacking;
        }
        if (spezialattack == true)
        {
            ifspezial();
        }
        if (Vector3.Distance(transform.position, new Vector3(currenttarget.transform.position.x, transform.position.y, currenttarget.transform.position.z)) > attackrange)
        {
            ChangeAnimationState(runstate);
            state = State.gettomeleerange;
        }
        if (Vector3.Distance(spawnpostion, transform.position) > enemyresetrange)
        {
            ifreset();
        }
    }
    private void backtowaitforattack()
    {
        int newposi = UnityEngine.Random.Range(0, 100);   
        if (newposi < chancetochangeposi)
        {
            posiafterattack = currenttarget.transform.position + UnityEngine.Random.insideUnitSphere * 5;
            posiafterattack.y = transform.position.y;
            NavMeshHit hit;
            bool blocked;
            blocked = NavMesh.Raycast(transform.position, posiafterattack, out hit, NavMesh.AllAreas);
            if (blocked == true)
            {
                posiafterattack = hit.position;
                Meshagent.SetDestination(posiafterattack);
                ChangeAnimationState(runstate);
                state = State.changeposi;
            }
            else
            {
                Meshagent.SetDestination(posiafterattack);
                ChangeAnimationState(runstate);
                state = State.changeposi;
            }
        }
        else
        {
            ChangeAnimationState(idlestate);
            state = State.waitforattacks;
        }
    }
    private void repositionafterattack()
    {
        posiafterattack.y = transform.position.y;
        normalattacktimer += Time.deltaTime;
        if (Vector3.Distance(transform.position, posiafterattack) <= 2)
        {
            if(normalattacktimer > normalattackcd - (float)0.2)
            {
                normalattacktimer = normalattackcd - (float)0.2;              // damit sich der gegner noch dreht bevor er angreift
            }
            Meshagent.ResetPath();
            ChangeAnimationState(idlestate);
            state = State.gettomeleerange;
        }
        if (spezialattack == true)
        {
            ifspezial();
        }
        if (Vector3.Distance(spawnpostion, transform.position) > enemyresetrange)
        {
            ifreset();
        }
    }
    private void enemyhealreset()
    {
        healticktimer += Time.deltaTime;
        if(healticktimer > healticksafterreset)
        {
            enemyhpscript.enemyheal(healtickamount);
            healticktimer = 0f;
            if (enemyhpscript.currenthealth >= enemyhpscript.maxhealth)
            {
                ChangeAnimationState(runstate);
                state = State.resetstopheal;
            }
        }
        Meshagent.SetDestination(spawnpostion);
        if (Vector3.Distance(spawnpostion, transform.position) < 2)
        {
            currenttarget = LoadCharmanager.Overallmainchar;
            Meshagent.ResetPath();
            ChangeAnimationState(idlestate);
            state = State.idleheal;
        }
    }
    private void healwhileidle()
    {
        healticktimer += Time.deltaTime;
        if (healticktimer > healticksafterreset)
        {
            enemyhpscript.enemyheal(healtickamount);
            healticktimer = 0f;
            if (enemyhpscript.currenthealth >= enemyhpscript.maxhealth)
            {
                ChangeAnimationState(idlestate);
                state = State.idlestate;
            }
        }
        if (Vector3.Distance(transform.position, LoadCharmanager.Overallmainchar.transform.position) < aggrorangecheck)
        {
            ChangeAnimationState(runstate);
            Meshagent.speed = normalnavspeed;
            state = State.gettomeleerange;
        }
    }
    private void enemyhasreset()
    {
        Meshagent.SetDestination(spawnpostion);
        if (Vector3.Distance(spawnpostion, transform.position) < 2)
        {
            currenttarget = LoadCharmanager.Overallmainchar;
            Meshagent.ResetPath();
            ChangeAnimationState(idlestate);
            state = State.idlestate;
        }
    }
    private void ifspezial()
    {
        spezialattack = false;
        ChangeAnimationState(spezialattackstate);
        state = State.spezialattack;
    }
    private void ifreset()
    {
        healticktimer = 0f;
        gameObject.GetComponent<EnemyHP>().enemyhasreset();
        spezialattack = false;
        enemyhpscript.takeplayerdamage(-healinstant, 0, false);
        ChangeAnimationState(runstate);
        state = State.resetheal;
        if (Infightcontroller.infightenemylists.Contains(transform.root.gameObject))
        {
            Infightcontroller.infightenemylists.Remove(transform.root.gameObject);
            infightlistupdate?.Invoke();
        }
    }
    private void resetpath()
    {
        Meshagent.ResetPath();
    }
    private void callemptystate()
    {
        state = State.empty;
    }
    private void normalattackdmg()
    {
        currenttarget.GetComponent<Playerhp>().TakeDamage(basedmg);
    }
    private void resethardattackcheck()
    {
        hardattackcheck = false;
    }
    public void hardattackinterrupt()
    {
        if (hardattackcheck == true)
        {
            hardattackcheck = false;
            ChangeAnimationState(stunnedstate);
            state = State.isstunned;
        }
    }
    private void standupafterstun()
    {
        ChangeAnimationState(standupstate);
    }
    private void FaceTraget()
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
    private void OnAnimatorMove()
    {
        if (rootmotion == true)
        {

        }
    }
}

/*private void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, attackrange);
}*/

/*if (hardattacktimer > hardattackcd + UnityEngine.Random.Range(0, 3))
{
    hardattackcheck = true;
    hardattacktimer = 0;
    ChangeAnimationState(hardattackstate);
    state = State.hardattack;
}*/