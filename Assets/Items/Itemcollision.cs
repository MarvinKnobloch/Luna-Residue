using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemcollision : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(8, 12);
        Physics.IgnoreLayerCollision(11, 12);
        Physics.IgnoreLayerCollision(6, 12);
    }
    private void OnEnable()
    {
        rb.isKinematic = false;
        int randomx = Random.Range(-4, 4);
        int randomz = Random.Range(-4, 4);
        rb.velocity = new Vector3(randomx, 4, randomz);
        StartCoroutine("disablemovement");
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator disablemovement()
    {
        yield return new WaitForSeconds(5);
        rb.velocity = new Vector3(0, 0, 0);
        rb.isKinematic = true;
    }
}
