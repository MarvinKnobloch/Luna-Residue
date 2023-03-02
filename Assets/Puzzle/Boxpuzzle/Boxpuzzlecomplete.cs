using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxpuzzlecomplete : MonoBehaviour
{
    [SerializeField] private int boxfinishcount;
    [SerializeField] private int boxfinishneeded;

    private void OnEnable()
    {
        boxfinishcount = 0;
    }
    public void addfinishcount()
    {
        boxfinishcount++;
        if(boxfinishcount == boxfinishneeded)
        {
            Debug.Log("reward");
        }
    }
    public void removefinishcount()
    {
        boxfinishcount--;
    }
}
