using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menucharcontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] firstcharicons;
    [SerializeField] private GameObject[] secondcharicons;
    private int firstchar;
    private int secondchar;

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
        foreach (GameObject Chars in firstcharicons)
        {
            Chars.SetActive(false);
        }
        firstchar = Statics.currentfirstchar;
        firstcharicons[firstchar].SetActive(true);
        secondchar = Statics.currentsecondchar;
        secondcharicons[secondchar].SetActive(true);
    }
    public void changefirstchar(int newcharacter)
    {
        firstcharicons[firstchar].SetActive(false);
        firstcharicons[newcharacter].SetActive(true);
        if (secondchar == newcharacter)
        {
            firstsameassecond();
            firstchar = newcharacter;
            Statics.currentfirstchar = firstchar;
        }
        else
        {
            firstchar = newcharacter;
            Statics.currentfirstchar = firstchar;
        }
        thirdandforthchar();
    }
    private void firstsameassecond()
    {
        secondcharicons[secondchar].SetActive(false);
        secondchar = firstchar;
        Statics.currentsecondchar = secondchar;
        secondcharicons[secondchar].SetActive(true);
    }

    public void changesecondchar(int newcharacter)
    {
        secondcharicons[secondchar].SetActive(false);
        secondcharicons[secondchar].SetActive(true);
        if (firstchar == newcharacter)
        {
            secondsameasfirst();
            secondchar = newcharacter;
            Statics.currentsecondchar = secondchar;
        }
        else
        {
            secondchar = newcharacter;
            Statics.currentsecondchar = secondchar;
        }
        thirdandforthchar();
    }
    private void secondsameasfirst()
    {
        firstcharicons[firstchar].SetActive(false);
        firstchar = secondchar;
        Statics.currentfirstchar = firstchar;
        firstcharicons[firstchar].SetActive(true);
    }

    private void thirdandforthchar()
    {
        thirdcharselection.SetActive(false);
        forthcharselection.SetActive(false);
        if (firstchar == Statics.currentthirdchar)
        {
            Statics.currentthirdchar = -1;
            thirdchartext.text = "empty";
        }
        if (firstchar == Statics.currentforthchar)
        {
            Statics.currentforthchar = -1;
            forthchartext.text = "empty";
        }
    }
    public void savecharplayerprefs()
    {
        Debug.Log("hallo");
        PlayerPrefs.SetInt("Maincharindex", firstchar);
        //PlayerPrefs.SetInt("Secondcharindex", secondchar.selectetdCharacter);
    }
}

