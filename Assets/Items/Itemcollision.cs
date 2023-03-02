using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemcollision : MonoBehaviour
{
    private Rigidbody rb;
    private int itemlaunch = 6;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //ignore collision in Setitemandinventorys
    }
    private void OnEnable()
    {
        rb.isKinematic = false;
        int randomx = Random.Range(-itemlaunch, itemlaunch);
        int randomz = Random.Range(-itemlaunch, itemlaunch);
        rb.velocity = new Vector3(randomx, itemlaunch, randomz);
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
