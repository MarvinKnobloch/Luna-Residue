using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantcontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] plantspheres;

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
            if (sphere.activeSelf == true)
            {
                LoadCharmanager.Overallmainchar.GetComponent<SpielerHP>().TakeDamage(basedmg);
                sphere.SetActive(false);
            }
        }
        gameObject.SetActive(false);
    }
}
