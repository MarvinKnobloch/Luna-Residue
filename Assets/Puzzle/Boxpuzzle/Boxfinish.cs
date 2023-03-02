using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boxfinish : MonoBehaviour
{
    [SerializeField] private Boxpuzzlecomplete boxpuzzlecomplete;
    [SerializeField] private GameObject requiredcube;
    public Color finishcolor;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == requiredcube)
        {
            Invoke("checkforposi", 0.5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == requiredcube)
        {
            CancelInvoke();
            requiredcube.GetComponent<Renderer>().material.color = Color.white;
            boxpuzzlecomplete.removefinishcount();
        }
    }
    private void checkforposi()
    {
        requiredcube.GetComponent<Renderer>().material.color = finishcolor;
        boxpuzzlecomplete.addfinishcount();
    }
}
