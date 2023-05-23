using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playernature
{
    public Movescript psm;

    public void naturethendrilstart()
    {
        if (Movescript.lockontarget != null)
        {
            Vector3 endposi = Movescript.lockontarget.transform.position + (psm.transform.right * -4 + psm.transform.up * 1.5f);
            Vector3 test = endposi - psm.transform.position;
            Vector3 move = test.normalized * 15 * Time.deltaTime;
            psm.charactercontroller.Move(move);
            if (Vector3.Distance(psm.transform.position, endposi) < 1)
            {
                psm.nature1startpos = psm.transform.position;
                psm.state = Movescript.State.Naturethendrilgettotarget;
                psm.starttime = Time.time;
            }
        }
        else
        {
            psm.Abilitiesend();
        }
    }
    public void naturethendrilgettotarget()
    {
        Vector3 endposi = Movescript.lockontarget.transform.position;// + (psm.transform.forward * 3 + psm.transform.right * 1);
        Vector3 center = (psm.nature1startpos + Movescript.lockontarget.position) * 0.5f;

        center -= new Vector3(1, 0, 0);

        Vector3 startRelcenter = psm.nature1startpos - center;
        Vector3 endRelcenter = endposi - center;

        float fracComplete = (Time.time - psm.starttime) / psm.nature1traveltime * psm.nature1speed;

        Vector3 newposi = Vector3.Slerp(startRelcenter, endRelcenter, fracComplete);
        newposi += center;
        Vector3 lookPos = newposi;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        psm.transform.rotation = Quaternion.Slerp(psm.transform.rotation, rotation, 100 * Time.deltaTime);
        psm.transform.position = newposi;

        if (Vector3.Distance(psm.transform.position, endposi) < 1f)
        {
            psm.state = Movescript.State.Naturethendrilfinalmovement;
            psm.eleAbilities.overlapssphereeledmg(Movescript.lockontarget.gameObject, 3, 18);
        }
    }
    public void naturethendrilfinalmovement()
    {
        psm.transform.position = Vector3.MoveTowards(psm.transform.position, Movescript.lockontarget.position, 10 * Time.deltaTime);
        psm.transform.rotation = Quaternion.LookRotation(Movescript.lockontarget.transform.position - psm.transform.position, Vector3.up);
        if (Vector3.Distance(psm.transform.position, Movescript.lockontarget.transform.position) < 2)
        {
            Vector3 lookPos = Movescript.lockontarget.transform.position - psm.transform.position;
            lookPos.y = 0;
            psm.transform.rotation = Quaternion.LookRotation(lookPos);
            psm.Abilitiesend();
        }
    }
}
