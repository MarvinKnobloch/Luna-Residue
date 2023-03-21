using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantcontroller : MonoBehaviour
{
    public GameObject[] plantspheres;

    [SerializeField] private float basedmg;
    [SerializeField] private float sphereuptimer;

    private Color redcolor;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#B72020", out redcolor);
        redcolor.a = 0.6f;
    }
    private void OnEnable()
    {
        foreach (GameObject sphere in plantspheres)
        {          
            sphere.SetActive(true);
        }
        plantspheres[0].GetComponent<Plantsphere>().isactivsphere = true;
        plantspheres[1].GetComponent<MeshRenderer>().material.color = redcolor;
        plantspheres[2].GetComponent<MeshRenderer>().material.color = redcolor;
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
            sphere.GetComponent<Plantsphere>().isactivsphere = false;
            sphere.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
