using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirespezial : MonoBehaviour
{
    [SerializeField] private GameObject vampirecontroller;
    public void castspezialattack()
    {
        vampirecontroller.SetActive(true);
    }
}
