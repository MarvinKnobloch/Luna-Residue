using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatebarcollider : MonoBehaviour
{
    private EnemyHP enemyhpscript;
    private void Awake()
    {
        enemyhpscript = GetComponentInParent<EnemyHP>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            enemyhpscript.addtocanvas();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            enemyhpscript.removefromcanvas();
        }
    }
}
