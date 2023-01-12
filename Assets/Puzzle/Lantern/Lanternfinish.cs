using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanternfinish : MonoBehaviour
{
    public int finishneeded;
    public int lanternfinish;
    public void checkforfinish()
    {
        lanternfinish++;
        if(finishneeded == lanternfinish)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }
    public void removelanternfromfinish()
    {
        lanternfinish--;
    }
}
