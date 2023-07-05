using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblincontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] spheres;

    [SerializeField] private float spheredmg;
    private int spherenumber;
    [SerializeField] private float timebetweenspawn;
    [SerializeField] private float explodetimer;
    private int castnumber;
    [SerializeField] private int castsphereamount;

    private void Awake()
    {
        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i].GetComponent<Goblinsphere>().basedmg = spheredmg;
            spheres[i].GetComponent<Goblinsphere>().timetoexplode = explodetimer;
        }
    }
    private void OnEnable()
    {
        StopCoroutine("controllerdisable");
        spherenumber = 0;
        castnumber = 0;
        spheres[spherenumber].transform.position = LoadCharmanager.Overallmainchar.transform.position;
        spheres[spherenumber].SetActive(true);
        spherenumber++;
        castnumber++;
        InvokeRepeating("spezial", timebetweenspawn, timebetweenspawn);
    }
    private void spezial()
    {
        spheres[spherenumber].transform.position = LoadCharmanager.Overallmainchar.transform.position;
        spheres[spherenumber].SetActive(true);
        if (spherenumber >= 2) spherenumber = 0;
        else spherenumber++;
        castnumber++;
        if(castnumber >= castsphereamount)
        {
            CancelInvoke();
            StartCoroutine("controllerdisable");
        }
    }
    IEnumerator controllerdisable()
    {
        yield return new WaitForSeconds(0.6f + explodetimer);
        gameObject.SetActive(false);
    }
}
