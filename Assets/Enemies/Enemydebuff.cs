using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemydebuff : MonoBehaviour
{
    public bool takendmgdebuff;
    public bool takendmgdebuffcd;
    private float takendmgdebufftime;
    private float takendmgdebuffcdtime;

    public float enemycritdebuff = 0;

    public Image takendmgdebuffbackground;
    public Image takendmgdebuffUI;
    public Image takendmgdebuffcdbackground;
    public Image takendmgdebuffcdUI;
}

    /*public void starttakendmgdebuff()
    {
        StartCoroutine("takendmgbuff");
    }
    IEnumerator takendmgbuff()
    {
        takendmgdebuffbackground.gameObject.SetActive(true);
        takendmgdebuff = true;
        takendmgdebuffcd = true;
        enemycritdebuff = LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().basiccrit;
        takendmgdebuffUI.fillAmount = 0;
        takendmgdebufftime = 0f;

        while (true)
        {
            takendmgdebufftime += Time.deltaTime;
            takendmgdebuffUI.fillAmount = takendmgdebufftime / Statics.enemytakendmgdebuff;

            if (takendmgdebufftime >= Statics.enemytakendmgdebuff)
            {
                takendmgdebuffbackground.gameObject.SetActive(false);
                takendmgdebuff = false;
                StopCoroutine("takendmgbuff");
                StartCoroutine("takendmgbuffcd");
            }
        yield return null;
        }
    }
    IEnumerator takendmgbuffcd()
    {
        enemycritdebuff = LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().basiccrit;
        takendmgdebuffcdbackground.gameObject.SetActive(true);
        takendmgdebuffcdUI.fillAmount = 0;
        takendmgdebuffcdtime = 0f;

        while (true)
        {
            takendmgdebuffcdtime += Time.deltaTime;
            takendmgdebuffcdUI.fillAmount = takendmgdebuffcdtime / Statics.enemytakendmgdebuffcd;

            if (takendmgdebuffcdtime >= Statics.enemytakendmgdebuffcd)
            {
                enemycritdebuff = 0;
                takendmgdebuffcd = false;
                takendmgdebuffcdbackground.gameObject.SetActive(false);
                StopCoroutine("takendmgbuffcd");
            }
        yield return null;
        }
    }
}*/
