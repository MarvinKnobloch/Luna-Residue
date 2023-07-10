using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npcendarrow : MonoBehaviour
{
    //ist noch gleich wie der basicarrow, hab 2 versionen falls ich AOE adden will

    public float arrowspeed;
    private float timetodestroyafterhit = 0.2f;
    public bool dmgonce;
    public Vector3 hitposi;
    public Transform Arrowtarget;

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
