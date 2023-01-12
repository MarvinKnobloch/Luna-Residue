using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzlearrow : MonoBehaviour
{
    public float arrowspeed;
    public float timetodestroy;
    private float destroyafterhit = 0.1f;
    public Vector3 arrowziel { get; set; }
    public bool hit { get; set; }
    public bool dmgonce;
    //public Transform Arrowtarget;

    void Awake()
    {
        Destroy(gameObject, timetodestroy);
        dmgonce = false;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, arrowziel, arrowspeed * Time.deltaTime);
        if (hit == false && Vector3.Distance(transform.position, arrowziel) < 0.1f)
        {
            Destroy(gameObject);
        }
        if (hit == true && Vector3.Distance(transform.position, arrowziel) < 0.1f)
        {
            destroyafterhit -= Time.deltaTime;
            if(destroyafterhit <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
