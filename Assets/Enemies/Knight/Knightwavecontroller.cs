using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Knightwavecontroller : MonoBehaviour
{
    public Vector3 endposi;
    [NonSerialized] public float basedmg;
    [NonSerialized] public float speed;

    private void Update()
    {
        RaycastHit hit;
        Ray nachunten = new Ray(LoadCharmanager.Overallmainchar.transform.position, Vector3.down * 5);
        if (Physics.Raycast(nachunten, out hit))
        {
            endposi.y = hit.point.y - 1;
        }
        transform.position = Vector3.MoveTowards(transform.position, endposi, speed);
        if (Vector3.Distance(transform.position, endposi) < 2)
        {
            gameObject.SetActive(false);
        }
    }
}
