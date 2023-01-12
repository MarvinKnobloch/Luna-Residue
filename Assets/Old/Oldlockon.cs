using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oldlockon : MonoBehaviour
{/*
    private void Movement()
    {
        if (moveDirection != Vector3.zero) //&& lockoncheck == false)               //Lockontraget.lockoncheck == false
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);                                              //Char dreht sich
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationgesch * Time.deltaTime);
        }
    }
    /*private void lockonnormalrotation()
    {
        if (lockontarget != null && lockoncheck == true)
        {
            if (moveDirection != Vector3.zero)
            {             
                transform.forward = moveDirection;
            }
        }
    }*/
    /*public void Charlockon()   Charlockon mit targetgroup
{
    // bug vorhanden, irgendwo wird der targetgroupmember nicht removed
    if (LoadCharmanager.disableattackbuttons == false || LoadCharmanager.gameispaused == false)
    {
        Checkforenemy = Physics.CheckSphere(transform.position, lockonrange, Lockonlayer);
        if (Checkforenemy == true)
        {
            float shortestDistance = 100f;                                                          // Value um später if (distancefromtarget) für einen frame zu resten, der wert muss immer höher sein als distanceformtarget, deswegen infinity
            Collider[] colliders = Physics.OverlapSphere(transform.position, lockonrange);

            for (int i = 0; i < colliders.Length; i++)
            {
                Enemylistcollider = colliders[i].GetComponent<Enemylockon>();
                if (Enemylistcollider != null)
                {
                    if (!availabletargets.Contains(Enemylistcollider))
                    {
                        availabletargets.Add(Enemylistcollider);
                    }
                }
            }
            for (int t = 0; t < availabletargets.Count; t++)
            {
                float distancefromtarget = Vector3.Distance(transform.position, availabletargets[t].transform.position);
                if (distancefromtarget < shortestDistance)
                {
                    if (Steuerung.Spielerboden.Lockon.WasPressedThisFrame() && lockoncheck == false)
                    {
                        LockonUI.SetActive(true);
                        shortestDistance = distancefromtarget;
                        targetgroup.RemoveMember(lockontarget);
                        lockontarget = availabletargets[t].lockontransform;
                        targetgroup.AddMember(lockontarget, 1f, 0);
                        lockoncam.gameObject.SetActive(true);
                        lockoncheck = true;
                        Invoke("changelockoncancel", 0.1f);
                    }
                    if (Steuerung.Spielerboden.Lockonchange.WasReleasedThisFrame() && lockoncheck == true || EnemyHP.switchtargetafterdeath == true && lockoncheck == true || bowair3intoground == true)
                    {
                        LockonUI.SetActive(true);
                        EnemyHP.switchtargetafterdeath = false;
                        shortestDistance = distancefromtarget;
                        targetgroup.RemoveMember(lockontarget);
                        lockontarget = availabletargets[t].lockontransform;
                        targetgroup.AddMember(lockontarget, 1f, 0);
                        Invoke("changelockoncancel", 0.1f);
                    }
                }
            }
        }
        if (Steuerung.Spielerboden.Lockon.WasPerformedThisFrame() && cancellockon == true || Checkforenemy == false)
        {
            targetgroup.RemoveMember(lockontarget);
            lockoncam.gameObject.SetActive(false);
            LockonUI.SetActive(false);
            lockoncheck = false;
            availabletargets.Clear();
            cancellockon = false;
            lockontarget = null;
        }
    }
    /*public void lockon()                                               //Bowaim Lockon
    {
        LockonUI.SetActive(true);
        if(lockontarget != null)
        {
            targetgroup.RemoveMember(lockontarget);
        }
        lockontarget = availabletargets[0].lockontransform;
        targetgroup.AddMember(lockontarget, 1f, 0);
        lockoncam.gameObject.SetActive(true);
    }
}
*/
}

//Aimscript
/*public void aimend()
{
    mousetarget.SetActive(false);
    virtualcam = false;
    Charotation = false;
    Cam1.m_RecenterToTargetHeading.m_enabled = false;
    Cam2.gameObject.SetActive(false);
    activatecam = false;
    arrow.SetActive(false);
    aimanimation = false;
    /*if (Movescript.lockoncheck == true)
    {
        Movementscript.lockon();
    }
}*/

//Bowattack
/*private void bowair3togroundend()
{
    root = false;
    air3downintobasic = false;
    Movementscript.gravitation = 4f;
    Movementscript.runter = -0.5f;

    Ray ray = new Ray(this.transform.position + Vector3.up * 0.3f, Vector3.down);
    if (Physics.Raycast(ray, out RaycastHit hit, 0.4f) && input == false)
    {
        Movementscript.controller.stepOffset = 0.2f;
        Movementscript.attackonceair = true;
        Movementscript.amBoden = true;
        Movementscript.inderluft = false;
        Movementscript.attackabstandboden = false;
        Movementscript.currentstate = null;                                        //airchain, Aircharge muss resetet werden
        animator.SetBool("attack1", true);
        Movementscript.state = Movescript.State.BowGroundattack;
        //Movementscript.lockoncam.gameObject.SetActive(true);
    }
    else
    {
        input = true;
    }
    if (input == true)
    {
        input = false;
        Statics.otheraction = false;
        basicattackcd = 0.5f;
        combochain = 0;
        Movementscript.state = Movescript.State.Actionintoair;
    }
}*/
