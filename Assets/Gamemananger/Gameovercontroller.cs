using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameovercontroller : MonoBehaviour
{
    [SerializeField] private Infightcontroller infightcontroller;
    [SerializeField] private CanvasGroup gameover;
    [SerializeField] private float gameoverfadeinspeed;

    [SerializeField] private GameObject Loadingscreen;
    [SerializeField] private Image loadingscreenbar;

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
            StartCoroutine("loadingscreen");
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine("gameoverfade");
        }
    }
    IEnumerator loadingscreen()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        Loadingscreen.SetActive(true);

        while (!operation.isDone)
        {
            float loadingbaramount = Mathf.Clamp01(operation.progress / 0.9f);
            loadingscreenbar.fillAmount = loadingbaramount;
            yield return null;
        }
    }
}
