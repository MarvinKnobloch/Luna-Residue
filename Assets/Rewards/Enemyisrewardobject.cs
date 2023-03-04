using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyisrewardobject : MonoBehaviour
{
    [SerializeField] private GameObject reward;

    public void checkforrewardcondition()
    {
        reward.GetComponent<Rewardinterface>().addrewardcount();
    }
}
