using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chestreward : MonoBehaviour, Rewardinterface, Interactioninterface
{
    [SerializeField] private Areacontroller areacontroller;
    [SerializeField] private GameObject closedchest;
    [SerializeField] private GameObject openchest;
    public int areachestnumber;

    private int rewardcount;
    [SerializeField] private int rewardcountneeded;
    [SerializeField] Itemsinchest[] rewards;

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
        if (areacontroller.enemychestisopen[areachestnumber] == true)
        {
            closedchest.SetActive(false);
            openchest.SetActive(true);
            GetComponent<Detectinteractionobject>().enabled = false;
            enabled = false;
        }
        else if (areacontroller.enemychestcanopen[areachestnumber])
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
            areacontroller.enemychestcanopen[areachestnumber] = true;
            areacontroller.autosave();
        }
    }

    public bool Interact(Closestinteraction interactor)
    {
        if(areacontroller.enemychestcanopen[areachestnumber] == true)
        {
            if (areacontroller.enemychestisopen[areachestnumber] == false)
            {
                foreach (Itemsinchest iteminchest in rewards)
                {
                    if(iteminchest.item.type != Itemtype.Crafting)
                    {
                        iteminchest.inventory.Addequipment(iteminchest.item, iteminchest.item.seconditem, iteminchest.amount);
                    }
                    else
                    {
                        iteminchest.inventory.Additem(iteminchest.item, iteminchest.amount);
                    }
                }
                closedchest.SetActive(false);
                openchest.SetActive(true);
                areacontroller.enemychestisopen[areachestnumber] = true;
                areacontroller.autosave();
                GetComponent<Detectinteractionobject>().enabled = false;
                enabled = false;
            }
        }
        return true;
    }
}

[System.Serializable]
public class Itemsinchest
{
    public Inventorycontroller inventory;
    public Itemcontroller item;
    public int amount;
}
