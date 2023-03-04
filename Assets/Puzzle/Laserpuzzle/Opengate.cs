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

    [SerializeField] private Areacontroller areacontroller;
    private int puzzlenumber;

    public Vector3 gateisclosedposi;
    public Vector3 gateisopen;
    private float gateopeningtime = 7f;
    private float gateopentimer;

    private SpielerSteu controlls;

    private void Start()
    {
        puzzlenumber = GetComponent<Areanumber>().areanumber;
        controlls = Keybindinputmanager.inputActions;
        if (areacontroller.puzzlecomplete[puzzlenumber] == true)
        {
            transform.position = gateisopen;
        }
    }
    private void Update()
    {
        if (controlls.Player.Dash.WasPerformedThisFrame())
        {
            if (areacontroller.puzzlecomplete[puzzlenumber] == false)
            {
                StartCoroutine("openthegate");
                areacontroller.puzzlecomplete[puzzlenumber] = true;
            }
        }
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void opengate()
    {
        if (Greenend.GetComponent<Laserpuzzlefinish>().laserdoeshit == true && Redend.GetComponent<Laserpuzzlefinish>().laserdoeshit == true && Blueend.GetComponent<Laserpuzzlefinish>().laserdoeshit == true)
        {
            if(areacontroller.puzzlecomplete[puzzlenumber] == false)
            {
                StartCoroutine("openthegate");
                areacontroller.puzzlecomplete[puzzlenumber] = true;
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
                Greenend.GetComponent<Laserpuzzlefinish>().laserdoeshit = false;
                Redend.GetComponent<Laserpuzzlefinish>().laserdoeshit = false;
                Blueend.GetComponent<Laserpuzzlefinish>().laserdoeshit = false;
                StopCoroutine("openthegate");
            }
            yield return null;
        }
    }
}
