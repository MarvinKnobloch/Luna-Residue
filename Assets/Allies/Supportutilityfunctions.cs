using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Supportutilityfunctions
{
    public Supportmovement ssm;

    private float distance;
    private float maxtriggerdistance = 50;

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
                ssm.meshagent.ResetPath();
                ssm.resetcombattimer = 0;
            }
            else
            {
                ssm.meshagent.SetDestination(LoadCharmanager.Overallmainchar.transform.position);
                ssm.ChangeAnimationState(runstate);
            }
            ssm.resetcombattimer = 0;
        }
    }
    public void checkforpath()
    {
        ssm.meshagent.CalculatePath(LoadCharmanager.Overallmainchar.transform.position, ssm.path);
        if (ssm.path.status == NavMeshPathStatus.PathComplete)
        {
            Debug.Log("gotpath");
            if (checkdistance() == false) LoadCharmanager.Overallmainchar.GetComponent<Movescript>().spawnteammates(ssm.gameObject);
        }
        else
        {
            Debug.Log("gotnopath");
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().spawnteammates(ssm.gameObject);
        } 
    }
    private bool checkdistance()
    {
        distance = 0;
        Vector3[] corners = ssm.path.corners;

        if (corners.Length > 2)
        {
            for (int i = 1; i < corners.Length; i++)
            {
                Vector2 previous = new Vector2(corners[i - 1].x, corners[i - 1].z);
                Vector2 current = new Vector2(corners[i].x, corners[i].z);

                distance += Vector2.Distance(previous, current);
                if (distance > maxtriggerdistance) return false;
            }
            return true;
        }
        else
        {
            return true;
        }
    }
}
