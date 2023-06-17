using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzlearrow : MonoBehaviour
{
    public float arrowspeed;
    public float timetodestroy;
    [SerializeField] private BoxCollider boxCollider;
    public Vector3 arrowtarget { get; set; }
    public bool hit { get; set; }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, arrowtarget, arrowspeed * Time.deltaTime);
        if (hit == true && Vector3.Distance(transform.position, arrowtarget) < 0.1f)
        {
            hit = false;
            Weaponsounds.instance.playarrowimpact();
            StartCoroutine(disablecollider());
            StartCoroutine(destroy());
        }
    }
    IEnumerator disablecollider()
    {
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = false;
    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
