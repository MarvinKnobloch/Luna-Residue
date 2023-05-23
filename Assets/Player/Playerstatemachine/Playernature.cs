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
                psm.startpos = psm.transform.position;
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
        Vector3 endposi = Movescript.lockontarget.transform.position + (psm.transform.forward * 3 + psm.transform.right * 1);
        Vector3 center = (psm.startpos + Movescript.lockontarget.position) * 0.5f;

        center -= new Vector3(1, 0, 0);

        Vector3 startRelcenter = psm.startpos - center;
        Vector3 endRelcenter = endposi - center;

        float fracComplete = (Time.time - psm.starttime) / psm.nature1traveltime * psm.nature1speed;

        psm.transform.position = Vector3.Slerp(startRelcenter, endRelcenter, fracComplete);
        psm.transform.position += center;

        if (Vector3.Distance(psm.transform.position, endposi) < 1f)
        {
            Vector3 lookPos = Movescript.lockontarget.transform.position - psm.transform.position;
            lookPos.y = 0;
            psm.transform.rotation = Quaternion.LookRotation(lookPos);

            psm.eleAbilities.overlapssphereeledmg(Movescript.lockontarget.gameObject, 3, 18);
            psm.Abilitiesend();
        }
    }
}
