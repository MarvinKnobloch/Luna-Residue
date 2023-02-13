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

    public static int memoryclicknumber;
    public static bool canclick;
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
        Invoke("turnofffirstgrid", 2f);
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
        Invoke("turnonsecondgrid", 5f);
    }
    private void turnonsecondgrid()
    {
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtostun();
        Statics.enemyspezialtimescale = true;
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = Statics.normaltimedelta * 0.5f;
        maincam.m_YAxis.m_MaxSpeed = 0;
        maincam.m_XAxis.m_MaxSpeed = 0;
        secondgrid.SetActive(true);
        Invoke("dealdmg", 3.5f);
    }
    public void fail()
    {
        CancelInvoke();
        Invoke("dealdmg", 0.4f);
    }
    private void dealdmg()
    {
        Statics.otheraction = false;
        Statics.dash = false;
        Statics.enemyspezialtimescale = false;
        Time.timeScale = Statics.normalgamespeed;
        Time.fixedDeltaTime = Statics.normaltimedelta;
        maincam.m_YAxis.m_MaxSpeed = 0.008f;
        maincam.m_XAxis.m_MaxSpeed = 0.6f;
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
        dmgcount = 5 - memoryclicknumber;
        LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(dmgcount * basedmg);
        secondgrid.SetActive(false);
        gameObject.SetActive(false);
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
        maincam.m_YAxis.m_MaxSpeed = 0.008f;
        maincam.m_XAxis.m_MaxSpeed = 0.6f;
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
        CancelInvoke();
        secondgrid.SetActive(false);
        gameObject.SetActive(false);
    }
}
