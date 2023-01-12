using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menucharcontroller : MonoBehaviour
{
    [SerializeField] internal Secondcharcontroller secondchar;
    public GameObject[] charactersicon;
    public int selectetdCharacter;

    public GameObject thirdcharselection;
    public Text thirdchartext;

    public GameObject forthcharselection;
    public Text forthchartext;

    private SpielerSteu Steuerung;

    private void Awake()
    {
        Steuerung = new SpielerSteu();
    }
    private void OnEnable()
    {
        Steuerung.Enable();
    }
    void Start()
    {
        foreach (GameObject Chars in charactersicon)
        {
            Chars.SetActive(false);
        }
        int maincharload = PlayerPrefs.GetInt("Maincharindex");
        charactersicon[maincharload].SetActive(true);
        selectetdCharacter = maincharload;
    }
    public void ChangeCharacter(int newCharacter)
    {
        charactersicon[selectetdCharacter].SetActive(false);
        charactersicon[newCharacter].SetActive(true);
        if (secondchar.selectetdCharacter == newCharacter)
        {
            secondchar.samenumberfalse();
            selectetdCharacter = newCharacter;
            PlayerPrefs.SetInt("Maincharindex", selectetdCharacter);
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
            PlayerPrefs.SetInt("Maincharindex", selectetdCharacter);
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
        selectetdCharacter = secondchar.selectetdCharacter;
        PlayerPrefs.SetInt("Maincharindex", selectetdCharacter);
        charactersicon[selectetdCharacter].SetActive(true);
    }
    public void savecharplayerprefs()
    {
        PlayerPrefs.SetInt("Maincharindex", selectetdCharacter);
        PlayerPrefs.SetInt("Secondcharindex", secondchar.selectetdCharacter);
    }
}


/*{
    foreach (GameObject Chars in charactersicon)
    {
        Chars.SetActive(false);
    }
    int maincharload = PlayerPrefs.GetInt("Maincharindex");
    charactersicon[maincharload].SetActive(true);
    selectetdCharacter = maincharload;
    maincharloadindex = selectetdCharacter;
}
void Update()
{
    if (Steuerung.Menusteuerung.Loadgame.WasPerformedThisFrame())
    {
        Startgame();
    }
}
public void ChangeCharacter(int newCharacter)
{
    charactersicon[selectetdCharacter].SetActive(false);
    charactersicon[newCharacter].SetActive(true);
    if (secondchar.selectetdCharacter == newCharacter)
    {
        secondchar.samenumberfalse();
        selectetdCharacter = newCharacter;
        maincharloadindex = selectetdCharacter;
        PlayerPrefs.SetInt("Maincharindex", maincharloadindex);
        thirdcharselection.SetActive(false);
        forthcharselection.SetActive(false);
    }
    else
    {
        selectetdCharacter = newCharacter;
        maincharloadindex = selectetdCharacter;
        PlayerPrefs.SetInt("Maincharindex", maincharloadindex);
        thirdcharselection.SetActive(false);
        forthcharselection.SetActive(false);
    }
}
public void samenumberfalse()
{
    charactersicon[selectetdCharacter].SetActive(false);
    selectetdCharacter = secondchar.selectetdCharacter;
    PlayerPrefs.SetInt("Maincharindex", selectetdCharacter);
    Changecharwhensame(selectetdCharacter);
}
public void Changecharwhensame(int newCharacter)
{
    charactersicon[newCharacter].SetActive(true);
    selectetdCharacter = newCharacter;
    maincharloadindex = selectetdCharacter;
    PlayerPrefs.SetInt("Maincharindex", maincharloadindex);
}
public void Startgame()
{
    SceneManager.LoadScene(0);
    PlayerPrefs.SetInt("Maincharindex", maincharloadindex);
    PlayerPrefs.SetInt("Secondcharindex", secondchar.secondcharloadindex);
}*/

