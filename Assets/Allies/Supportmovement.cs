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

    [NonSerialized] public float lookfortargetrange = 20f;
    [NonSerialized] public float attackrangecheck;
    [NonSerialized] public float addedattackrangetocollider = 2.5f;
    public float attacktimer;
    public float attackspeed;
    private float resetrange = 34;                  //muss kleiner als die enemybaractivate sein
    private float chancetochangeposi = 33;

    public bool rangeweaponequiped;
    public bool ishealer;
    [NonSerialized] public float healtimer;
    [NonSerialized] public float additverandonhealtimer;
    [SerializeField] public GameObject healpotion;
    private int basicpotionheal = 12;

    private Vector3 posiafterattack;

    public string currentstate;
    const string idlestate = "Idle";
    const string runstate = "Run";
    const string attack1state = "Attack1";
    const string attack2state = "Attack2";
    const string attack3state = "Attack3";

    private Supportchoosetarget supportchoosetarget = new Supportchoosetarget();
    private Supportheal supportheal = new Supportheal();

    public State state;
    public enum State
    {
        gettomeleerange,
        rangeweaporange,
        waitformeleeattack,
        waitforrangeattack,
        attacktarget,
        changeposiafterattack,
        castheal,
        reset,
    }

    private void Awake()
    {
        hitbox = LayerMask.GetMask("Meleehitbox");
        Meshagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attacktimer = attackspeed;
        supportchoosetarget.ssm = this;
    }
    private void OnEnable()
    {
        currentstate = null;
        EnemyHP.supporttargetdied += enemyhasdied;
        Infightcontroller.setsupporttarget += settarget;
        currenttarget = null;
        settarget();
        if (rangeweaponequiped == false)
        {
            state = State.gettomeleerange;
        }
        else
        {
            state = State.rangeweaporange;
        }
        float healamount = Mathf.Round(basicpotionheal + (Statics.groupstonehealbonus + GetComponent<Attributecontroller>().stoneclassbonusheal) * 0.01f * basicpotionheal * Statics.charcurrentlvl);
        healpotion.GetComponent<Alliesbottlecontroller>().potionheal = healamount;
    }
    private void OnDisable()
    {
        currenttarget = null;
        EnemyHP.supporttargetdied -= enemyhasdied;
        Infightcontroller.setsupporttarget -= settarget;
    }
    void Update()
    {
        switch (state)
        {
            default:
            case State.gettomeleerange:
                meleeweaponmovement();
                supportheal.supporthealing();
                break;
            case State.waitformeleeattack:
                waitingformeleeattack();
                supportheal.supporthealing();
                break;
            case State.rangeweaporange:
                rangeweaponmovement();
                supportheal.supporthealing();
                break;
            case State.waitforrangeattack:
                waitingforrangeattack();
                supportheal.supporthealing();
                break;
            case State.attacktarget:
                Attackenemy();
                break;
            case State.changeposiafterattack:
                repositionafterattack();
                break;
            case State.castheal:
                break;
            case State.reset:
                resetcombat();
                break;
        }
    }
    public void enemyhasdied()
    {
        currenttarget = null;
        settarget();
    }
    private void settarget() => supportchoosetarget.settarget();
    public void playerfocustarget() => supportchoosetarget.playerfocustarget();
    private void supportreset()
    {
        if (Vector3.Distance(currenttarget.transform.position, LoadCharmanager.Overallmainchar.transform.position) > resetrange)
        {
            state = State.reset;
            ChangeAnimationState(runstate);
        }
    }


    private void meleeweaponmovement()
    {
        if (currenttarget != null)
        {
            supportreset();

            attacktimer += Time.deltaTime;
            Meshagent.SetDestination(currenttarget.transform.position);
            if (Meshagent.velocity.magnitude < 0.3f)
            {
                ChangeAnimationState(idlestate);
            }
            else
            {
                ChangeAnimationState(runstate);
            }
            if (Vector3.Distance(transform.position, currenttarget.transform.position) < attackrangecheck)
            {
                state = State.waitformeleeattack;
                ChangeAnimationState(idlestate);
                Meshagent.ResetPath();
            }
        }
        else
        {
            settarget();
            state = State.reset;
        }
    }

    private void rangeweaponmovement()
    {
        if (currenttarget != null)
        {
            supportreset();

            attacktimer += Time.deltaTime;
            Meshagent.SetDestination(currenttarget.transform.position);
            if (Meshagent.velocity.magnitude < 0.3f)
            {
                ChangeAnimationState(idlestate);
            }
            else
            {
                ChangeAnimationState(runstate);
            }
            if (Vector3.Distance(transform.position, currenttarget.transform.position) < attackrangecheck + 10)
            {
                state = State.waitforrangeattack;
                ChangeAnimationState(idlestate);
                Meshagent.ResetPath();
            }
        }
        else
        {
            settarget();
            state = State.reset;
        }
    }

    private void waitingformeleeattack()
    {
        if (currenttarget != null)
        {
            supportreset();
            attacktimer += Time.deltaTime;
            FaceTraget();
            if (attacktimer > attackspeed)
            {
                state = State.attacktarget;
            }
            if (Vector3.Distance(transform.position, currenttarget.transform.position) > attackrangecheck)
            {
                state = State.gettomeleerange;
            }
        }
        else
        {
            settarget();
            state = State.reset;
        }
    }

    private void waitingforrangeattack()
    {
        if (currenttarget != null)
        {
            supportreset();
            attacktimer += Time.deltaTime;
            if (attacktimer > attackspeed)
            {
                FaceTraget();
                Meshagent.ResetPath();
                state = State.attacktarget;
            }
            else
            {

                if (gameObject != currenttarget.GetComponentInParent<Enemymovement>().currenttarget)
                {
                    Vector3 rangenewposi = currenttarget.transform.position + currenttarget.transform.forward * -14;
                    rangenewposi.y = transform.position.y;
                    NavMeshHit hit;
                    bool blocked;
                    blocked = NavMesh.Raycast(transform.position, rangenewposi, out hit, NavMesh.AllAreas);
                    if (blocked == true)
                    {
                        //Debug.Log("blocked");
                        Meshagent.SetDestination(hit.position);
                        ChangeAnimationState(runstate);
                        if (Vector3.Distance(transform.position, hit.position) <= 2)
                        {
                            FaceTraget();
                            ChangeAnimationState(idlestate);
                            Meshagent.ResetPath();
                        }
                    }
                    else if (Vector3.Distance(transform.position, rangenewposi) < 5)
                    {
                        FaceTraget();
                        ChangeAnimationState(idlestate);
                        Meshagent.ResetPath();
                    }
                    else
                    {
                        Meshagent.SetDestination(rangenewposi);
                        ChangeAnimationState(runstate);
                    }
                }
                else
                {
                    FaceTraget();
                    ChangeAnimationState(idlestate);
                    Meshagent.ResetPath();
                }
            }
        }
        else
        {
            settarget();
            state = State.reset;
        }
    }
    private void Attackenemy()
    {
        if (currenttarget != null)
        {
            supportreset();
            FaceTraget();
            if (attacktimer > attackspeed)
            {
                ChangeAnimationState(attack1state);
                attacktimer = 0f;
            }
        }
        else
        {
            settarget();
            state = State.reset;
        }
    }

    private void repositionafterattack()
    {
        if (currenttarget != null)
        {
            supportreset();
            posiafterattack.y = transform.position.y;
            if (Vector3.Distance(transform.position, posiafterattack) <= 2)
            {
                Meshagent.ResetPath();
                ChangeAnimationState(idlestate);
                state = State.gettomeleerange;
            }
        }
        else
        {
            settarget();
            state = State.reset;
        }
    }
    private void resetcombat()
    {
        if (Vector3.Distance(transform.position, LoadCharmanager.Overallmainchar.transform.position) < 3)
        {
            ChangeAnimationState(idlestate);
            Meshagent.ResetPath();
            if (Statics.infight == true)
            {
                if(rangeweaponequiped == false)
                {
                    //state = State.gettomeleerange;
                }
                else
                {
                    //state = State.rangeweaporange;
                }
            }
        }
        else
        {
            Meshagent.SetDestination(LoadCharmanager.Overallmainchar.transform.position);
            ChangeAnimationState(runstate);
        }
    }

    private void meleeattack1end()
    {
        if (currenttarget != null)
        {
            supportreset();
            if (Vector3.Distance(transform.position, currenttarget.transform.position) > attackrangecheck)
            {
                state = State.gettomeleerange;
            }
            else
            {
                ChangeAnimationState(attack2state);
            }
        }
        else
        {
            settarget();
            state = State.reset;
        }
    }
    private void meleeattack2end()
    {
        if (currenttarget != null)
        {
            supportreset();
            if (Vector3.Distance(transform.position, currenttarget.transform.position) > attackrangecheck)
            {
                state = State.gettomeleerange;
            }
            else
            {
                ChangeAnimationState(attack3state);
            }
        }
        else
        {
            settarget();
            state = State.reset;
        }
    }
    private void meleeattack3end()
    {
        if (currenttarget != null)
        {
            supportreset();
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
                    state = State.changeposiafterattack;
                }
                else
                {
                    Meshagent.SetDestination(posiafterattack);
                    ChangeAnimationState(runstate);
                    state = State.changeposiafterattack;
                }
            }
            else
            {
                ChangeAnimationState(runstate);
                state = State.gettomeleerange;
            }
        }
        else
        {
            settarget();
            state = State.reset;
        }
    }
    private void rangeattack1end()
    {
        if (currenttarget != null)
        {
            supportreset();
            if (Vector3.Distance(transform.position, currenttarget.transform.position) > attackrangecheck + 15)
            {
                state = State.rangeweaporange;
            }
            else
            {
                ChangeAnimationState(attack2state);
            }
        }
        else
        {
            settarget();
            state = State.reset;
        }
    }
    private void rangeattack2end()
    {
        if (currenttarget != null)
        {
            supportreset();
            if (Vector3.Distance(transform.position, currenttarget.transform.position) > attackrangecheck + 15)
            {
                state = State.rangeweaporange;
            }
            else
            {
                ChangeAnimationState(attack3state);
            }
        }
        else
        {
            settarget();
            state = State.reset;
        }
    }
    private void rangeattack3end()
    {
        if (currenttarget != null)
        {
            supportreset();
            if (Vector3.Distance(transform.position, currenttarget.transform.position) > attackrangecheck + 15)
            {
                state = State.rangeweaporange;
            }
            else
            {
                state = State.waitforrangeattack;
                ChangeAnimationState(idlestate);
            }
        }
        else
        {
            settarget();
            state = State.reset;
        }
    }
    private void FaceTraget()
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
    public void matecastheal() => supportheal.matecastheal();
}
