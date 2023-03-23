using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Targetsactivate : MonoBehaviour, Interactioninterface
{
    [SerializeField] private string actiontext = "Activate";
    public string Interactiontext => actiontext;
    public GameObject[] targets;
    public float remainingtime;
    public float overalltime;
    public Text timetext;
    public GameObject timeUI;

    [SerializeField] private GameObject finishtarget;
    public bool Interact(Closestinteraction interactor)
    {
        if(Statics.timer == false)
        {
            finishtarget.GetComponent<Rewardinterface>().resetrewardcount();
            timeUI.SetActive(true);
            Statics.timer = true;
            StartCoroutine("starttimer");
            foreach (GameObject obj in targets)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            timeUI.SetActive(false);
            Statics.timer = false;
            StopCoroutine("starttimer");
            foreach (GameObject obj in targets)
            {
                obj.SetActive(false);
            }
        }
        return true;
    }
    IEnumerator starttimer()
    {
        timetext.color = Color.green;
        remainingtime = overalltime;
        while (true)
        {
            remainingtime -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(remainingtime % 60);
            float milliseconds = remainingtime % 1 * 100;
            timetext.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
            
            if(remainingtime <= 5)
            {
                timetext.color = Color.red;
            }
            if (remainingtime <= 0)
            {
                timeUI.SetActive(false);
                timetext.text = "0";
                Statics.timer = false;
                remainingtime = 0;
                foreach (GameObject obj in targets)
                {
                    obj.SetActive(false);
                }
                StopCoroutine("starttimer");
            }
            yield return null;
        }
    }
}

