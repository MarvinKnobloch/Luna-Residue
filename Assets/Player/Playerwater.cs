using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerwater
{
    public Movescript psm;

    const string waterkickstate = "Waterkick";
    public void waterpushback()
    {
        if (Movescript.lockontarget != null)
        {
            /*float h = psm.move.x;

            psm.moveDirection = new Vector3(h, 0, 0);
            float magnitude = Mathf.Clamp01(psm.moveDirection.magnitude) * 10;
            psm.moveDirection.Normalize();
            psm.moveDirection = Quaternion.AngleAxis(psm.CamTransform.rotation.eulerAngles.y, Vector3.up) * psm.moveDirection;

            psm.velocity = psm.moveDirection * magnitude;
            psm.velocity.y = 0;
            psm.charactercontroller.Move(psm.velocity * Time.deltaTime);*/

            Vector3 newtransformposi = psm.transform.position;
            newtransformposi.y = psm.transform.position.y;
            Vector3 newlockonposi = Movescript.lockontarget.position;
            newlockonposi.y = psm.transform.position.y;
            Vector3 endposi = newlockonposi + (psm.transform.forward * -15);
            Vector3 distancetomove = endposi - newtransformposi;
            Vector3 move = distancetomove.normalized * 17 * Time.deltaTime;
            psm.charactercontroller.Move(move);
            if (Vector3.Distance(psm.transform.position, Movescript.lockontarget.position) > 13f)
            {
                psm.Abilitiesend();
            }
        }
        else
        {
            psm.Abilitiesend();
        }

    }
    public void waterpushbackdmg()
    {
        if (Movescript.lockontarget != null)
        {
            dealwaterdmg(Movescript.lockontarget.position, 2, 7);
        }
    }
    public void waterintoair()
    {
        if (Movescript.lockontarget != null)
        {
            psm.transform.position = Vector3.MoveTowards(psm.transform.position, psm.transform.position + Vector3.up, 15 * Time.deltaTime);
        }
        else
        {
            psm.Abilitiesend();
        }
    }
    public void waterintoairdmg()
    {
        dealwaterdmg(psm.transform.position, 4, 7);
    }
    public void startwaterkick()
    {
        psm.ChangeAnimationState(waterkickstate);
        psm.state = Movescript.State.Waterkickend;
    }
    public void waterkickend()
    {
        if (Movescript.lockontarget != null)
        {
            Vector3 distancetomove = Movescript.lockontarget.position - psm.transform.position;
            Vector3 move = distancetomove.normalized * 25 * Time.deltaTime;
            psm.charactercontroller.Move(move);
            psm.transform.rotation = Quaternion.LookRotation(Movescript.lockontarget.transform.position - psm.transform.position, Vector3.up);
            if (Vector3.Distance(psm.transform.position, Movescript.lockontarget.position) < 3f)
            {
                dealwaterdmg(psm.transform.position, 4, 10);
                Vector3 lookPos = Movescript.lockontarget.transform.position - psm.transform.position;
                lookPos.y = 0;
                psm.transform.rotation = Quaternion.LookRotation(lookPos);
                psm.Abilitiesend();
            }
        }
        else psm.Abilitiesend();
    }
    public void dealwaterdmg(Vector3 hitposi, float size, float dmg)
    {
        Collider[] cols = Physics.OverlapSphere(hitposi, size, psm.spellsdmglayer);
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }
        }
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;
                    enemyscript.TakeDamage(dmg, 0 ,false);
                    //psm.activatedmgtext(Enemyhit.gameObject, dmg);
                }

            }
        }
    }

}
