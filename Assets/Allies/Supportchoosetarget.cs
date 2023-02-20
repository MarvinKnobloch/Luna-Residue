using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supportchoosetarget
{
    public Supportmovement ssm;

    const string idlestate = "Idle";
    public void settarget()
    {
        if(Infightcontroller.infightenemylists.Count != 0)
        {
            int enemycount = Infightcontroller.infightenemylists.Count;
            int newtarget = Random.Range(1, enemycount);            //enemycount + 1?
            ssm.currenttarget = Infightcontroller.infightenemylists[newtarget - 1].gameObject;
            ssm.attackrangecheck = (float)(ssm.currenttarget.GetComponent<CapsuleCollider>().radius + ssm.addedattackrangetocollider);
            ssm.switchtoweaponstate();
        }
        else
        {
            ssm.ChangeAnimationState(idlestate);
            ssm.state = Supportmovement.State.reset;
        }
    }
    public void playerfocustarget()
    {
        if (Movescript.lockontarget != null)
        {
            ssm.currenttarget = Movescript.lockontarget.gameObject;
            if (ssm.currenttarget.GetComponent<CapsuleCollider>())                                            //gegner muss ein Capuslecollider haben
            {
                ssm.attackrangecheck = (float)(ssm.currenttarget.GetComponent<CapsuleCollider>().radius + ssm.addedattackrangetocollider);
            }
        }
    }
}
