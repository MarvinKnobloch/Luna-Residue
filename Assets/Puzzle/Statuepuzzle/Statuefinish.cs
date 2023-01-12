using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Statuefinish : MonoBehaviour
{
    private Color greencolor;
    public static event Action finish;
    public static event Action notfinish;


    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#2C6536", out greencolor);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Statue"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = greencolor;
            finish?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Statue"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.white;
            notfinish?.Invoke();
        }
    }
}
