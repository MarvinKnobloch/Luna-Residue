using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startquest : MonoBehaviour
{
    [SerializeField] private Areacontroller areacontroller;
    public Quests[] quest;

    public void startquest()
    {
        for (int i = 0; i < quest.Length; i++)
        {
            if (quest[i].questactiv == false)
            {
                quest[i].questactiv = true;
                LoadCharmanager.autosave();
            }
        }
    }
}
