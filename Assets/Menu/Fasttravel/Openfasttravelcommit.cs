using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Openfasttravelcommit : MonoBehaviour, IPointerEnterHandler
{
    private Fasttravelpoints fasttravelpoints;
    public Vector3 setfasttravelpoint;
    public Vector2 setpointonmap;
    private GameObject commitfasttravelobj;


    private void Awake()
    {
        fasttravelpoints = GetComponentInParent<Fasttravelpoints>();
        commitfasttravelobj = fasttravelpoints.fasttravelcommit;
    }
    public void opencommit()
    {
        commitfasttravelobj.SetActive(true);
        commitfasttravelobj.GetComponent<Commitfasttravel>().fasttravelpoint = setfasttravelpoint;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("hallo");
        
        if (fasttravelpoints.mapimage.activeSelf == false) fasttravelpoints.mapimage.SetActive(true);
        fasttravelpoints.mapimage.GetComponent<RectTransform>().anchoredPosition = setpointonmap;
    }
}
