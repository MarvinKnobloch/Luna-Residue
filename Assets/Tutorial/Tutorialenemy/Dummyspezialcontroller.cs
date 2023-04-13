using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummyspezialcontroller : MonoBehaviour
{
    [SerializeField] private GameObject dummysphere;
    [SerializeField] private float spheredmg;
    [SerializeField] private int buttonpressesneed;
    [SerializeField] private float timetopressbuttons;

    [SerializeField] private GameObject gate;
    private Vector3 gatestartposi;
    private Vector3 gateendposi;
    private float movetime = 6f;
    private float movetimer;

    [SerializeField] private EnemyHP enemyHP;

    private void Awake()
    {
        dummysphere.GetComponent<Dummysphere>().basedmg = spheredmg;
        dummysphere.GetComponent<Dummysphere>().explodetime = timetopressbuttons;
        gatestartposi = gate.transform.position;
        gateendposi = gatestartposi + new Vector3(0, -10, 0);

    }
    private void OnEnable()
    {
        Statics.currentenemyspecialcd = 7;
        dummysphere.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        dummysphere.SetActive(true);
        if (Statics.dash == false)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtobuttonmashstun(buttonpressesneed);
        }
    }

    public IEnumerator opengate()
    {
        while (true)
        {
            movetimer += Time.deltaTime;
            float gateopenpercantage = movetimer / movetime;
            gate.transform.position = Vector3.Lerp(gatestartposi, gateendposi, gateopenpercantage);

            if (movetimer >= movetime)
            {
                movetimer = 0;
                StopCoroutine("opengate");
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }
    public IEnumerator killenemy()
    {
        yield return new WaitForSeconds(2);
        enemyHP.takesupportdmg(11000);
    }
}
