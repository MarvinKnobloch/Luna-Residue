using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Planttimer : MonoBehaviour
{
    private Plantcontroller plantcontroller;
    private TextMeshProUGUI timertext;

    private void Awake()
    {
        timertext = GetComponent<TextMeshProUGUI>();
        plantcontroller = GetComponentInParent<Plantcontroller>();
    }
    private void LateUpdate()
    {
        timertext.text = string.Format("{0:0}", plantcontroller.remainingtime);
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
