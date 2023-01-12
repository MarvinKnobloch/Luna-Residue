using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doozyspezial : MonoBehaviour
{
    [SerializeField] private GameObject doozycontroller;

    private void doozyspezial()
    {
        doozycontroller.SetActive(true);
    }
}
