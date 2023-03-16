using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuecontroller : MonoBehaviour
{
    private Vector3 startposi;
    public GameObject[] statues;
    public LayerMask statueraylayer;

    private void Awake()
    {
        startposi = transform.position;
    }
    public void resetstatues()
    {
        foreach(GameObject obj in statues)
        {
            obj.transform.position = obj.gameObject.GetComponent<Statuecontroller>().startposi;
        }
    }

}
