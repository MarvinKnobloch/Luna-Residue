using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Knightwavecontroller : MonoBehaviour
{
    public Vector3 endposi;
    [NonSerialized] public float basedmg;
    [NonSerialized] public float dmglevel;
    [NonSerialized] public float speed;


    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endposi, speed);
        if (Vector3.Distance(transform.position, endposi) < 2)
        {
            gameObject.SetActive(false);
        }
    }
}
