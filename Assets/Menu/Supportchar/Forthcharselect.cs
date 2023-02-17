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
            }
            else
            {
                selectetdCharacter = newCharacter;
                forthchartext.text = buttontext[newCharacter].text;
                Statics.currentforthchar = selectetdCharacter;
                charselection.SetActive(false);
            }
        }
    }
    public void sameforthcharfalse()
    {
        selectetdCharacter = thirdchar.selectetdCharacter;
        forthchartext.text = thirdchar.thirdchartext.text;
        Statics.currentforthchar = selectetdCharacter;
    }
}
