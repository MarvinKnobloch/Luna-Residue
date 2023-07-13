using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disableonawake : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
