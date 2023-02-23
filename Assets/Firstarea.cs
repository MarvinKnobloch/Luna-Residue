using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Firstarea : MonoBehaviour
{
    private Areacontroller areacontroller;

    public bool laserpuzzlecomplete;
    public bool boxpuzzlecomplete;
    public bool attacktutorialcomplete;
    public bool healtutorialcomplete;
    private void Awake()
    {
        areacontroller = GetComponent<Areacontroller>();
        areacontroller.areatosave = this;
        loadmonobehaviour(Statics.currentgameslot, areacontroller.areaname, this);
    }
    private void loadmonobehaviour(int slot, string filename, MonoBehaviour monobehaviour)
    {
        var filePath = Path.Combine(Application.persistentDataPath, filename + slot + ".json");
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(json, monobehaviour);
        }
    }
}
