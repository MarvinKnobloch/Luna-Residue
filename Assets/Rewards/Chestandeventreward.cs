using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chestandeventreward : MonoBehaviour, Rewardinterface, Interactioninterface, Chestinterface
{
    [SerializeField] private Areacontroller areacontroller;
    [SerializeField] private GameObject closedchest;
    [SerializeField] private GameObject openchest;
    public int areachestnumber;

    public int rewardcount;
    [SerializeField] private int rewardcountneeded;
    [SerializeField] private GameObject[] rewardenemies;
    [SerializeField] Itemsinchest[] rewards;
    [SerializeField] GameObject chestevent;

    private string chestlocked = "Locked";
    private string chestopen = "Open Chest";
    private string cheststatetext;
    public string Interactiontext => cheststatetext;

    private void Start()
    {
        if(rewardenemies.Length != 0)
        {
            for (int i = 0; i < rewardenemies.Length; i++)
            {
                if (rewardenemies[i].TryGetComponent(out EnemyHP enemyhp)) enemyhp.rewardobject = this.gameObject;
            }
        }
    }
    private void OnEnable()
    {
        Infightcontroller.resetrewards += afterenemyreset;
        StartCoroutine("currentcheststate");                          //ein frame delay damit die settings für die area vorher geladen werden
    }
    private void OnDisable()
    {
        Infightcontroller.resetrewards -= afterenemyreset;
    }
    IEnumerator currentcheststate()
    {
        yield return null;
        if (areacontroller.enemychestisopen[areachestnumber] == true)
        {
            closedchest.SetActive(false);
            openchest.SetActive(true);
            GetComponent<Detectinteractionobject>().enabled = false;
            enabled = false;
        }
        else if (areacontroller.enemychestcanopen[areachestnumber] == true)
        {
            closedchest.SetActive(true);
            cheststatetext = chestopen;
            openchest.SetActive(false);
        }
        else if (areacontroller.enemychestcanopen[areachestnumber] == false && rewardcountneeded == 0)
        {
            cheststatetext = chestopen;
            areacontroller.enemychestcanopen[areachestnumber] = true;
            LoadCharmanager.autosave();
        }
        else
        {
            rewardcount = 0;
            closedchest.SetActive(true);
            cheststatetext = chestlocked;
            openchest.SetActive(false);
        }
    }
    public void setareachestnumber(int number)
    {
        areachestnumber = number;
    }
    public void addrewardcount()
    {
        rewardcount++;
        if (rewardcount >= rewardcountneeded)
        {
            if (areacontroller.enemychestcanopen[areachestnumber] == false)
            {
                cheststatetext = chestopen;
                areacontroller.enemychestcanopen[areachestnumber] = true;
                LoadCharmanager.autosave();
            }
        }
    }
    public void removerewardcount()
    {
        rewardcount--;
    }

    public bool Interact(Closestinteraction interactor)
    {
        if (areacontroller.enemychestcanopen[areachestnumber] == true)
        {
            if (areacontroller.enemychestisopen[areachestnumber] == false)
            {
                foreach (Itemsinchest iteminchest in rewards)
                {
                    if (iteminchest.item.type != Itemtype.Crafting)
                    {
                        iteminchest.inventory.Addequipment(iteminchest.item, iteminchest.item.seconditem, iteminchest.amount);
                    }
                    else
                    {
                        iteminchest.inventory.Additem(iteminchest.item, iteminchest.amount);
                    }
                }
                chestevent.GetComponent<Eventinterface>().eventstart();
                if (gameObject.TryGetComponent(out Endactivquest questactivend)) questactivend.endquest();
                if (gameObject.TryGetComponent(out Endinactivquest questinactivend)) questinactivend.endquest();
                if (gameObject.TryGetComponent(out Startquest queststart)) queststart.startquest();
                closedchest.SetActive(false);
                openchest.SetActive(true);
                areacontroller.enemychestisopen[areachestnumber] = true;
                LoadCharmanager.autosave();
                GetComponent<Detectinteractionobject>().enabled = false;
                enabled = false;
            }
        }
        return true;
    }

    public void resetrewardcount()
    {
        rewardcount = 0;
    }
    private void afterenemyreset()
    {
        if (rewardenemies.Length != 0)
        {
            if (rewardcount != rewardcountneeded)
            {
                rewardcount = 0;
            }
            for (int i = 0; i < rewardenemies.Length; i++)
            {
                rewardenemies[i].SetActive(true);
            }
        }
    }
}

