using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzlerewardisportal : MonoBehaviour, Rewardinterface
{
    [SerializeField] private Areacontroller areacontroller;
    private int puzzlenumber;

    [SerializeField] GameObject portal;

    private void Start()
    {
        puzzlenumber = GetComponent<Areanumber>().areanumber;
    }
    private void OnEnable()
    {
        StartCoroutine("waitforsettingareanumber");
    }
    IEnumerator waitforsettingareanumber()
    {
        yield return null;
        if (areacontroller.puzzlecomplete[puzzlenumber] == true)
        {
            portal.SetActive(true);
        }
        else
        {
            portal.SetActive(false);
        }
    }
    public void addrewardcount()
    {
        areacontroller.puzzlecomplete[puzzlenumber] = true;
        areacontroller.autosave();
        portal.SetActive(true);
    }

    public void removerewardcount()
    {
        return;
    }

    public void resetrewardcount()
    {
        return;
    }
}
