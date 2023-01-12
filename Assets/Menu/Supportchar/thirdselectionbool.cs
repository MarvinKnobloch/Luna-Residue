using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdselectionbool : MonoBehaviour
{
    public GameObject charselection;
    private void OnDisable()
    {
        charselection.GetComponent<Opensupportchar>().thirdcharselectionactive = false;
    }
}
