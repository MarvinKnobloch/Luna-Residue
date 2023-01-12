using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preventcharpushdown : MonoBehaviour
{
    [SerializeField] private GameObject[] plattformmove;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar)
        {
            foreach(GameObject plattforms in plattformmove)
            {
                plattforms.GetComponent<Movetowerform>().state = Movetowerform.State.moveout;
                plattforms.GetComponent<Movetowerform>().movetime = 0f;
            }
        }
    }
}
