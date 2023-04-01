using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Commitfasttravel : MonoBehaviour
{
    public Vector3 fasttravelpoint;
    [SerializeField] private GameObject loadcharmananger;
    [SerializeField] private GameObject enemyhealthbars;

    [SerializeField] private GameObject loadingscreen;
    [SerializeField] private Image loadingscreenbar;
    [SerializeField] private Menusoundcontroller menusoundcontroller;

    private SpielerSteu controlls;
    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        controlls.Enable();
        menusoundcontroller.playmenubuttonsound();
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            closecommit();
        }
    }
    public void fasttravel()
    {
        menusoundcontroller.playmenubuttonsound();
        foreach (Transform enemys in enemyhealthbars.transform)
        {
            enemys.GetComponent<Enemyhealthbar>().removehealthbar();
        }
        LoadCharmanager.savemainposi = fasttravelpoint;
        StartCoroutine(loadgameloadingscreen());
        //SceneManager.LoadScene(1);
        //loadcharmananger.GetComponent<LoadCharmanager>().loadonfastravel();
        gameObject.SetActive(false);

    }
    IEnumerator loadgameloadingscreen()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        loadingscreen.SetActive(true);

        while (!operation.isDone)
        {
            float loadingbaramount = Mathf.Clamp01(operation.progress / 0.9f);
            loadingscreenbar.fillAmount = loadingbaramount;
            yield return null;
        }
    }
    public void closecommit()
    {
        gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        menusoundcontroller.playmenubuttonsound();
    }
}
