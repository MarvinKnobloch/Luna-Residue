using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using setplattforms;

public class Checkplattform : MonoBehaviour
{
    public int plattformnumber;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            if(plattformnumber == Setplattforms.currentplattformnumber)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                Setplattforms.currentplattformnumber++;
            }
            else
            {
                Setplattforms.currentplattformnumber = -1;
            }
        }
    }
}
