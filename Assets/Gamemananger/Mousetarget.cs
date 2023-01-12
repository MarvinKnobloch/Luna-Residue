using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousetarget : MonoBehaviour
{
    public Transform Kamerarichtung;
    public LayerMask aimlayer;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Kamerarichtung.position, Kamerarichtung.forward, out hit, 500, aimlayer, QueryTriggerInteraction.Ignore))
        {
            transform.position = hit.point;
        }
    }
}
