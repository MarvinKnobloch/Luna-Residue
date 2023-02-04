using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setstaticsnull : MonoBehaviour
{
    [SerializeField] private Itemcontroller nullobject;

    private void Awake()
    {
        for (int i = 0; i < Statics.charcurrenthead.Length; i++)
        {
            if (Statics.charcurrenthead[i] == null)
            {
                Statics.charcurrenthead[i] = nullobject;
            }
        }
        for (int i = 0; i < Statics.charcurrentchest.Length; i++)
        {
            if (Statics.charcurrentchest[i] == null)
            {
                Statics.charcurrentchest[i] = nullobject;
            }
        }
        for (int i = 0; i < Statics.charcurrentgloves.Length; i++)
        {
            if (Statics.charcurrentgloves[i] == null)
            {
                Statics.charcurrentgloves[i] = nullobject;
            }
        }
        for (int i = 0; i < Statics.charcurrentlegs.Length; i++)
        {
            if (Statics.charcurrentlegs[i] == null)
            {
                Statics.charcurrentlegs[i] = nullobject;
            }
        }
        for (int i = 0; i < Statics.charcurrentshoes.Length; i++)
        {
            if (Statics.charcurrentshoes[i] == null)
            {
                Statics.charcurrentshoes[i] = nullobject;
            }
        }
        for (int i = 0; i < Statics.charcurrentneckless.Length; i++)
        {
            if (Statics.charcurrentneckless[i] == null)
            {
                Statics.charcurrentneckless[i] = nullobject;
            }
        }
        for (int i = 0; i < Statics.charcurrentring.Length; i++)
        {
            if (Statics.charcurrentring[i] == null)
            {
                Statics.charcurrentring[i] = nullobject;
            }
        }
    }
}
