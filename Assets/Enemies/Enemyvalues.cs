using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemys/ New Enemy")]
public class Enemyvalues : ScriptableObject
{
    public string enemyname;
    public float maxhealth;
    public int enemylvl;
    public float movementspeed;
    public float attackdmg;
    public float attackspeed;
    public int enemysize;
}
