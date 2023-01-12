using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battlemonktimer : MonoBehaviour
{
    [SerializeField] private Text timertext;
    private float timer;
    [SerializeField] private float basedmg;

    private void OnEnable()
    {
        timertext.color = Color.red;
        timer = 6f;
        Invoke("dealdmg", 5.3f);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 3)
        {
            float seconds = Mathf.FloorToInt(timer % 60);
            timertext.text = string.Format("{0:00}", seconds);
        }
        else
        {
            timertext.text = "XX";
        }
        if (timer < 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void dealdmg()
    {
        if(Statics.dash == false)
        {
            LoadCharmanager.Overallmainchar.GetComponent<SpielerHP>().TakeDamage(basedmg);
        }
    }
}
