using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour, Interactioninterface
{
    [SerializeField] private string text;

    [SerializeField] private Inventorycontroller inventory;
    [SerializeField] private Itemcontroller item;
    [SerializeField] private int itemamount;
    [SerializeField] private float collecttime;

    [SerializeField] private Areacontroller areacontroller;
    public int gahteringnumber;

    public string Interactiontext => text;

    private void Start()
    {
        gahteringnumber = GetComponent<Areanumber>().areanumber;
    }
    private void OnEnable()
    {
        CancelInvoke();
        StartCoroutine("currentgatherstate");
    }
    IEnumerator currentgatherstate()
    {
        yield return null;
        if (areacontroller.gotgatheritem[gahteringnumber] == true)
        {
            gameObject.SetActive(false);
        }
    }
    public bool Interact(Closestinteraction interactor)
    {
        CancelInvoke();
        LoadCharmanager.interaction = true;
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().gahteritem(gameObject);
        Invoke("getitem", collecttime);
        return true;
    }
    private void getitem()
    {
        LoadCharmanager.interaction = false;
        if (LoadCharmanager.Overallmainchar.GetComponent<Movescript>().state == Movescript.State.Gatheritem)
        {
            areacontroller.gotgatheritem[gahteringnumber] = true;
            inventory.Additem(item, itemamount);
            gameObject.SetActive(false);
        }
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
    }
}
