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
        timer = 5.9f;
        Invoke("dealdmg", 5.4f);
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
        if(Statics.infight == true)
        {
            if (Statics.dash == false && Statics.bonusiframes == false)
            {
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(basedmg + Globalplayercalculations.calculateenemyspezialdmg());
            }
        }
    }
}
