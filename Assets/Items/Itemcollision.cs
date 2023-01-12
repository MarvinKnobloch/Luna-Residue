using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemcollision : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        int randomx = Random.Range(-4, 4);
        int randomz = Random.Range(-4, 4);
        rb.velocity = new Vector3(randomx, 4, randomz);
        Physics.IgnoreLayerCollision(8, 12);
        Physics.IgnoreLayerCollision(11, 12);
        Physics.IgnoreLayerCollision(6, 12);
    }
}
