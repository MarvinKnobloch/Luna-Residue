using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screenmodecontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons;

    [SerializeField] private Color selectedcolor;
    [SerializeField] private Color notselectedcolor;

    private void OnEnable()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Image>().color = notselectedcolor;
        }
        buttons[Statics.currentscreenmode].GetComponent<Image>().color = selectedcolor;
    }

    public void fullscreenmode()
    {
        if(Statics.currentscreenmode != 0)
        {
            buttons[Statics.currentscreenmode].GetComponent<Image>().color = notselectedcolor;
            Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
            Statics.currentscreenmode = 0;
            buttons[Statics.currentscreenmode].GetComponent<Image>().color = selectedcolor;
        }
    }
    public void borderlesswindowmode()
    {
        if (Statics.currentscreenmode != 1)
        {
            buttons[Statics.currentscreenmode].GetComponent<Image>().color = notselectedcolor;
            Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
            Statics.currentscreenmode = 1;
            buttons[Statics.currentscreenmode].GetComponent<Image>().color = selectedcolor;
        }
    }
    public void windowmode()
    {
        if (Statics.currentscreenmode != 2)
        {
            buttons[Statics.currentscreenmode].GetComponent<Image>().color = notselectedcolor;
            Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
            Statics.currentscreenmode = 2;
            buttons[Statics.currentscreenmode].GetComponent<Image>().color = selectedcolor;
        }
    }
}
