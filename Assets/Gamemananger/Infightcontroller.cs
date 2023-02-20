using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Infightcontroller : MonoBehaviour
{
    static MonoBehaviour instance;

    public GameObject setinfightimage;
    public static GameObject infightimage;
    public static List<GameObject> infightenemylists = new List<GameObject>();

    public static float teammatesdespawntime = 5;

    public float spezialtimer;


    private void Start()
    {
        instance = this;
        infightimage = setinfightimage;
        checkifinfight();
        instance.CancelInvoke();
    }
    public static void checkifinfight()
    {
        if (infightenemylists.Count == 0)
        {
            Statics.infight = false;
            Statics.currentenemyspecialcd = Statics.enemyspecialcd;
            instance.StopCoroutine("enemyspezialcd");
            infightimage.SetActive(false);
            instance.Invoke("disablechars", teammatesdespawntime);
            if(LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().playerisdead == true)
            {
                LoadCharmanager.Overallmainchar.GetComponent<Movescript>().resurrected();
            }
            GlobalCD.stopsupportresurrectioncd();
        }
        else
        {
            GameObject mainchar = LoadCharmanager.Overallmainchar;
            if (Statics.infight == false)
            {
                Statics.infightresurrectcd = Statics.presetresurrectcd;
                Statics.supportcanresurrect = false;
                Statics.oneplayerisdead = false;
                Statics.infight = true;
                instance.StopCoroutine("healalliesafterfight");
                instance.StartCoroutine("enemyspezialcd");
            }
            infightimage.SetActive(true);
            instance.CancelInvoke();                        //unterbricht den Allie despawn wenn man wieder infight kommmt
            if (LoadCharmanager.Overallthirdchar !=null && LoadCharmanager.Overallthirdchar.activeSelf == false)                              
            {
                LoadCharmanager.Overallthirdchar.transform.position = mainchar.transform.position + mainchar.transform.forward * -1 + mainchar.transform.right * -1;
                LoadCharmanager.Overallthirdchar.SetActive(true);
            }
            if (LoadCharmanager.Overallforthchar != null && LoadCharmanager.Overallforthchar.activeSelf == false)
            {
                LoadCharmanager.Overallforthchar.transform.position = mainchar.transform.position + mainchar.transform.forward * -1 + mainchar.transform.right * 1;
                LoadCharmanager.Overallforthchar.SetActive(true);
            }
        }
    }
    private void disablechars()
    {
        StartCoroutine("healalliesafterfight");
        if (LoadCharmanager.Overallthirdchar != null)
        {
            if (LoadCharmanager.Overallthirdchar.TryGetComponent(out Playerhp playerhp))
            {
                playerhp.playerisdead = false;
                playerhp.addhealth(Mathf.Round(Statics.charmaxhealth[Statics.currentthirdchar] * 0.1f));
            }
            LoadCharmanager.Overallthirdchar.SetActive(false);
        }
        if (LoadCharmanager.Overallforthchar != null)
        {
            if (LoadCharmanager.Overallforthchar.TryGetComponent(out Playerhp playerhp))
            {
                playerhp.playerisdead = false;
                playerhp.addhealth(Mathf.Round(Statics.charmaxhealth[Statics.currentforthchar] * 0.1f));
            }
            LoadCharmanager.Overallforthchar.SetActive(false);
        }
    }
    IEnumerator enemyspezialcd()
    {
        while (true)
        {
            yield return new WaitForSeconds(Statics.currentenemyspecialcd);
            if(LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().playerisdead == false)
            {
                int enemycount = infightenemylists.Count;
                int enemyonlist = UnityEngine.Random.Range(1, enemycount + 1);
                if (infightenemylists[enemyonlist - 1].GetComponent<Enemymovement>())
                {
                    infightenemylists[enemyonlist - 1].GetComponent<Enemymovement>().spezialattack = true;
                }
            }
        }
    }
    IEnumerator healalliesafterfight()
    {
        Playerhp charhp = LoadCharmanager.Overallmainchar.GetComponent<Playerhp>();
        int maxticks = 0;
        while (true)
        {
            yield return new WaitForSeconds(2);
            maxticks++;
            charhp.addhealth(Mathf.Round(Statics.charmaxhealth[Statics.currentfirstchar] * 0.1f));
            if (LoadCharmanager.Overallthirdchar != null)
            {
                LoadCharmanager.Overallthirdchar.GetComponent<Playerhp>().addhealth(Mathf.Round(Statics.charmaxhealth[Statics.currentthirdchar] * 0.11f));
            }
            if (LoadCharmanager.Overallforthchar != null)
            {
                LoadCharmanager.Overallforthchar.GetComponent<Playerhp>().addhealth(Mathf.Round(Statics.charmaxhealth[Statics.currentforthchar] * 0.11f));
            }
            if (maxticks >= 9)
            {
                StopCoroutine("healalliesafterfight");
            }
        }
    }
}
