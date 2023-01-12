using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Secondcharcontroller : MonoBehaviour
{
    [SerializeField] internal Menucharcontroller mainchar;
    public GameObject[] charactersicon;
    public int selectetdCharacter;

    public GameObject thirdcharselection;
    public Text thirdchartext;

    public GameObject forthcharselection;
    public Text forthchartext;
    void Start()
    {
        foreach (GameObject Chars in charactersicon)
        {
            Chars.SetActive(false);
        }
        int secondcharload = PlayerPrefs.GetInt("Secondcharindex");
        charactersicon[secondcharload].SetActive(true);
        selectetdCharacter = secondcharload;
    }
    public void ChangeCharacter(int newCharacter)
    {
        charactersicon[selectetdCharacter].SetActive(false);
        charactersicon[newCharacter].SetActive(true);
        if (mainchar.selectetdCharacter == newCharacter)
        {
            mainchar.samenumberfalse();
            selectetdCharacter = newCharacter;
            PlayerPrefs.SetInt("Secondcharindex", selectetdCharacter);
            thirdcharselection.SetActive(false);
            forthcharselection.SetActive(false);
            if (selectetdCharacter == PlayerPrefs.GetInt("Thirdcharindex"))
            {
                PlayerPrefs.SetInt("Thirdcharindex", 8);
                thirdchartext.text = "empty";
            }
            if (selectetdCharacter == PlayerPrefs.GetInt("Forthcharindex"))
            {
                PlayerPrefs.SetInt("Forthcharindex", 8);
                forthchartext.text = "empty";
            }
        }
        else
        {
            selectetdCharacter = newCharacter;
            PlayerPrefs.SetInt("Secondcharindex", selectetdCharacter);
            thirdcharselection.SetActive(false);
            forthcharselection.SetActive(false);
            if (selectetdCharacter == PlayerPrefs.GetInt("Thirdcharindex"))
            {
                PlayerPrefs.SetInt("Thirdcharindex", 8);
                thirdchartext.text = "empty";
            }
            if (selectetdCharacter == PlayerPrefs.GetInt("Forthcharindex"))
            {
                PlayerPrefs.SetInt("Forthcharindex", 8);
                forthchartext.text = "empty";
            }
        }
    } 
    public void samenumberfalse()
    {
        charactersicon[selectetdCharacter].SetActive(false);
        selectetdCharacter = mainchar.selectetdCharacter;
        PlayerPrefs.SetInt("Secondcharindex", selectetdCharacter);
        charactersicon[selectetdCharacter].SetActive(true);
    }
}

/*{
    selectetdCharacter = 1;
    foreach (GameObject Chars in charactersicon)
    {
        Chars.SetActive(false);
    }
    int secondcharload = PlayerPrefs.GetInt("Secondcharindex");
    charactersicon[secondcharload].SetActive(true);
    selectetdCharacter = secondcharload;
    secondcharloadindex = selectetdCharacter;
}
public void ChangeCharacter(int newCharacter)
{
    charactersicon[selectetdCharacter].SetActive(false);
    charactersicon[newCharacter].SetActive(true);
    if (mainchar.selectetdCharacter == newCharacter)
    {
        mainchar.samenumberfalse();
        selectetdCharacter = newCharacter;
        secondcharloadindex = selectetdCharacter;
        PlayerPrefs.SetInt("Secondcharindex", secondcharloadindex);
        thirdcharselection.SetActive(false);
        forthcharselection.SetActive(false);
    }
    else
    {
        selectetdCharacter = newCharacter;
        secondcharloadindex = selectetdCharacter;
        PlayerPrefs.SetInt("Secondcharindex", secondcharloadindex);
        thirdcharselection.SetActive(false);
        forthcharselection.SetActive(false);
    }
}
public void samenumberfalse()
{
    charactersicon[selectetdCharacter].SetActive(false);
    selectetdCharacter = mainchar.selectetdCharacter;
    PlayerPrefs.SetInt("Secondcharindex", selectetdCharacter);
    Changecharwhensame(selectetdCharacter);
}
public void Changecharwhensame(int newCharacter)
{
    charactersicon[newCharacter].SetActive(true);
    selectetdCharacter = newCharacter;
    secondcharloadindex = selectetdCharacter;
    PlayerPrefs.SetInt("Secondcharindex", secondcharloadindex);
}
}*/


