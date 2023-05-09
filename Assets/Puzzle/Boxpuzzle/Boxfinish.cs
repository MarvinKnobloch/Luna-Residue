using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boxfinish : MonoBehaviour
{
    [SerializeField] private GameObject requiredcube;
    public Color finishcolor;
    private bool isfinished;

    [SerializeField] private GameObject reward;

    private void OnEnable()
    {
        isfinished = false;
    }
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
            if(isfinished == true)
            {
                isfinished = false;
                reward.GetComponent<Rewardinterface>().removerewardcount();
            }
        }
    }
    private void checkforposi()
    {
        isfinished = true;
        requiredcube.GetComponent<Renderer>().material.color = finishcolor;
        reward.GetComponent<Rewardinterface>().addrewardcount();
    }
}
