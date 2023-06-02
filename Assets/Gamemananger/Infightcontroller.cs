using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Infightcontroller : MonoBehaviour
{
    public static Infightcontroller instance;

    [SerializeField] GameObject infightimage;
    public static List<GameObject> infightenemylists = new List<GameObject>();

    public static float teammatesdespawntime = 5;
    public float spezialtimer;

    [SerializeField] private GameObject map;
    [SerializeField] private GameObject playergameover;
    [SerializeField] private GameObject gameovercontroller;

    private Bonushealscript bonushealscript;

    [SerializeField] private AudioClip battle1;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        bonushealscript = GetComponent<Bonushealscript>();
    }
    private void OnEnable()
    {
        Playerhp.playergameover += activateplayergameover;
        Playerhp.removeplayergameover += disableplayergameover;
        Playerhp.triggergameover += activategameovercontroller;
    }
    private void OnDisable()
    {
        Playerhp.playergameover -= activateplayergameover;
        Playerhp.removeplayergameover -= disableplayergameover;
        Playerhp.triggergameover -= activategameovercontroller;
    }

    private void Start()
    {
        //checkifinfight();                   //ausgeblendet wegen musik call, weiß auch nicht für was der funktions call ist
        instance.CancelInvoke();
    }
    public void checkifinfight()
    {
        if (infightenemylists.Count == 0)
        {
            Musiccontroller.instance.startfadeout(Musiccontroller.instance.currentzonemusic, Musiccontroller.instance.currentzonemusictime, 2, 3);
            Statics.infight = false;
            Statics.supportcanresurrect = false;
            Statics.currentenemyspecialcd = Statics.enemyspecialcd;
            instance.StopCoroutine("firstenemyspezialcd");
            instance.StopCoroutine("enemyspezialcd");
            bonushealscript.enabled = false;
            infightimage.SetActive(false);
            instance.Invoke("disablechars", teammatesdespawntime);
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().endlockon();
            if (LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().playerisdead == true)
            {
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().disableplayergameoverui();
                LoadCharmanager.Overallmainchar.GetComponent<Movescript>().resurrected();
            }
            GlobalCD.stopsupportresurrectioncd();
        }
        else
        {
            if (Statics.infight == false)
            {
                infightimage.SetActive(true);
                instance.CancelInvoke();                        //unterbricht den Allie despawn wenn man wieder infight kommmt
                Statics.nextattackdealbonusdmg = false;
                Statics.infightresurrectcd = Statics.presetresurrectcd;
                GlobalCD.stopsupportresurrectioncd();                       //res probleme weil supportrezzcdisrunning nicht resetet wird???? 
                Statics.supportcanresurrect = false;
                Statics.infight = true;
                map.SetActive(false);
                instance.StopCoroutine("healalliesafterfight");
                instance.StartCoroutine("firstenemyspezialcd");
                if (Statics.bonushealovertimebool == true)
                {
                    bonushealscript.healovertimeremainingtime = Statics.bonushealtimer;
                    bonushealscript.enabled = true;
                }
                LoadCharmanager.Overallmainchar.GetComponent<Movescript>().autolockon();
                LoadCharmanager.Overallmainchar.GetComponent<Movescript>().spawnallies();
            }
        }
    }
    IEnumerator firstenemyspezialcd()
    {
        int firstcd = UnityEngine.Random.Range(3, (int)Statics.currentenemyspecialcd);
        yield return new WaitForSeconds(firstcd);
        instance.StartCoroutine("enemyspezialcd");
        if (LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().playerisdead == false)
        {
            int enemyonlist = UnityEngine.Random.Range(1, infightenemylists.Count + 1);          //+ 1 weil random.range bei 1-2 immer nur 1 ausgibt
            if (infightenemylists[enemyonlist - 1].GetComponent<Enemymovement>())
            {
                infightenemylists[enemyonlist - 1].GetComponent<Enemymovement>().spezialattack = true;
            }
        }
    }
    IEnumerator enemyspezialcd()
    {
        while (true)
        {
            yield return new WaitForSeconds(Statics.currentenemyspecialcd);
            if (LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().playerisdead == false)
            {
                int enemyonlist = UnityEngine.Random.Range(1, infightenemylists.Count + 1);          //+ 1 weil random.range bei int die höchste zahl nicht nimmt
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
    public void activateplayergameover() => playergameover.SetActive(true);
    public void disableplayergameover() => playergameover.SetActive(false);
    public void activategameovercontroller()
    {
        gameovercontroller.SetActive(true);
    }
    public void savegameoverposi(Vector3 saveposi)
    {
        Statics.gameoverposi = saveposi;
        Statics.gameoverrota = LoadCharmanager.Overallmainchar.transform.rotation;
        Statics.gameovercam = LoadCharmanager.savecamvalueX;
        Statics.aftergameovermusic = Statics.currentzonemusicint;
    }
}
