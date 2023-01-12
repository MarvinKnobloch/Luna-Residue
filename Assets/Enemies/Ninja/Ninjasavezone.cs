using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninjasavezone : MonoBehaviour
{
    [SerializeField] private GameObject ninjastarcontroller;

    private void OnEnable()
    {
        Invoke("turnoff", 6f);
    }
    private void turnoff()
    {
        ninjastarcontroller.SetActive(false);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemythrowable"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
