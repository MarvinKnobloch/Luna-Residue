using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setspellnumbers : MonoBehaviour
{
    [SerializeField] private Dragspell[] dragspell;
    [SerializeField] private GameObject dragimage;
    private void Awake()
    {
        int number = 0;
        foreach (Dragspell spell in dragspell)
        {
            spell.spellnumber = number;
            spell.dragimage = dragimage;
            number++;
        }
    }
}
