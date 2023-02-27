using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzlearrow : MonoBehaviour
{
    public float arrowspeed;
    public float timetodestroy;
    [SerializeField] private BoxCollider boxCollider;
    private float disablecollidertime;
    public Vector3 arrowtarget { get; set; }
    public bool hit { get; set; }
    public bool dmgonce;

    void Awake()
    {
        disablecollidertime = 0;
        Destroy(gameObject, timetodestroy);
        dmgonce = false;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, arrowtarget, arrowspeed * Time.deltaTime);
        if (hit == false && Vector3.Distance(transform.position, arrowtarget) < 0.1f)
        {
            Destroy(gameObject);
        }
        if (hit == true && Vector3.Distance(transform.position, arrowtarget) < 0.1f)
        {
            disablecollidertime += Time.deltaTime;
            if(disablecollidertime >= 0.1f)
            {
                boxCollider.enabled = false;
            }
        }
    }
}
