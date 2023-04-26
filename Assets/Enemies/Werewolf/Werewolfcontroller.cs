using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Werewolfcontroller : MonoBehaviour
{
    [SerializeField] private Werewolfsphere werewolfsphere;
    [SerializeField] private int buttonpressesneed;
    [SerializeField] private float timetopressbuttons;
    [SerializeField] private float basedmg;

    private void Awake()
    {
        werewolfsphere.explodetime = timetopressbuttons;
        werewolfsphere.basedmg = basedmg;
    }
    private void OnEnable()
    {
        werewolfsphere.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        werewolfsphere.gameObject.SetActive(true);
        if (Statics.dash == false)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtobuttonmashstun(buttonpressesneed);
        }
    }
}
