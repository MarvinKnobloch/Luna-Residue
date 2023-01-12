using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resettargets : MonoBehaviour
{
    public GameObject gate;

    public void resettarget()
    {
        gate.gameObject.GetComponent<Targetgate>().targethits = 0;
    }
}
