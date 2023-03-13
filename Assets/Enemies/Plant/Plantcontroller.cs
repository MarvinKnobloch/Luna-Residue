using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantcontroller : MonoBehaviour
{
    public GameObject[] plantspheres;

    [SerializeField] private float basedmg;
    [SerializeField] private float sphereuptimer;

    private void OnEnable()
    {
        foreach (GameObject sphere in plantspheres)
        {
            sphere.SetActive(true);
        }
        Invoke("dealdmg", sphereuptimer);
    }
    private void dealdmg()
    {
        foreach (GameObject sphere in plantspheres)
        {
            if (sphere.activeSelf == true && Statics.infight == true)
            {
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(basedmg + Globalplayercalculations.calculateenemyspezialdmg());
            }
            sphere.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
