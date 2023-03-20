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

    private bool changetime;

    private void Awake()
    {
        maincam.GetComponent<CinemachineFreeLook>();
    }
    private void OnEnable()
    {
        maincam.m_YAxis.m_MaxSpeed = 0;
        maincam.m_XAxis.m_MaxSpeed = 0;
        changetime = false;
        timer = answertime;
        StartCoroutine("mathtimer");
        Mouseactivate.enablemouse();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (changetime == false)
        {
            changetime = true;
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = Statics.normaltimedelta * 0.3f;
        }
    }
    public void commitnumber()
    {
        if (solution.text != "")
        {
            answer = int.Parse(solution.text);
            if (answer == mathmancontroller.GetComponent<Mathmancontroller>().rightanswer)
            {
                solutionUI.color = Color.green;
                LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
                Statics.otheraction = false;
                Statics.dash = false;
                Time.timeScale = Statics.normalgamespeed;
                Time.fixedDeltaTime = Statics.normaltimedelta;
                maincam.m_YAxis.m_MaxSpeed = 0.008f;
                maincam.m_XAxis.m_MaxSpeed = 0.6f;
            }
            else
            {
                if (Statics.infight == true)
                {
                    LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(Mathf.Round(basedmg * (timer / 2 + 1) + Globalplayercalculations.calculateenemyspezialdmg()));
                }
                solutionUI.color = Color.red;
                LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
                Statics.otheraction = false;
                Statics.dash = false;
                Time.timeScale = Statics.normalgamespeed;
                Time.fixedDeltaTime = Statics.normaltimedelta;
                maincam.m_YAxis.m_MaxSpeed = 0.008f;
                maincam.m_XAxis.m_MaxSpeed = 0.6f;
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
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
            Statics.otheraction = false;
            Statics.dash = false;
            Time.timeScale = Statics.normalgamespeed;
            Time.fixedDeltaTime = Statics.normaltimedelta;
            maincam.m_YAxis.m_MaxSpeed = 0.008f;
            maincam.m_XAxis.m_MaxSpeed = 0.6f;
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
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
            Statics.otheraction = false;
            Statics.dash = false;
            Time.timeScale = Statics.normalgamespeed;
            Time.fixedDeltaTime = Statics.normaltimedelta;
            maincam.m_YAxis.m_MaxSpeed = 0.008f;
            maincam.m_XAxis.m_MaxSpeed = 0.6f;
            mathmancontroller.SetActive(false);
        }
    }
}
