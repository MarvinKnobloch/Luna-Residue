using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forthcharselect : MonoBehaviour
{
    [SerializeField] internal Thirdcharselect thirdchar;

    public Text forthchartext;
    public Text[] buttontext;
    public GameObject charselection;

    public int selectetdCharacter;

    [SerializeField] private GameObject menuoverview;
    private Menucontroller menucontroller;
    private void Awake()
    {
        menucontroller = menuoverview.GetComponent<Menucontroller>();
    }
    private void OnEnable()
    {
        if (Statics.currentforthchar != -1)
        {
            forthchartext.text = buttontext[Statics.currentforthchar].text;
        }
        else
        {
            forthchartext.text = "empty";
        }
        charselection.SetActive(false);
        selectetdCharacter = Statics.currentforthchar;
    }
    public void changeforthcharacter(int newCharacter)
    {
        if (newCharacter == -1)
        {
            Statics.currentforthchar = -1;
            forthchartext.text = "empty";
            charselection.SetActive(false);
            menuoverview.GetComponent<Menucontroller>().somethinginmenuisopen = false;
        }
        else
        {
            if (thirdchar.selectetdCharacter == newCharacter)
            {
                thirdchar.samethirdcharfalse();
                selectetdCharacter = newCharacter;
                forthchartext.text = buttontext[newCharacter].text;
                Statics.currentforthchar = selectetdCharacter;
                charselection.SetActive(false);
                menuoverview.GetComponent<Menucontroller>().somethinginmenuisopen = false;
            }
            else
            {
                selectetdCharacter = newCharacter;
                forthchartext.text = buttontext[newCharacter].text;
                Statics.currentforthchar = selectetdCharacter;
                charselection.SetActive(false);
                menuoverview.GetComponent<Menucontroller>().somethinginmenuisopen = false;
            }
        }
        menucontroller.menusoundcontroller.playmenubuttonsound();
    }
    public void sameforthcharfalse()
    {
        selectetdCharacter = thirdchar.selectetdCharacter;
        forthchartext.text = thirdchar.thirdchartext.text;
        Statics.currentforthchar = selectetdCharacter;
    }
}
