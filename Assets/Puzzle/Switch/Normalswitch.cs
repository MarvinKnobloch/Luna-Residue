using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normalswitch : MonoBehaviour
{
    public GameObject[] activatedred;
    public GameObject[] activateblue;

    public GameObject[] switches;

    public static bool switchcolor;
    public static bool timeswitch;

    private Color redcolor;
    private Color bluecolor;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#B72020", out redcolor);
        ColorUtility.TryParseHtmlString("#2944D6", out bluecolor);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Puzzlearrow"))
        {
            if (switchcolor == false)
            {
                timeswitch = true;
                switchcolor = true;
                changetoblue();
            }
            else
            {
                timeswitch = false;
                switchcolor = false;
                changetored();
            }
        }      
    }
    private void changetoblue()
    {
        foreach (GameObject obj in switches)
        {
            if (obj.GetComponent<Timeswitch>())
            {
                if (obj.GetComponent<Timeswitch>().maxreached == false)
                {
                    obj.GetComponent<Renderer>().material.color = bluecolor;
                }
            }
            else if (obj.GetComponent<Switchwithtimer>())
            {
                if (obj.GetComponent<Switchwithtimer>().maxreached == false)
                {
                    obj.GetComponent<Renderer>().material.color = bluecolor;
                }
            }
            else
            {
                obj.GetComponent<Renderer>().material.color = bluecolor;
            }
        }
        foreach (GameObject blue in activateblue)
        {
            blue.gameObject.transform.localScale = new Vector3(blue.transform.localScale.x, 6, blue.transform.localScale.z);
        }
        foreach (GameObject red in activatedred)
        {
            red.gameObject.transform.localScale = new Vector3(red.transform.localScale.x, 0.001f, red.transform.localScale.z);
        }
    }
    private void changetored()
    {
        foreach (GameObject obj in switches)
        {
            if (obj.GetComponent<Timeswitch>())
            {
                if (obj.GetComponent<Timeswitch>().maxreached == false)
                {
                    obj.GetComponent<Renderer>().material.color = redcolor;
                }
            }
            else if (obj.GetComponent<Switchwithtimer>())
            {
                if (obj.GetComponent<Switchwithtimer>().maxreached == false)
                {
                    obj.GetComponent<Renderer>().material.color = redcolor;
                }
            }
            else
            {
                obj.GetComponent<Renderer>().material.color = redcolor;
            }
        }
        foreach (GameObject red in activatedred)
        {
            red.gameObject.transform.localScale = new Vector3(red.transform.localScale.x, 6, red.transform.localScale.z);
        }

        foreach (GameObject blue in activateblue)
        {
            blue.gameObject.transform.localScale = new Vector3(blue.transform.localScale.x, 0.001f, blue.transform.localScale.z);
        }
    }
}
