using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disablepointerlayer : MonoBehaviour
{
    private void Update()   // kann nicht mit Zeit arbeit weil Time.delta auf 0 ist
    {
        this.gameObject.SetActive(false);
    }
}
