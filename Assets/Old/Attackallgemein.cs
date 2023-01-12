using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackallgemein : MonoBehaviour
{
    /*[SerializeField]
    internal Movementallgemein Movementscript;

    public bool moveattackcheck;

    private SpielerSteu Steuerung;
    private Animator animator;
    private bool root;
    private bool basic;
    private bool basic2;
    private bool attackup;
    private bool basicairattack;
    private bool stayairattack;
    private float basicattacktime;
    public bool basicattackonce;
    private bool basiccheck;
    private bool basic2check;
    private float animationtime;
    private bool attackupcheck;
    private bool attackendcheck;
    private bool air1check;
    private bool air2check;
    private bool airend;
    private float gravitionattack = 0f;
    private float dashcd;
    public float resetdash;
    [SerializeField]
    private float gravitionnormal;

    void Awake()
    {
        Steuerung = new SpielerSteu();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Steuerung.Enable();
        moveattackcheck = false;
        basic = false;
        root = false;
        attackupcheck = false;
        air1check = false;
        basicattackonce = true;
    }
    void Update()
    {
        dashcd += Time.deltaTime;
        resetdash += Time.deltaTime;

        if (Movementscript.amBoden == true)
        {
            animator.SetBool("fallen", false);
            animator.SetBool("springen", false);
            animator.SetBool("landen", true);
            air1check = false;
            animator.SetBool("air1", false);
            animator.SetBool("fallafterattack", false);
            animator.SetBool("air2", false);
            animator.SetBool("air3", false);

            if (Movementscript.laufanimation == true)
            {
                animator.SetBool("rennen", true);
            }
            else
            {
                animator.SetBool("rennen", false);
            }
            if (Movementscript.idleanimation == true)
            {
                animator.SetBool("idle", true);
            }
            else
            {
                animator.SetBool("idle", false);
            }
            if (basicattackonce == true && Steuerung.Spielerboden.Attack1.WasPressedThisFrame())
            {
                animator.SetBool("attack1", true);
                basiccheck = true;
            }
            if (basic == true && Steuerung.Spielerboden.Attack2.WasPressedThisFrame())
            {
                animator.SetBool("attack2", true);
                basic2check = true;
            }
            if (basic2 == true && Steuerung.Spielerboden.Attack2.WasPerformedThisFrame())
            {
                attackupcheck = true;
                animator.SetBool("attack3", true);
            }
            if (attackup == true && Steuerung.Spielerboden.Attack3.WasPerformedThisFrame())
            {
                attackendcheck = true;
                animator.SetBool("attack4", true);
            }
        }
        else
        {
            animator.SetBool("idle", false);
            animator.SetBool("rennen", false);
            animator.SetBool("landen", false);
            if (Movementscript.fallenanicheck == true)
            {
                animator.SetBool("fallen", true);
                animator.SetBool("fallafterattack", true);
            }
        }
        if (basiccheck == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Basic"))
        {
            basicattackonce = false;
            basiccheck = false;
            animator.SetBool("attack1", false);
            animationtime = animator.GetCurrentAnimatorStateInfo(0).length - 0.35f;
            moveattackcheck = true;
            Invoke("Attack1true", 0.3f);
        }
        if (basic2check == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Basic2"))
        {
            animationtime = animator.GetCurrentAnimatorStateInfo(0).length - 0.3f;
            basic2check = false;
            Invoke("Attack2true", 0.3f);
        }

        if (attackupcheck == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Groundup"))
        {
            animationtime = animator.GetCurrentAnimatorStateInfo(0).length - 0.4f;
            attackupcheck = false;
            Invoke("Attack3true", 0.35f);
        }

        if (attackendcheck == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Groundupend"))
        {
            root = true;
            attackendcheck = false;
            animationtime = animator.GetCurrentAnimatorStateInfo(0).length - 0.48f;
            Movementscript.gravitation = gravitionattack;
            Invoke("Attack4true", 0.45f);
        }

        if (Movementscript.springt == true)
        {
            animator.SetBool("springen", true);
            animator.SetBool("idle", false);
        }
        else
        {
            animator.SetBool("springen", false);
        }
        if (Steuerung.Spielerboden.Dodge.WasPerformedThisFrame() && dashcd > 0.5f)
        {
            root = true;
            dashcd = 0f;
            resetdash = 0f;
            CancelInvoke();
            animator.SetBool("dash", true);
            animator.SetBool("fallen", false);
            Movementscript.runter = 0f;
            Movementscript.gravitation = gravitionattack;
            Invoke("dashend", 0.1f);
        }
        if (Movementscript.attackabstandboden == true && Movementscript.attackonceperair == true && Movementscript.amBoden == false)
        {

            if (Movementscript.air1check == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Basic") == false && Steuerung.Spielerluft.Luft1.WasPressedThisFrame())
            {
                air1check = true;
                animator.SetBool("air1", true);
                Movementscript.gravitation = gravitionattack;
                Movementscript.runter = 0f;
                Invoke("air1fail", 0.6f);
            }
            if (air1check == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Air1"))
            {
                Movementscript.gravitation = gravitionattack;
                Movementscript.runter = 0f;
                air1check = false;
                air2check = false;
                airend = false;
                Movementscript.air1check = false;
                Movementscript.fallenanicheck = false;
                animator.SetBool("fallafterattack", false);
                animationtime = animator.GetCurrentAnimatorStateInfo(0).length - 0.39f;
                moveattackcheck = true;
                Invoke("Air1true", 0.35f);
            }
        }
        if (Movementscript.attackonceperair == true && Movementscript.amBoden == false)
        {
            if (basicairattack == true && Steuerung.Spielerluft.Luft2.WasPressedThisFrame())
            {
                air2check = true;
                animator.SetBool("air2", true);
            }
            if (air2check == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Air2"))
            {
                basicairattack = false;
                air2check = false;
                animationtime = animator.GetCurrentAnimatorStateInfo(0).length - 0.48f;
                moveattackcheck = true;
                Invoke("Air2true", 0.45f);
            }
            if (stayairattack == true && Steuerung.Spielerluft.Luft3.WasPressedThisFrame())
            {
                airend = true;
                animator.SetBool("air3", true);
            }
            if (airend == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Air3"))
            {
                stayairattack = false;
                airend = false;
                animationtime = animator.GetCurrentAnimatorStateInfo(0).length - 0.1f;
                moveattackcheck = true;
                Movementscript.attackonceperair = false;
                Invoke("Air3fertig", animationtime);
            }
        }
    }
    private void OnAnimatorMove()
    {
        if (root == true)
        {
            Vector3 velocity = animator.deltaPosition;
            Movementscript.controller.Move(velocity);
        }
    }
    private void dashend()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dash") == true || resetdash > 0.35f)
        {
            Invoke("seconddashend", 0.1f);
        }
        else
        {
            Invoke("dashend", 0.05f);
        }
    }
    private void seconddashend()
    {
        root = false;
        moveattackcheck = false;
        animator.SetBool("dash", false);
        Movementscript.gravitation = gravitionnormal;
    }
    private void Attack1true()
    {
        basic = true;
        animator.SetBool("attack1", false);
        basicattacktime = animationtime - 0.1f;
        Invoke("basicfertig", basicattacktime);
        Invoke("basicafter", animationtime);
    }
    private void basicfertig()
    {
        basic = false;
    }
    private void basicafter()
    {
        Invoke("faustbasiccd", 0.2f);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Basic2") == false)
        {
            moveattackcheck = false;
        }
    }
    private void basiccd()
    {
        basicattackonce = true;
    }
    private void Attack2true()
    {
        basic2 = true;
        animator.SetBool("attack2", false);
        basicattacktime = animationtime - 0.1f;
        Invoke("basic2fertig", basicattacktime);
        Invoke("basic2after", animationtime);
    }
    private void basic2fertig()
    {
        basic2 = false;
    }
    private void basic2after()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Groundup") == false)
        {
            moveattackcheck = false;
        }
    }
    private void Attack3true()
    {
        attackup = true;
        animator.SetBool("attack3", false);
        basicattacktime = animationtime - 0.1f;
        Invoke("groundupfertig", basicattacktime);
        Invoke("groundupafter", animationtime);
    }
    private void groundupfertig()
    {
        attackup = false;
    }
    private void groundupafter()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Groundupend") == false)
        {
            moveattackcheck = false;
        }
    }
    private void Attack4true()
    {
        Movementscript.runter = 0f;
        animator.SetBool("attack4", false);
        basicattacktime = animationtime - 0.12f;
        Invoke("groundupendfertig", basicattacktime);
        Invoke("groundupendafter", animationtime);
    }
    private void groundupendfertig()
    {
        Movementscript.air1check = false;
        root = false;
    }
    private void groundupendafter()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Air1") == false)
        {
            Movementscript.gravitation = gravitionnormal;
            moveattackcheck = false;
        }
    }
    private void air1fail()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Air1") == false)
        {
            animator.SetBool("air1", false);
            Movementscript.attackonceperair = false;
            Movementscript.gravitation = gravitionnormal;
        }
    }
    private void Air1true()
    {
        basicairattack = true;
        animator.SetBool("air1", false);
        basicattacktime = animationtime - 0.10f;
        Invoke("Air1fertig", basicattacktime);
        Invoke("afterair1", animationtime);
    }
    private void Air1fertig()
    {
        basicairattack = false;
    }
    private void afterair1()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Air2") == false)
        {
            Movementscript.attackonceperair = false;
            Movementscript.gravitation = gravitionnormal;
            moveattackcheck = false;
        }
    }
    private void Air2true()
    {
        stayairattack = true;
        animator.SetBool("air2", false);
        basicattacktime = animationtime - 0.1f;
        Invoke("Air2fertig", basicattacktime);
        Invoke("afterair2", animationtime);
    }
    private void Air2fertig()
    {
        stayairattack = false;
    }
    private void afterair2()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Air3") == false)
        {
            Movementscript.attackonceperair = false;
            Movementscript.gravitation = gravitionnormal;
            moveattackcheck = false;
        }
    }
    private void Air3fertig()
    {
        animator.SetBool("air3", false);
        Movementscript.gravitation = gravitionnormal;
        moveattackcheck = false;
    }*/
}