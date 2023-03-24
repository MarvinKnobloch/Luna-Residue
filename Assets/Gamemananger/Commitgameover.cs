using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commitgameover : MonoBehaviour
{
    [SerializeField] private GameObject gameover;

    [SerializeField] private Image commitbar;

    private SpielerSteu controlls;
    private float committime = 2;
    private float committimer;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        committimer = 0;
        commitbar.fillAmount = 0;
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Space.WasPressedThisFrame())
        {
            StartCoroutine("commit");
        }
        if (controlls.Menusteuerung.Space.WasReleasedThisFrame())
        {
            commitbar.fillAmount = 0;
            StopCoroutine("commit");
        }
    }
    IEnumerator commit()
    {
        committimer = 0;
        commitbar.fillAmount = 0;
        while (committimer < committime)
        {
            committimer += Time.deltaTime;
            commitbar.fillAmount = committimer / committime;
            yield return null;
        }
        gameover.SetActive(true);
        StopCoroutine("commit");
        gameObject.SetActive(false);
    }
}
