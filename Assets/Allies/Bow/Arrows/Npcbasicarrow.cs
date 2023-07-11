using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Npcbasicarrow : MonoBehaviour
{
    //ist noch gleich wie der endarrow, hab 2 versionen falls ich AOE adden will

    public float arrowspeed;
    public float timetodestroyafterhit = 0.2f;
    [NonSerialized] public bool dmgonce;
    [NonSerialized] public Vector3 hitposi;
    [NonSerialized] public Transform Arrowtarget;

    public float basicdmgtodeal;


    void Update()
    {
        if (Arrowtarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, hitposi, arrowspeed * Time.deltaTime);
            if (dmgonce == false && Vector3.Distance(transform.position, hitposi) < 0.1f)
            {
                Checkhitboxbasic();
                dmgonce = true;
                StartCoroutine(destroyarrow());
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
    IEnumerator destroyarrow()
    {
        yield return new WaitForSeconds(timetodestroyafterhit);
        Destroy(gameObject);
    }
}
