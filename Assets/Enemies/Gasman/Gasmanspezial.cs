using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gasmanspezial : MonoBehaviour
{
    [SerializeField] private GameObject gasmancontroller;

    private void gasmanspezial()
    {
        gasmancontroller.SetActive(true);
    }
}
