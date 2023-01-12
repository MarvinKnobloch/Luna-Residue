using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Changeform : MonoBehaviour, Interactioninterface
{
    [SerializeField] private string actiontext = "Change";
    public string Interactiontext => actiontext;
    public GameObject lantern;
    
    private Color browncolor;
    public int state;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#4D2505", out browncolor);
    }

    public bool Interact(Closestinteraction interactor)
    {
        if(state == 0)
        {
            state++;
            lantern.SetActive(true);
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        else if(state == 1)
        {
            state++;
            lantern.SetActive(false);
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if(state == 2)
        {
            state = 0;
            gameObject.GetComponent<Renderer>().material.color = browncolor;
        }
        return true;
    }
    public void lanternreset()
    {
        state = 0;
        gameObject.GetComponent<Renderer>().material.color = browncolor;
        lantern.SetActive(false);
    }
}
