using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Npcbasicarrow : MonoBehaviour
{
    //ist noch gleich wie der endarrow, hab 2 versionen falls ich AOE adden will

    public float arrowspeed;
    public float timetodestroy;
    public bool dmgonce;
    public Transform Arrowtarget;

    public float basicdmgtodeal;

    void Start()
    {
        Destroy(gameObject, timetodestroy);
    }

    void Update()
    {
        if (Arrowtarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Arrowtarget.position, arrowspeed * Time.deltaTime);
            if (dmgonce == false && Vector3.Distance(transform.position, Arrowtarget.position) < 0.1f)
            {
                Checkhitboxbasic();
                dmgonce = true;
            }
        }
    }
    private void Checkhitboxbasic()
    {
        if (Arrowtarget != null)
        {
            if (Arrowtarget.TryGetComponent(out EnemyHP enemyhp))
            {
                enemyhp.takesupportdmg(basicdmgtodeal);
            }
        }
    }
}
