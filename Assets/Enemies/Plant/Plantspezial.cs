using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plantspezial : MonoBehaviour
{
    [SerializeField] private Plantcontroller plantcontroller;

    private void plantspezial()
    {
        plantcontroller.enemyposi = gameObject.transform.position;
        plantcontroller.gameObject.SetActive(true);
    }
}
