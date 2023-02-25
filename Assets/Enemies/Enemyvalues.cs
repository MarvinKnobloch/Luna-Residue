using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemys/ New Enemy")]
public class Enemyvalues : ScriptableObject
{
    public string enemyname;
    public float basehealth;
    public int enemylvl;
    public float movementspeed;
    public float basedmg;
    public float attackspeed;
    public int enemysize;
    public GameObject gold;
    public int golddropamount;
    public int expgain;
}
