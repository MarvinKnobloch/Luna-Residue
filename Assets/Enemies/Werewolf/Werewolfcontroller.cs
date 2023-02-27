using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Werewolfcontroller : MonoBehaviour
{
    [SerializeField] private GameObject werewolfsphere;
    [SerializeField] private float spheredmg;
    [SerializeField] private int buttonpressesneed;
    [SerializeField] private float timetopressbuttons;


    private void Awake()
    {
        werewolfsphere.GetComponent<Werewolfsphere>().basedmg = spheredmg;
        werewolfsphere.GetComponent<Werewolfsphere>().explodetime = timetopressbuttons;

    }
    private void OnEnable()
    {
        werewolfsphere.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        werewolfsphere.SetActive(true);
        if (Statics.dash == false)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtobuttonmashstun(buttonpressesneed);
        }
    }
}
