using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuepuzzlecomplete : MonoBehaviour
{
    public int finish;
    public int neededfinish;

    private void OnEnable()
    {
        Statuefinish.finish += done;
        Statuefinish.notfinish += notdone;
    }
    private void OnDisable()
    {
        Statuefinish.finish -= done;
        Statuefinish.notfinish -= notdone;
    }

    private void done()
    {
        finish += 1;
        if(finish >= neededfinish)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
    }
    private void notdone()
    {
        finish -= 1;
    }
}
