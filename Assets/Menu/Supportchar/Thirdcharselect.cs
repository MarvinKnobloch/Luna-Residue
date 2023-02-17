using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thirdcharselect : MonoBehaviour
{
    [SerializeField] internal Forthcharselect forthchar;

    public Text thirdchartext;
    public Text[] buttontext;
    public GameObject charselection;

    public int selectetdCharacter;

    private void OnEnable()
    {
        if (Statics.currentthirdchar != -1)
        {
            thirdchartext.text = buttontext[Statics.currentthirdchar].text;
        }
        else
        {
            thirdchartext.text = "empty";
        }
        charselection.SetActive(false);
        selectetdCharacter = Statics.currentthirdchar;
    }
    public void changethirdcharacter(int newCharacter)
    {
        if(newCharacter == -1)
        {
            Statics.currentthirdchar = -1;
            thirdchartext.text = "empty";
            charselection.SetActive(false);
        }
        else
        {
            if (forthchar.selectetdCharacter == newCharacter)
            {
                forthchar.sameforthcharfalse();
                selectetdCharacter = newCharacter;
                thirdchartext.text = buttontext[newCharacter].text;
                Statics.currentthirdchar = selectetdCharacter;
                charselection.SetActive(false);
            }
            else
            {
                selectetdCharacter = newCharacter;
                thirdchartext.text = buttontext[newCharacter].text;
                Statics.currentthirdchar = selectetdCharacter;
                charselection.SetActive(false);
            }
        }

    }
    public void samethirdcharfalse()
    {
        selectetdCharacter = forthchar.selectetdCharacter;
        thirdchartext.text = forthchar.forthchartext.text;
        Statics.currentthirdchar = selectetdCharacter;
    }
}
