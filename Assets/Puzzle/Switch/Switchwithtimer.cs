using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Switchwithtimer : MonoBehaviour
{
    public GameObject[] activatedred;
    public GameObject[] activateblue;

    public GameObject[] switches;

    public GameObject Timerui;
    public Text timetext;

    private float remainingtime;
    public float switchtimer;

    public int maxstore = 2;
    public bool maxreached;
    private int currentstore;

    private Color redcolor;
    private Color bluecolor;

    [SerializeField] private TextMeshProUGUI floatingtimertext;

    private void Awake()
    {
        floatingtimertext = GetComponentInChildren<TextMeshProUGUI>();
        floatingtimertext.text = switchtimer.ToString();
        ColorUtility.TryParseHtmlString("#B72020", out redcolor);
        ColorUtility.TryParseHtmlString("#2944D6", out bluecolor);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Puzzlearrow"))
        {
            if (currentstore < maxstore)
            {

                if (Normalswitch.timeswitch == false)
                {
                    if(Timerui.activeSelf == false)
                    {
                        Normalswitch.timeswitch = true;
                        currentstore += 1;
                        gameObject.GetComponent<Renderer>().material.color = bluecolor;
                        if (currentstore >= maxstore)
                        {
                            maxreached = true;
                            gameObject.GetComponent<Renderer>().material.color = Color.gray;
                        }
                        Timerui.SetActive(true);
                        StartCoroutine("bluestart");
                    }
                }
                else
                {
                    if(Timerui.activeSelf == false)
                    {
                        Normalswitch.timeswitch = false;
                        currentstore += 1;
                        Invoke("changetored", switchtimer);
                        gameObject.GetComponent<Renderer>().material.color = redcolor;
                        if (currentstore >= maxstore)
                        {
                            maxreached = true;
                            gameObject.GetComponent<Renderer>().material.color = Color.gray;
                        }
                        Timerui.SetActive(true);
                        StartCoroutine("redstart");
                    }
                }
            }
        }
    }
    IEnumerator bluestart()
    {
        timetext.color = Color.green;
        remainingtime = switchtimer;
        while (true)
        {
            remainingtime -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(remainingtime % 60);
            float milliseconds = remainingtime % 1 * 100;
            timetext.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
            if (remainingtime <= 5)
            {
                timetext.color = Color.red;
            }
            if (remainingtime <= 0)
            {
                changetoblue();
                Timerui.SetActive(false);
                StopCoroutine("bluestart");
            }
            yield return null;
        }
    }
    IEnumerator redstart()
    {
        timetext.color = Color.green;
        remainingtime = switchtimer;
        while (true)
        {
            remainingtime -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(remainingtime % 60);
            float milliseconds = remainingtime % 1 * 100;
            timetext.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
            if (remainingtime <= 5)
            {
                timetext.color = Color.red;
            }
            if (remainingtime <= 0)
            {
                changetored();
                Timerui.SetActive(false);
                StopCoroutine("redstart");
            }
            yield return null;
        }
    }
    private void changetoblue()
    {
        currentstore -= 1;
        maxreached = false;
        Normalswitch.switchcolor = true;
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
        currentstore -= 1;
        maxreached = false;
        Normalswitch.switchcolor = false;
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
    public void resettimer()
    {
        currentstore = 0;
        maxreached = false;
        remainingtime = 0;
        Timerui.SetActive(false);
    }
}
