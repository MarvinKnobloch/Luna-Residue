using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endinactivquest : MonoBehaviour
{
    [SerializeField] private Areacontroller areacontroller;
    public Quests[] quest;
    public void endquest()
    {
        for (int i = 0; i < quest.Length; i++)
        {
            if (quest[i].questcomplete == false)
            {
                quest[i].questactiv = true;
                quest[i].questcomplete = true;
                areacontroller.autosave();
            }
        }
    }
}
