using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fasttravelpointunlock : MonoBehaviour
{
    [SerializeField] private Areacontroller areacontroller;
    [SerializeField] Travelpointvalues travelpointvalue;
    [SerializeField] private GameObject ingamemessage;
    public int fasttravelnumber;

    private void Start()
    {
        fasttravelnumber = GetComponent<Areanumber>().areanumber;
    }
    private void OnEnable()
    {
        StartCoroutine("currentstate");                          //ein frame delay damit die settings für die area vorher geladen werden
    }
    IEnumerator currentstate()
    {
        yield return null;
        if (areacontroller.gotfasttravelpoint[fasttravelnumber] == true)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            addtravelpoint();
        } 
        else gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
        private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar.gameObject && areacontroller.gotfasttravelpoint[fasttravelnumber] == false && Statics.infight == false)
        {
            areacontroller.gotfasttravelpoint[fasttravelnumber] = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            LoadCharmanager.autosave();
            if(ingamemessage.activeSelf == true) ingamemessage.GetComponent<Ingamemessagecontroller>().cancelfadeout();
            else ingamemessage.SetActive(true);
            ingamemessage.GetComponentInChildren<TextMeshProUGUI>().text = "New Fast Travel Point unlocked: " + "\n" + "(" + travelpointvalue.travelpointname + ")";
            addtravelpoint();
        }
    }
    public void addtravelpoint()
    {
        if (Fasttravelpoints.travelpoints.Contains(travelpointvalue) == false)
        {
            Fasttravelpoints.travelpoints.Add(travelpointvalue);
        }
    }
}