using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selectcharcontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] firstcharicons;
    [SerializeField] private GameObject[] secondcharicons;
    private int firstchar;
    private int secondchar;

    [SerializeField] private GameObject thirdcharselectiongrid;
    [SerializeField] private Text thirdchartext;

    [SerializeField] private GameObject forthcharselectiongrid;
    [SerializeField] private Text forthchartext;

    private SpielerSteu Steuerung;
    [SerializeField] private Menusoundcontroller menusoundcontroller;

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
        }
        thirdandforthchar(newcharacter, firstchar);
        firstchar = newcharacter;
        Statics.currentfirstchar = firstchar;
        menusoundcontroller.playmenubuttonsound();
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
        secondcharicons[newcharacter].SetActive(true);
        if (firstchar == newcharacter)
        {
            secondsameasfirst();
        }
        thirdandforthchar(newcharacter, secondchar);
        secondchar = newcharacter;
        Statics.currentsecondchar = secondchar;
        menusoundcontroller.playmenubuttonsound();
    } 
    private void secondsameasfirst()
    {
        firstcharicons[firstchar].SetActive(false);
        firstchar = secondchar;
        Statics.currentfirstchar = firstchar;
        firstcharicons[firstchar].SetActive(true);
    }

    private void thirdandforthchar(int newchar, int oldchar)
    {
        thirdcharselectiongrid.SetActive(false);
        forthcharselectiongrid.SetActive(false);
        if (newchar == Statics.currentthirdchar)
        {
            Statics.currentthirdchar = oldchar;
            thirdchartext.text = Statics.characternames[oldchar];
        }
        else if (newchar == Statics.currentforthchar)
        {
            Statics.currentforthchar = oldchar;
            forthchartext.text = Statics.characternames[oldchar];
        }
    }
}

