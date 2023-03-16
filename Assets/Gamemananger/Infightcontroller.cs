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
    //private int maxhealticks;
    public float spezialtimer;

    [SerializeField] private GameObject gameovercontroller;

    private void OnEnable()
    {
        Playerhp.triggergameover += activategameovercontroller;
    }
    private void OnDisable()
    {
        Playerhp.triggergameover -= activategameovercontroller;
    }

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
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().endlockon();
            if (LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().playerisdead == true)
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
                LoadCharmanager.Overallmainchar.GetComponent<Movescript>().autolockon();
                LoadCharmanager.Overallmainchar.GetComponent<Movescript>().spawnallies();
            }
            infightimage.SetActive(true);
            instance.CancelInvoke();                        //unterbricht den Allie despawn wenn man wieder infight kommmt
        }
    }
    IEnumerator enemyspezialcd()
    {
        while (true)
        {
            yield return new WaitForSeconds(Statics.currentenemyspecialcd);
            if (LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().playerisdead == false)
            {
                int enemyonlist = UnityEngine.Random.Range(1, infightenemylists.Count + 1);          //+ 1 weil random.range bei 1-2 immer nur 1 ausgibt
                if (infightenemylists[enemyonlist - 1].GetComponent<Enemymovement>())
                {
                    infightenemylists[enemyonlist - 1].GetComponent<Enemymovement>().spezialattack = true;
                }
            }
        }
    }
    public void disablechars()
    {
        rescharsafterfight(LoadCharmanager.Overallsecondchar, Statics.currentsecondchar);
        rescharsafterfight(LoadCharmanager.Overallthirdchar, Statics.currentthirdchar);
        rescharsafterfight(LoadCharmanager.Overallforthchar, Statics.currentforthchar);
        if (LoadCharmanager.Overallthirdchar != null) LoadCharmanager.Overallthirdchar.SetActive(false);
        if (LoadCharmanager.Overallforthchar != null) LoadCharmanager.Overallforthchar.SetActive(false);
        StartCoroutine("healalliesafterfight");
    }
    private void rescharsafterfight(GameObject character, int slot)
    {
        if (character != null)
        {
            if (character.TryGetComponent(out Playerhp playerhp))
            {
                if (playerhp.playerisdead == true)
                {
                    playerhp.playerisdead = false;
                    playerhp.health = 1;
                }
                playerhp.addhealth(Mathf.Round(Statics.charmaxhealth[slot] * 0.1f));
            }
        }
    }
    IEnumerator healalliesafterfight()
    {
        int maxticks = 0;
        while (true)
        {
            yield return new WaitForSeconds(2);
            maxticks++;
            LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().addhealth(Mathf.Round(Statics.charmaxhealth[Statics.currentfirstchar] * 0.1f));
            if(LoadCharmanager.Overallsecondchar != null)
            {
                LoadCharmanager.Overallsecondchar.GetComponent<Playerhp>().addhealth(Mathf.Round(Statics.charmaxhealth[Statics.currentsecondchar] * 0.11f));
            }
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
    public void gameover()
    {
        infightenemylists.Clear();
        Statics.currentenemyspecialcd = Statics.enemyspecialcd;
        Statics.infight = false;
        instance.StopCoroutine("enemyspezialcd");
        infightimage.SetActive(false);
    }
    public void activategameovercontroller()
    {
        gameovercontroller.SetActive(true);
    }
}
