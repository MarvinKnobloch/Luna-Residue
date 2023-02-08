using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opengate : MonoBehaviour
{
    [SerializeField] GameObject Greenend;
    [SerializeField] GameObject Redend;
    [SerializeField] GameObject Blueend;
    [SerializeField] GameObject disablegreenlaser;
    [SerializeField] GameObject disableredlaser;
    [SerializeField] GameObject disablebluelaser;
    [SerializeField] private Firstarea firstarea;

    public Vector3 gateisclosedposi;
    public Vector3 gateisopen;
    private float gateopeningtime = 7f;
    private float gateopentimer;

    private SpielerSteu controlls;

    private void Start()
    {
        controlls = Keybindinputmanager.inputActions;
        if (firstarea.laserpuzzlecomplete == true)
        {
            transform.position = gateisopen;
        }
    }
    private void Update()
    {
        if (controlls.Player.Dash.WasPerformedThisFrame())
        {
            if (firstarea.laserpuzzlecomplete == false)
            {
                StartCoroutine("openthegate");
                firstarea.laserpuzzlecomplete = true;
            }
        }
    }
    private void OnEnable()
    {
        Lasersuccsess.checklaserhitlist += opengate;
    }
    private void OnDisable()
    {
        Lasersuccsess.checklaserhitlist -= opengate;
    }
    private void opengate()
    {
        if (Greenend.GetComponent<Lasersuccsess>().laserdoeshit == true && Redend.GetComponent<Lasersuccsess>().laserdoeshit == true && Blueend.GetComponent<Lasersuccsess>().laserdoeshit == true)
        {
            if(firstarea.laserpuzzlecomplete == false)
            {
                StartCoroutine("openthegate");
                firstarea.laserpuzzlecomplete = true;
            }
        }                
    }
    IEnumerator openthegate()
    {
        while (true)
        {
            gateopentimer += Time.deltaTime;
            float gateopenpercantage = gateopentimer / gateopeningtime;
            transform.position = Vector3.Lerp(gateisclosedposi, gateisopen, gateopenpercantage);

            if (gateopentimer >= gateopeningtime)
            {
                gateopentimer = 0;
                disablegreenlaser.SetActive(false);
                disableredlaser.SetActive(false);
                disablebluelaser.SetActive(false);
                Greenend.GetComponent<Lasersuccsess>().laserdoeshit = false;
                Redend.GetComponent<Lasersuccsess>().laserdoeshit = false;
                Blueend.GetComponent<Lasersuccsess>().laserdoeshit = false;
                StopCoroutine("openthegate");
            }
            yield return null;
        }
    }
}
