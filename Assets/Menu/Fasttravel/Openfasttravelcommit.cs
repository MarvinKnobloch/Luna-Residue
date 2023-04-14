using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Openfasttravelcommit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Travelpointvalues travelpoint;
    private GameObject commitfasttravelobj;
    private GameObject travelpointnametext;


    private void Awake()
    {
        commitfasttravelobj = GetComponentInParent<Fasttravelpoints>().fasttravelcommit;
        travelpointnametext = GetComponentInParent<Fasttravelpoints>().travelpointnametext;
    }
    public void opencommit()
    {
        commitfasttravelobj.SetActive(true);
        commitfasttravelobj.GetComponent<Commitfasttravel>().fasttravelpoint = travelpoint.travelcordinates;
        commitfasttravelobj.GetComponentInChildren<TextMeshProUGUI>().text = "Fastravel to " + travelpoint.name + "?";
        travelpointnametext.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        travelpointnametext.gameObject.transform.position = transform.position + new Vector3(0, 35 , 0);
        travelpointnametext.SetActive(true);
        travelpointnametext.GetComponentInChildren<TextMeshProUGUI>().text = travelpoint.name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        travelpointnametext.SetActive(false);
    }
}
