using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;
using TMPro;

public class Mathcommit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI solution;
    [SerializeField] private Image solutionUI;
    private Mathmancontroller mathmancontroller;
    [SerializeField] private CinemachineFreeLook maincam;
    private int answer;
    [SerializeField] private float answertime;
    private float timer;
    private float timerdmgmultiplier = -0.33f;                 //-0.4f sind 40% dmgreduction nach ablauf des timers (-0.3f wären 30%)

    [NonSerialized] public float basedmg;

    private void Awake()
    {
        maincam.GetComponent<CinemachineFreeLook>();
        mathmancontroller = GetComponentInParent<Mathmancontroller>();
    }
    private void OnEnable()
    {
        maincam.m_YAxis.m_MaxSpeed = 0;
        maincam.m_XAxis.m_MaxSpeed = 0;

        Statics.enemyspezialtimescale = true;
        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = Statics.normaltimedelta * 0.3f;
        timer = 0;
        StartCoroutine("mathtimer");
        Mouseactivate.enablemouse();
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    public void commitnumber()
    {
        if (solution.text != "")
        {
            answer = int.Parse(solution.text);
            if (answer == mathmancontroller.rightanswer)
            {
                solutionUI.color = Color.green;
                resetvalues();
            }
            else
            {
                if (Statics.infight == true)
                {
                    float dmg = Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 1);
                    float finaldmg = (timerdmgmultiplier * (timer / answertime) + 1) * dmg;
                    LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().takedamagecheckiframes(finaldmg, true);
                }
                solution.text = answer + " (<color=green>" + mathmancontroller.rightanswer + "</color>)";
                solutionUI.color = Color.red;
                resetvalues();
            }
            Invoke("turnoffUI", 0.5f);
            StopAllCoroutines();
        }
        else
        {
            if (Statics.infight == true)
            {
                float dmg = Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 1);
                float finaldmg = (timerdmgmultiplier * (timer / answertime) + 1) * dmg;
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().takedamagecheckiframes(finaldmg, true);
            }
            solution.text = " (<color=green>" + mathmancontroller.rightanswer + "</color>)";
            solutionUI.color = Color.red;
            resetvalues();
            Invoke("turnoffUI", 0.5f);
            StopAllCoroutines();
        }
    }
    private void turnoffUI()
    {
        solutionUI.color = Color.white;
        solution.text = "";
        mathmancontroller.gameObject.SetActive(false);
        Mouseactivate.disablemouse();
    }
    IEnumerator mathtimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(answertime);
            if(Statics.infight == true)
            {
                float dmg = Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 1);
                float finaldmg = (timerdmgmultiplier * (timer / answertime) + 1) * dmg;
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().takedamagecheckiframes(finaldmg, true);
            }
            solution.text = "";
            resetvalues();
            mathmancontroller.gameObject.SetActive(false);
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
