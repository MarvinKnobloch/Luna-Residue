using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Video;
using TMPro;

public class Enemysizetutorial : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    private Vector3 gatestartposi;
    private Vector3 gateendposi;
    private float movetime = 6f;
    private float movetimer;

    private bool tutorialfinished;

    [SerializeField] private EnemyHP enemyHP;
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
        if (enemyHP.enemyincreasebasicdmg == true)
        {
            tutorialfinished = true;
            requierementtext.text = string.Empty;
            StartCoroutine(killenemy());
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
    IEnumerator killenemy()
    {
        yield return new WaitForSeconds(2);
        enemyHP.takesupportdmg(15000);
    }
}
