using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveobjectafterfinish : MonoBehaviour, Rewardinterface
{
    [SerializeField] private Areacontroller areacontroller;
    private int puzzlenumber;

    private int rewardcount;
    [SerializeField] private int rewardcountneeded;

    public Vector3 objectstartposi;
    public Vector3 objectendposi;
    private float movetime = 7f;
    private float movetimer;

    private SpielerSteu controlls;

    private void Start()
    {
        puzzlenumber = GetComponent<Areanumber>().areanumber;
        controlls = Keybindinputmanager.inputActions;
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
        StopAllCoroutines();
        StartCoroutine("currentgatestate");
    }
    IEnumerator currentgatestate()
    {
        yield return null;
        if (areacontroller.puzzlecomplete[puzzlenumber] == true) transform.position = objectendposi;
        else transform.position = objectstartposi;
    }

    public void addrewardcount()
    {
        rewardcount++;
        if (rewardcount >= rewardcountneeded)
        {
            if (areacontroller.puzzlecomplete[puzzlenumber] == false)
            {
                StartCoroutine("openthegate");
                areacontroller.puzzlecomplete[puzzlenumber] = true;
                areacontroller.autosave();
            }
        }
    }
    IEnumerator openthegate()
    {
        while (true)
        {
            movetimer += Time.deltaTime;
            float gateopenpercantage = movetimer / movetime;
            transform.position = Vector3.Lerp(objectstartposi, objectendposi, gateopenpercantage);

            if (movetimer >= movetime)
            {
                movetimer = 0;
                StopCoroutine("openthegate");
            }
            yield return null;
        }
    }

    public void removerewardcount()
    {
        rewardcount--;
    }

    public void resetrewardcount()
    {
        rewardcount = 0;
    }
}
