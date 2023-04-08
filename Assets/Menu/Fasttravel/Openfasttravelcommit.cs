using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Openfasttravelcommit : MonoBehaviour
{
    public Travelpointvalues travelpoint;
    private GameObject commitfasttravelobj;


    private void Awake()
    {
        commitfasttravelobj = GetComponentInParent<Fasttravelpoints>().fasttravelcommit;
    }
    public void opencommit()
    {
        commitfasttravelobj.SetActive(true);
        commitfasttravelobj.GetComponent<Commitfasttravel>().fasttravelpoint = travelpoint.travelcordinates;
        commitfasttravelobj.GetComponentInChildren<TextMeshProUGUI>().text = "Fastravel to " + travelpoint.name + "?";
    }
}
