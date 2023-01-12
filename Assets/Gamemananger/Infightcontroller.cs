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

    public static event Action setsupporttarget;

    public float spezialtimer;
    private bool spezialtimerisrunning;


    private void Start()
    {
        instance = this;
        infightimage = setinfightimage;
        checkifinfight();
        instance.CancelInvoke();
    }
    private void OnEnable()
    {
        EnemyHP.infightlistupdate += checkifinfight;
        Enemymovement.infightlistupdate += checkifinfight;
    }
    private void OnDisable()
    {
        EnemyHP.infightlistupdate -= checkifinfight;
        Enemymovement.infightlistupdate -= checkifinfight;
    }
    public static void checkifinfight()
    {
        if (infightenemylists.Count == 0)
        {
            Statics.infight = false;
            Statics.currentenemyspecialcd = Statics.enemyspecialcd;
            instance.StopCoroutine("enemyspezialcd");
            infightimage.SetActive(false);
            if (LoadCharmanager.Overallthirdchar != null)                               
            {
                instance.Invoke("disablethirdchar", teammatesdespawntime);
            }
            if (LoadCharmanager.Overallforthchar != null)
            {
                instance.Invoke("disableforthchar", teammatesdespawntime);
            }
        }
        else
        {
            GameObject mainchar = LoadCharmanager.Overallmainchar;
            setsupporttarget?.Invoke();
            if (Statics.infight == false)
            {
                Statics.infight = true;
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
    public void disablethirdchar()
    {
        LoadCharmanager.Overallthirdchar.SetActive(false);
    }
    public void disableforthchar()
    {
        LoadCharmanager.Overallforthchar.SetActive(false);
    }
    IEnumerator enemyspezialcd()
    {
        while (true)
        {
            yield return new WaitForSeconds(Statics.currentenemyspecialcd);
            int enemycount = infightenemylists.Count;
            //Debug.Log(Statics.currentenemyspecialcd);
            int enemyonlist = UnityEngine.Random.Range(1, enemycount +1);
            if (infightenemylists[enemyonlist - 1].GetComponentInChildren<Enemymovement>())                        //inChildren weil ich momentan desn collider auf die liste setzte
            {
                infightenemylists[enemyonlist - 1].GetComponentInChildren<Enemymovement>().spezialattack = true;
            }
        }
    }
}
