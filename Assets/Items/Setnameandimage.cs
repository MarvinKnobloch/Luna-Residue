using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setnameandimage : MonoBehaviour
{
    public void setequipnameandimage(int equipslot, int charnumber , string itemname, GameObject itemimageobj)             //hier werden name und image gesetzt damit ich nur ein script für chooseitem brauch, jeder equipmentslot hat eine eigene nummer
                                                                                                                        //muss im InventoryUI geändert werden falls die Reihenfolge sich ändert
    {
        if(equipslot == 3)                //head = number3
        {
            if (Statics.currentheadimage[charnumber] != null)
            {
                Statics.currentheadimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentheadname[charnumber] = itemname;
            Statics.currentheadimage[charnumber] = itemimageobj;
            Statics.activeheadslot = itemimageobj;
        }
        else if (equipslot == 4)         //chest = number4
        {
            if (Statics.currentchestimage[charnumber] != null)
            {
                Statics.currentchestimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentchestname[charnumber] = itemname;
            Statics.currentchestimage[charnumber] = itemimageobj;
            Statics.activechestslot = itemimageobj;
        }
        else if (equipslot == 5)        //gloves = number5
        {
            if (Statics.currentglovesimage[charnumber] != null)
            {
                Statics.currentglovesimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentglovesname[charnumber] = itemname;
            Statics.currentglovesimage[charnumber] = itemimageobj;
            Statics.activeglovesslot = itemimageobj;
        }
        else if (equipslot == 6)        //belt = number6
        {
            if (Statics.currentlegimage[charnumber] != null)
            {
                Statics.currentlegimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentlegname[charnumber] = itemname;
            Statics.currentlegimage[charnumber] = itemimageobj;
            Statics.activebeltslot = itemimageobj;
        }
        else if (equipslot == 7)        //shoes = number7
        {
            if (Statics.currentshoesimage[charnumber] != null)
            {
                Statics.currentshoesimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentshoesname[charnumber] = itemname;
            Statics.currentshoesimage[charnumber] = itemimageobj;
            Statics.activeshoesslot = itemimageobj;
        }
        else if (equipslot == 8)        //neckless = number8
        {
            if (Statics.currentnecklessimage[charnumber] != null)
            {
                Statics.currentnecklessimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentnecklessname[charnumber] = itemname;
            Statics.currentnecklessimage[charnumber] = itemimageobj;
            Statics.activenecklessslot = itemimageobj;
        }
        else if (equipslot == 9)        //ring = number9
        {
            if (Statics.currentringimage[charnumber] != null)
            {
                Statics.currentringimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentringname[charnumber] = itemname;
            Statics.currentringimage[charnumber] = itemimageobj;
            Statics.activeringslot = itemimageobj;
        }
    }
}
