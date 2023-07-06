using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblincontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] spheres;
    [SerializeField] private GameObject bigsphere;

    [SerializeField] private float spheredmg;
    private int spherenumber;
    [SerializeField] private float timebetweenspawn;
    [SerializeField] private float explodetimer;
    private int castnumber;
    [SerializeField] private int castsphereamount;

    private int randombigsphere;

    private void Awake()
    {
        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i].GetComponent<Goblinsphere>().basedmg = spheredmg;
            spheres[i].GetComponent<Goblinsphere>().timetoexplode = explodetimer;
        }
        bigsphere.GetComponent<Goblinbigsphere>().basedmg = spheredmg;
        bigsphere.GetComponent<Goblinbigsphere>().timetoexplode = explodetimer;
    }
    private void OnEnable()
    {
        StopCoroutine("controllerdisable");
        spherenumber = 0;
        castnumber = 0;
        randombigsphere = Random.Range(2, castsphereamount);
        spheres[spherenumber].transform.position = LoadCharmanager.Overallmainchar.transform.position;
        spheres[spherenumber].SetActive(true);
        spherenumber++;
        castnumber++;
        InvokeRepeating("spezial", timebetweenspawn, timebetweenspawn);
    }
    private void spezial()
    {
        if (randombigsphere == castnumber)
        {
            bigsphere.transform.position = LoadCharmanager.Overallmainchar.transform.position;
            bigsphere.SetActive(true);
        }
        else
        {
            spheres[spherenumber].transform.position = LoadCharmanager.Overallmainchar.transform.position;
            spheres[spherenumber].SetActive(true);
        }
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
        yield return new WaitForSeconds(0.9f + explodetimer);
        gameObject.SetActive(false);
    }
}
