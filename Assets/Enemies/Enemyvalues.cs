using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemys/ New Enemy")]
public class Enemyvalues : ScriptableObject
{
    public string enemyname;
    public float basehealth;
    public int enemylvl;
    public float movementspeed;                  //movementspeed wird gerade in enemymovement durch die enemysize bestimmt
    public float basedmg;
    public float attackspeed;
    public int enemysize;
    public GameObject gold;
    public int golddropamount;
    public int expgain;
    public Enemydrops[] enemydrops;
}
[Serializable]
public class Enemydrops
{
    [SerializeField] public GameObject itemtodrop;
    [SerializeField] public int itemdropchance;
}
