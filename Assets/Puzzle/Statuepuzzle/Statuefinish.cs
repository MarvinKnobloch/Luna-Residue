using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Statuefinish : MonoBehaviour
{
    [SerializeField] private GameObject Reward;

    private Color greencolor;
    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#2C6536", out greencolor);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Statue"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = greencolor;
            Reward.GetComponent<Rewardinterface>().addrewardcount();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Statue"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.white;
            Reward.GetComponent<Rewardinterface>().removerewardcount();
        }
    }
}
