using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bonuscritstacksscript : MonoBehaviour
{
    private TextMeshProUGUI stacktext;
    private void Awake()
    {
        stacktext = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        EnemyHP.addbonuscritstacks += checkstacks;
    }
    private void OnDisable()
    {
        EnemyHP.addbonuscritstacks -= checkstacks;
    }
    private void checkstacks()
    {
        if(Statics.bonuscritstacks <= Statics.bonuscritstacksneeded)
        {
            if(Statics.bonuscritstacks == Statics.bonuscritstacksneeded) Statics.bonusdashcantrigger = true;
            stacktext.text = Statics.bonuscritstacks.ToString();
        }
    }
}
