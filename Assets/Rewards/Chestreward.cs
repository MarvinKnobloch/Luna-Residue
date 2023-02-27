using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chestreward : MonoBehaviour, Rewardinterface, Interactioninterface
{
    [SerializeField] private Firstarea firstarea;
    [SerializeField] private GameObject closedchest;
    [SerializeField] private GameObject openchest;
    public int areachestnumber;

    private int rewardcount;
    [SerializeField] private int rewardcountneeded;

    private string chestlocked = "Locked";
    private string chestopen = "Open Chest";
    private string cheststatetext;
    public string Interactiontext => cheststatetext;

    private void OnEnable()
    {
        StartCoroutine("currentcheststate");                          //ein frame delay damit die settings für die area vorher geladen werden
    }
    IEnumerator currentcheststate()
    {
        yield return null;
        if (firstarea.startingzonechestisopen[areachestnumber] == true)
        {
            closedchest.SetActive(false);
            openchest.SetActive(true);
            GetComponent<Detectinteractionobject>().enabled = false;
            enabled = false;
        }
        else if (firstarea.startingzonechestcanopen[areachestnumber])
        {
            closedchest.SetActive(true);
            cheststatetext = chestopen;
            openchest.SetActive(false);
        }
        else
        {
            rewardcount = 0;
            closedchest.SetActive(true);
            cheststatetext = chestlocked;
            openchest.SetActive(false);
        }
    }
    public void checkforreward()
    {
        rewardcount++;
        if (rewardcount >= rewardcountneeded)
        {
            cheststatetext = chestopen;
            firstarea.startingzonechestcanopen[areachestnumber] = true;
            firstarea.autosave();
        }
    }

    public bool Interact(Closestinteraction interactor)
    {
        if(firstarea.startingzonechestcanopen[areachestnumber] == true)
        {
            if (firstarea.startingzonechestisopen[areachestnumber] == false)
            {
                Debug.Log("getitems");
                closedchest.SetActive(false);
                openchest.SetActive(true);
                firstarea.startingzonechestisopen[areachestnumber] = true;
                firstarea.autosave();
                GetComponent<Detectinteractionobject>().enabled = false;
                enabled = false;
            }
        }
        return true;
    }
}
