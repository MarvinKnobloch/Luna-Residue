using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameovercontroller : MonoBehaviour
{
    [SerializeField] private Infightcontroller infightcontroller;
    [SerializeField] private CanvasGroup gameover;
    [SerializeField] private float gameoverfadeinspeed;

    private void OnEnable()
    {
        gameover.alpha = 0;
        StartCoroutine("gameoverfade");
    }
    IEnumerator gameoverfade()
    {
        yield return new WaitForSeconds(0.01f);
        gameover.alpha += gameoverfadeinspeed * Time.deltaTime;
        if(gameover.alpha > 0.8f)
        {
            StopCoroutine("gameoverfade");
            LoadCharmanager.savemainposi = Statics.gameoverposi;
            LoadCharmanager.savemainrota = Statics.gameoverrota;
            LoadCharmanager.savecamvalueX = Statics.savecamvalueX;
            infightcontroller.gameover();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine("gameoverfade");
        }
    }
}
