using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closemap : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private RectTransform playerposi;
    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        float xposi = LoadCharmanager.Overallmainchar.transform.position.x * 1.41f;
        float zposi = (LoadCharmanager.Overallmainchar.transform.position.z - 350) * 1.16f;
        playerposi.anchoredPosition = new Vector2(xposi, zposi);
        playerposi.rotation = Quaternion.Euler(0, 0, LoadCharmanager.Overallmainchar.transform.localRotation.eulerAngles.y * -1);
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame()) gameObject.SetActive(false);  //+ wir im infightcontroller deaktiviert
    }

}
