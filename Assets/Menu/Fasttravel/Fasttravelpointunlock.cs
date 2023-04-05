using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fasttravelpointunlock : MonoBehaviour
{
    [SerializeField] private Areacontroller areacontroller;
    [SerializeField] Travelpointvalues travelpointvalue;
    public int fasttravelnumber;

    private void Start()
    {
        Debug.Log("start");
        fasttravelnumber = GetComponent<Areanumber>().areanumber;
    }
    private void OnEnable()
    {
        StartCoroutine("currentstate");                          //ein frame delay damit die settings für die area vorher geladen werden
    }
    IEnumerator currentstate()
    {
        yield return null;
        Debug.Log("load");
        if (areacontroller.gotfasttravelpoint[fasttravelnumber] == true)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            addtravelpoint();
        } 
        else gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
        private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar.gameObject && areacontroller.gotfasttravelpoint[fasttravelnumber] == false)
        {
            areacontroller.gotfasttravelpoint[fasttravelnumber] = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            addtravelpoint();
        }
    }
    private void addtravelpoint()
    {
        if (Fasttravelpoints.travelpoints.Contains(travelpointvalue) == false)
        {
            Fasttravelpoints.travelpoints.Add(travelpointvalue);
        }
    }
}
