using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supportutilityfunctions
{
    public Supportmovement ssm;

    const string idlestate = "Idle";
    const string runstate = "Run";

    public void attackstate()
    {
        if (ssm.currenttarget == null)
        {
            ssm.switchtarget();
        }
        else
        {
            supportreset();
            ssm.FaceTraget();
        }
    }
    public void supportreset()
    {
        if (ssm.playerhp.playerisdead == false)
        {
            ssm.resettimer += Time.deltaTime;
            if (ssm.resettimer > 0.4f)
            {
                if (Vector3.Distance(ssm.currenttarget.transform.position, LoadCharmanager.Overallmainchar.transform.position) > ssm.supportresetrange)
                {
                    ssm.ChangeAnimationState(runstate);
                    ssm.state = Supportmovement.State.reset;
                }
                ssm.resettimer = 0;
            }
        }
    }
    public void resetcombat()
    {
        ssm.resetcombattimer += Time.deltaTime;
        if (ssm.resetcombattimer > 0.4f)
        {
            if (Vector3.Distance(ssm.transform.position, LoadCharmanager.Overallmainchar.transform.position) < 3)
            {
                if (Statics.infight == true && Infightcontroller.infightenemylists.Count != 0)
                {
                    int enemycount = Infightcontroller.infightenemylists.Count;
                    int newtarget = Random.Range(1, enemycount);            //enemycount + 1?
                    ssm.currenttarget = Infightcontroller.infightenemylists[newtarget - 1].gameObject;
                    ssm.attackrangecheck = ssm.currenttarget.GetComponent<CapsuleCollider>().radius + ssm.addedattackrangetocollider;
                    ssm.switchtoweaponstate();
                    return;
                }
                ssm.ChangeAnimationState(idlestate);
                ssm.Meshagent.ResetPath();
                ssm.resetcombattimer = 0;
            }
            else
            {
                ssm.Meshagent.SetDestination(LoadCharmanager.Overallmainchar.transform.position);
                ssm.ChangeAnimationState(runstate);
            }
            ssm.resetcombattimer = 0;
        }
    }
}
