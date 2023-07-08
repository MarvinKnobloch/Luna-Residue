using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using TMPro;

public class Attacktutorial : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    private Vector3 gatestartposi;
    private Vector3 gateendposi;
    private float movetime = 4f;
    private float movetimer;

    private bool tutorialfinished;
    [SerializeField] private TextMeshProUGUI requierementtext;
    private void Start()
    {
        gatestartposi = gate.transform.position;
        gateendposi = gatestartposi + new Vector3(0, -10, 0);
    }
    private void Update()
    {
        if (tutorialfinished == false) checkgate();
    }
    private void checkgate()
    {
        if (LoadCharmanager.Overallmainchar.GetComponent<Movescript>().attackcombochain > 0)
        {
            tutorialfinished = true;
            requierementtext.text = string.Empty;
            StartCoroutine(opengate());
        }
    }
    IEnumerator opengate()
    {
        while (true)
        {
            movetimer += Time.deltaTime;
            float gateopenpercantage = movetimer / movetime;
            gate.transform.position = Vector3.Lerp(gatestartposi, gateendposi, gateopenpercantage);

            if (movetimer >= movetime)
            {
                movetimer = 0;
                StopCoroutine("opengate");
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}