using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mathmanspezial : MonoBehaviour
{
    [SerializeField] private GameObject mathmancontroller;

    const string dazestate = "Daze";
    private void mathspezial()
    {
        if (Statics.dash == false)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtostun();
            mathmancontroller.SetActive(true);
        }
    }
}
