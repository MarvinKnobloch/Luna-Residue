using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setstaticsnull : MonoBehaviour
{
    [SerializeField] private Itemcontroller nullobject;

    private void Awake()
    {
        setnullobject(Statics.charcurrenthead);
        setnullobject(Statics.charcurrentchest);
        setnullobject(Statics.charcurrentgloves);
        setnullobject(Statics.charcurrentlegs);
        setnullobject(Statics.charcurrentshoes);
        setnullobject(Statics.charcurrentneckless);
        setnullobject(Statics.charcurrentring);
    }
    private void setnullobject(Itemcontroller[] staticslot)
    {
        for (int i = 0; i < staticslot.Length; i++)
        {
            if (staticslot[i] == null)
            {
                staticslot[i] = nullobject;
            }
        }
    }
}
