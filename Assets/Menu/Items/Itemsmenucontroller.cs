using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Itemsmenucontroller : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private GameObject overview;
    [SerializeField] private GameObject itemsmenu;
    [SerializeField] private GameObject itemsbackground;
    [SerializeField] private Inventorycontroller matsinventory;
    [SerializeField] private GameObject itemprefab;

    [SerializeField] private Menusoundcontroller menusoundcontroller;
    private List<Itemcontroller> items = new List<Itemcontroller>();

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        for (int i = 0; i < matsinventory.Container.Items.Length; i++)
        {
            if (matsinventory.Container.Items[i].itemid != 0 && items.Contains(matsinventory.Container.Items[i].item) == false)
            {
                items.Add(matsinventory.Container.Items[i].item);
                GameObject obj = Instantiate(itemprefab, itemsbackground.transform);
                obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = matsinventory.Container.Items[i].itemname;
                obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = matsinventory.Container.Items[i].amount.ToString();
            }
        }
    }
    void Update()
    {
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            overview.SetActive(true);
            itemsmenu.SetActive(false);
            menusoundcontroller.playmenubuttonsound();
        }
    }
}
