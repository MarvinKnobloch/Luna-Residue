using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishmanspezial : MonoBehaviour
{
    [SerializeField] private Fishmancontroller spezialcontroller;
    private void fishmanspezial()
    {
        spezialcontroller.gameObject.SetActive(true);
    }
}
