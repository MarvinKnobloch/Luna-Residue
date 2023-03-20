using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nightshadespezial : MonoBehaviour
{
    [SerializeField] private Nightshadecontroller nightshadecontroller;
    private void nightshadespezial()
    {
        nightshadecontroller.enemy = gameObject;
        nightshadecontroller.gameObject.SetActive(true);
    }
}
