using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;

public class Mathcommit : MonoBehaviour
{
    [SerializeField] private Text solution;
    [SerializeField] private Image solutionUI;
    [SerializeField] private GameObject mathmancontroller;
    [SerializeField] private CinemachineFreeLook maincam;
    private int answer;
    private float timer;

    [NonSerialized] public float basedmg;
    [NonSerialized] public float answertime;

    private void Awake()
    {
        maincam.GetComponent<CinemachineFreeLook>();
    }
    private void OnEnable()
    {
        maincam.m_YAxis.m_MaxSpeed = 0;
        maincam.m_XAxis.m_MaxSpeed = 0;

        Statics.enemyspezialtimescale = true;
        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = Statics.normaltimedelta * 0.3f;

        timer = answertime;
        StartCoroutine("mathtimer");
        Mouseactivate.enablemouse();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
    }
    public void commitnumber()
    {
        if (solution.text != "")
        {
            answer = int.Parse(solution.text);
            if (answer == mathmancontroller.GetComponent<Mathmancontroller>().rightanswer)
            {
                solutionUI.color = Color.green;
                resetvalues();
            }
            else
            {
                if (Statics.infight == true)
                {
                    LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(Mathf.Round(basedmg * (timer / 2 + 1) + Globalplayercalculations.calculateenemyspezialdmg()));
                }
                solutionUI.color = Color.red;
                resetvalues();
            }
            Invoke("turnoffUI", 0.3f);
            StopAllCoroutines();
        }
        else
        {
            if (Statics.infight == true)
            {
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(Mathf.Round(basedmg * (timer / 2 + 1) + Globalplayercalculations.calculateenemyspezialdmg()));
            }
            solutionUI.color = Color.red;
            resetvalues();
            Invoke("turnoffUI", 0.3f);
            StopAllCoroutines();
        }
    }
    private void turnoffUI()
    {
        solutionUI.color = Color.white;
        solution.text = "";
        mathmancontroller.SetActive(false);
        Mouseactivate.disablemouse();
    }
    IEnumerator mathtimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            if(Statics.infight == true)
            {
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(Mathf.Round(basedmg * (timer / 2 + 1) + Globalplayercalculations.calculateenemyspezialdmg()));
            }
            solution.text = "";
            resetvalues();
            mathmancontroller.SetActive(false);
        }
    }
    private void resetvalues()
    {
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
        Statics.enemyspezialtimescale = false;
        Statics.otheraction = false;
        Statics.dash = false;
        Time.timeScale = Statics.normalgamespeed;
        Time.fixedDeltaTime = Statics.normaltimedelta;
        maincam.m_YAxis.m_MaxSpeed = Statics.presetcamymaxspeed * PlayerPrefs.GetFloat("mousesensitivity") / 50;
        maincam.m_XAxis.m_MaxSpeed = Statics.presetcamxmaxspeed * PlayerPrefs.GetFloat("mousesensitivity") / 50;
    }
}
