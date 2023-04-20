using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladinspezial : MonoBehaviour
{
    [SerializeField] private GameObject paladincontroller;

    private void paladinspezial()
    {
        paladincontroller.SetActive(true);
    }
}
