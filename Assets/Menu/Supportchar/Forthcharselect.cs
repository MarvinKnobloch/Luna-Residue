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

    private void Start()
    {
        if (PlayerPrefs.GetInt("Forthcharindex") < 8)
        {
            forthchartext.text = buttontext[PlayerPrefs.GetInt("Forthcharindex")].text;
        }
        else
        {
            forthchartext.text = "empty";
        }
    }
    private void OnEnable()
    {
        charselection.SetActive(false);
        selectetdCharacter = PlayerPrefs.GetInt("Forthcharindex");
    }
    public void changeforthcharacter(int newCharacter)
    {
        if (thirdchar.selectetdCharacter == newCharacter)
        {
            thirdchar.samethirdcharfalse();
            selectetdCharacter = newCharacter;
            forthchartext.text = buttontext[newCharacter].text;
            PlayerPrefs.SetInt("Forthcharindex", selectetdCharacter);
            charselection.SetActive(false);
        }
        else
        {
            selectetdCharacter = newCharacter;
            forthchartext.text = buttontext[newCharacter].text;
            PlayerPrefs.SetInt("Forthcharindex", selectetdCharacter);
            charselection.SetActive(false);
        }
    }
    public void sameforthcharfalse()
    {
        selectetdCharacter = thirdchar.selectetdCharacter;
        forthchartext.text = thirdchar.thirdchartext.text;
        PlayerPrefs.SetInt("Forthcharindex", selectetdCharacter);
    }
}
