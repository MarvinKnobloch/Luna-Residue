using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class Stonecontroller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int stoneclassroll;
    private TextMeshProUGUI stonetext;
    public int element;
    public bool isactiv;
    public int stonenumber;

    private Elemenucontroller elemenucontroller;
    private GameObject awakemessageobj;
    private Awakecontroller awakecontroller;
    [NonSerialized] public TextMeshProUGUI elementalcorecosttext;
    [SerializeField] private TextMeshProUGUI bonustext;
    private Inventorycontroller matsinventory;
    [SerializeField] private Craftingobject elementalcore;
    private int elementalcoreamount;


    private void Awake()
    {
        elemenucontroller = GetComponentInParent<Stonegridcontroller>().elemenucontroller;
        awakemessageobj = GetComponentInParent<Stonegridcontroller>().awakemessage;
        awakecontroller = awakemessageobj.GetComponent<Awakecontroller>();
        matsinventory = GetComponentInParent<Stonegridcontroller>().matsinventory;
        elementalcorecosttext = GetComponentInParent<Stonegridcontroller>().elementalcorecosttext;
        stonetext = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void setelemenustats()
    {
        if (isactiv == true)
        {
            GetComponentInParent<Stonegridcontroller>().resetslotifnewelement(element);
            elemenucontroller.stoneclassroll = stoneclassroll;                     // roll(Damage, Tank , Healer)
            elemenucontroller.stonetext = stonetext.text;
            elemenucontroller.stonecolor = GetComponent<Image>().color;
            elemenucontroller.elementonstoneselect = element;                      //element des Ausgewählen Steines
            elemenucontroller.choosestone();
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (isactiv == false)
        {
            awakemessageobj.GetComponent<Awakecontroller>().stonecontroller = this;
            elementalcorecosttext.text = "(" + elementalcoreamount + "/1)" + elementalcore.itemname;
            awakecontroller.awake[awakecontroller.awakelvl].awakemats[0].awakemat = elementalcore;

            awakecontroller.neededawakecore = false;
            if (awakecontroller.awakelvl == awakecontroller.maxawakening)
            {
                elementalcorecosttext.text = "";
            }
            else
            {
                if (elementalcore.inventoryslot != 0)
                {
                    elementalcoreamount = matsinventory.Container.Items[elementalcore.inventoryslot - 1].amount;            // kann ich nicht im enable machen, weil sich der amount nach dem awake ändert
                    if (awakecontroller.awake[awakecontroller.awakelvl].awakemats[0].costs <= elementalcoreamount)
                    {
                        elementalcorecosttext.text = "(" + "<color=green>" + elementalcoreamount + "</color>" + "/" + awakecontroller.awake[awakecontroller.awakelvl].awakemats[0].costs + ")" + elementalcore.itemname;
                        awakecontroller.neededawakecore = true;
                    }
                    else
                    {
                        elementalcorecosttext.text = "(" + "<color=red>" + elementalcoreamount + "</color>" + "/" + awakecontroller.awake[awakecontroller.awakelvl].awakemats[0].costs + ")" + elementalcore.itemname;
                    }
                }
                else
                {
                    elementalcorecosttext.text = "(" + "<color=red>" + "0" + "</color>" + "/" + awakecontroller.awake[awakecontroller.awakelvl].awakemats[0].costs + ")" + elementalcore.itemname;
                }
            }
            if(stoneclassroll == 0)
            {
                float newvalue = Statics.groupstonedmgbonus + 1;
                bonustext.text = "Damage increase (" + "<color=green>" + newvalue + "</color>" + "%)";
            }
            if (stoneclassroll == 1)
            {
                float newvalue = Statics.groupstonedefensebonus + 2;
                bonustext.text = "Damage reduction (" + "<color=green>" + newvalue + "</color>" + "%)";
            }
            if (stoneclassroll == 2)
            {
                float newvalue = Statics.groupstonehealbonus + 3;
                bonustext.text = "Heal increase (" + "<color=green>" + newvalue + "</color>" + "%)";
            }
            awakecontroller.checkforawake();
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (isactiv == false)
        {
            awakemessageobj.GetComponent<Awakecontroller>().stonecontroller = null;
            awakecontroller.awakecancel();
            elementalcorecosttext.text = "Elemental Core";
            awakecontroller.awake[awakecontroller.awakelvl].awakemats[0].awakemat = null;
            if (stoneclassroll == 0)
            {
                bonustext.text = "Damage increase (" + Statics.groupstonedmgbonus + "%)";
            }
            if (stoneclassroll == 1)
            {

                bonustext.text = "Damage reduction (" + Statics.groupstonedefensebonus + "%)";
            }
            if (stoneclassroll == 2)
            {
                bonustext.text = "Heal increase (" + Statics.groupstonehealbonus + "%)";
            }
        }
    }
    public void stonecontrollerupdate()
    {
        matsinventory.Container.Items[elementalcore.inventoryslot - 1].amount -= awakecontroller.awake[awakecontroller.awakelvl].awakemats[0].costs;
        elementalcorecosttext.text = "Elemental Core";
        if (stoneclassroll == 0)
        {
            Statics.groupstonedmgbonus += 1;
            bonustext.text = "Damage increase (" + Statics.groupstonedmgbonus + "%)";
        }
        if (stoneclassroll == 1)
        {
            Statics.groupstonedefensebonus += 2;
            bonustext.text = "Damage reduction (" + Statics.groupstonedefensebonus + "%)";
        }
        if (stoneclassroll == 2)
        {
            Statics.groupstonehealbonus += 3;
            bonustext.text = "Heal increase (" + Statics.groupstonehealbonus + "%)";
        }
    }
}
