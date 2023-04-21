using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Doozycontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] numbers;
    [SerializeField] private GameObject[] secondnumbers;
    [SerializeField] private GameObject firstgrid;
    [SerializeField] private GameObject secondgrid;

    [SerializeField] private CinemachineFreeLook maincam;

    public int memoryclicknumber;
    public bool canclick;
    private int activnumber;
    private int choosenumber;

    [SerializeField] private float basedmg;
    private int dmgcount;

    //const string dazestate = "Daze";

    private void OnEnable()
    {
        canclick = true;
        memoryclicknumber = 1;
        choosenumber = 1;
        Invoke("turnofffirstgrid", 3f);
        foreach (GameObject obj in numbers)
        {
            obj.transform.SetAsLastSibling();
            obj.SetActive(false);
        }
        activnumber = 0;
        while (activnumber < 4)
        {
            int randomnumber = Random.Range(0, numbers.Length);
            if(numbers[randomnumber].activeSelf == false)
            {
                secondnumbers[randomnumber].GetComponent<Doozynumber>().setnumber = choosenumber;
                numbers[randomnumber].transform.SetAsLastSibling();
                numbers[randomnumber].SetActive(true);
                activnumber++;
                choosenumber++;
            }
        }
        firstgrid.SetActive(true);
    }
    private void turnofffirstgrid()
    {
        firstgrid.SetActive(false);
        Invoke("turnonsecondgrid", 4f);
    }
    private void turnonsecondgrid()
    {
        Mouseactivate.enablemouse();
        Statics.enemyspezialtimescale = true;
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtostun();
        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = Statics.normaltimedelta * 0.3f;
        maincam.m_YAxis.m_MaxSpeed = 0;
        maincam.m_XAxis.m_MaxSpeed = 0;
        secondgrid.SetActive(true);
        Invoke("dealdmg", 2f);
    }
    public void fail()
    {
        CancelInvoke();
        Invoke("dealdmg", 0.4f);
    }
    private void dealdmg()
    {       
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
        dmgcount = 5 - memoryclicknumber;
        if(Statics.infight == true)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(dmgcount * basedmg + Globalplayercalculations.calculateenemyspezialdmg());
        }
        turnoff();
    }
    public void success()
    {
        CancelInvoke();
        Invoke("turnoff", 0.4f);
    }
    public void turnoff()
    {
        Statics.otheraction = false;
        Statics.dash = false;
        Statics.enemyspezialtimescale = false;
        Time.timeScale = Statics.normalgamespeed;
        Time.fixedDeltaTime = Statics.normaltimedelta;
        maincam.m_YAxis.m_MaxSpeed = Statics.presetcamymaxspeed * PlayerPrefs.GetFloat("mousesensitivity") / 50;
        maincam.m_XAxis.m_MaxSpeed = Statics.presetcamxmaxspeed * PlayerPrefs.GetFloat("mousesensitivity") / 50;
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
        CancelInvoke();
        secondgrid.SetActive(false);
        gameObject.SetActive(false);
        Mouseactivate.disablemouse();
    }
}
