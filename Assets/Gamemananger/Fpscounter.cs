using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fpscounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpscountertext;

    private float time;
    private float timerupdate = 0.4f;
    private int fpscounter;

    private void OnEnable()
    {
        fpscountertext.text = "-- FPS";
        fpscounter = 0;
        time = 0;
    }
    private void Update()
    {
        time += Time.deltaTime;
        fpscounter++;
        if(time > timerupdate)
        {
            int fps = Mathf.RoundToInt(fpscounter / time);
            fpscountertext.text = fps.ToString() + " FPS";
            time -= timerupdate;
            fpscounter = 0;
        }
    }
}
