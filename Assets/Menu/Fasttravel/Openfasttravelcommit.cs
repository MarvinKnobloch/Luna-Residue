using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openfasttravelcommit : MonoBehaviour
{
    public Vector3 setfasttravelpoint;
    private GameObject commitfasttravelobj;

    private void Awake()
    {
        commitfasttravelobj = GetComponentInParent<Fasttravelpoints>().fasttravelcommit;
    }
    public void opencommit()
    {
        commitfasttravelobj.SetActive(true);
        commitfasttravelobj.GetComponent<Commitfasttravel>().fasttravelpoint = setfasttravelpoint;
    }
}
