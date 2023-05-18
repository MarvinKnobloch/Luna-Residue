using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Setdifficulty : MonoBehaviour
{
    [SerializeField] private GameObject selection;
    [SerializeField] private TextMeshProUGUI difficultytext;
    [SerializeField] private GameObject difficultymessage;

    [SerializeField] private Menusoundcontroller menusoundcontroller;
    private void OnEnable()
    {
        if (Statics.difficulty == 0) difficultytext.text = "Easy";
        else if (Statics.difficulty == 1) difficultytext.text = "Normal";
        else difficultytext.text = "Hard";
        selection.SetActive(false);
    }
    public void openselection()
    {
        if (selection.activeSelf == false)
        {
            selection.SetActive(true);
        }
        else
        {
            selection.SetActive(false);
        }
    }
    public void easy()
    {
        Statics.difficulty = 0;
        difficultytext.text = "Easy";
        selection.SetActive(false);
        difficultymessage.SetActive(true);
        menusoundcontroller.playmenubuttonsound();
    }
    public void normal()
    {
        Statics.difficulty = 1;
        difficultytext.text = "Normal";
        selection.SetActive(false);
        difficultymessage.SetActive(true);
        menusoundcontroller.playmenubuttonsound();
    }
    public void hard()
    {
        Statics.difficulty = 2;
        difficultytext.text = "Hard";
        selection.SetActive(false);
        difficultymessage.SetActive(true);
        menusoundcontroller.playmenubuttonsound();
    }
}
