using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forthselectionbool : MonoBehaviour
{
    public GameObject charselection;
    private void OnDisable()
    {
        charselection.GetComponent<Opensupportchar>().forthcharselectionactive = false;
    }
}
