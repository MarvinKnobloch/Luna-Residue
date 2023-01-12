using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mathnumbers : MonoBehaviour
{
    [SerializeField] private Text solution;
    [SerializeField] private Text number;

    public void setnumber()
    {
        solution.text += number.text;
    }
}
