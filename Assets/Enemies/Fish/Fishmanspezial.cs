using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishmanspezial : MonoBehaviour
{
    [SerializeField] private GameObject fishmancontroller;
    private void fishmanspezial()
    {
        fishmancontroller.SetActive(true);
    }
}
