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

    private void Start()
    {
        if (PlayerPrefs.GetInt("Thirdcharindex") < 8)
        {
            thirdchartext.text = buttontext[PlayerPrefs.GetInt("Thirdcharindex")].text;
        }
        else
        {
            thirdchartext.text = "empty";
        }
    }
    private void OnEnable()
    {
        charselection.SetActive(false);
        selectetdCharacter = PlayerPrefs.GetInt("Thirdcharindex");
    }
    public void changethirdcharacter(int newCharacter)
    {
        if (forthchar.selectetdCharacter == newCharacter)
        {
            forthchar.sameforthcharfalse();
            selectetdCharacter = newCharacter;
            thirdchartext.text = buttontext[newCharacter].text;
            PlayerPrefs.SetInt("Thirdcharindex", selectetdCharacter);
            charselection.SetActive(false);
        }
        else
        {
            selectetdCharacter = newCharacter;
            thirdchartext.text = buttontext[newCharacter].text;
            PlayerPrefs.SetInt("Thirdcharindex", selectetdCharacter);
            charselection.SetActive(false);
        }
    }
    public void samethirdcharfalse()
    {
        selectetdCharacter = forthchar.selectetdCharacter;
        thirdchartext.text = forthchar.forthchartext.text;
        PlayerPrefs.SetInt("Thirdcharindex", selectetdCharacter);
    }
}
