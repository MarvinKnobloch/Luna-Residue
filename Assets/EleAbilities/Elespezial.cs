using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elespezial : MonoBehaviour
{
    public static Elespezial instance;

    private SpielerSteu controlls;

    private Manamanager manacontroller;

    public int currentelement;
    public int newelement;
    public int combospell;

    private Color spezialbackgroundcolor;
    [SerializeField] private Text spezialtext;
    [SerializeField] private Image spezialbackground;

    private float spezialmanacosts;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        controlls = Keybindinputmanager.inputActions;
        manacontroller = GetComponent<Manamanager>();
        newelement = 0;
        currentelement = 0;
        combospell = 0;
    }
    private void OnEnable()
    {
        controlls.Enable();
    }
    private void Update()
    {
        if (Statics.otheraction == false && Statics.infight == true)
        {
            if (Manamanager.mana >= spezialmanacosts)
            {
                if (controlls.Player.Spezial.WasPerformedThisFrame())
                {
                    choosecombospell(combospell);
                    manacontroller.Managemana(spezialmanacosts);
                }
            }
        }
    }
    private void setspezialnull()
    {
        spezialtext.text = "fail";
        combospell = 0;
    }
    private void firestorm()
    {
        Debug.Log("firestorm");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setfirestorm()
    {
        spezialtext.text = "Firestorm";
        combospell = 1;
    }
    private void burn()
    {
        Debug.Log("burn");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setburn()
    {
        spezialtext.text = "Burn";
        combospell = 2;
    }
    private void waterdome()
    {
        Debug.Log("waterdome");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setwaterdome()
    {
        spezialtext.text = "Waterdome";
        combospell = 3;
    }
    private void geysir()
    {
        Debug.Log("geysir");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setgeysir()
    {
        spezialtext.text = "Geysir";
        combospell = 4;
    }
    private void thorntendril()
    {
        Debug.Log("thorntendril");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setthrontendril()
    {
        spezialtext.text = "Thorntendril";
        combospell = 5;
    }
    private void posioncloud()
    {
        Debug.Log("posioncloud");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setposioncloud()
    {
        spezialtext.text = "poisoncload";
        combospell = 6;
    }
    private void icerain()
    {
        Debug.Log("icerain");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void seticerain()
    {
        spezialtext.text = "Icerain";
        combospell = 7;
    }
    private void icegravity()
    {
        Debug.Log("icegravity");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void seticegravity()
    {
        spezialtext.text = "Icegravity";
        combospell = 8;
    }
    private void sunflair()
    {
        Debug.Log("sunflair");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setsunflair()
    {
        spezialtext.text = "Sunflair";
        combospell = 9;
    }
    private void icelightballs()
    {
        Debug.Log("icelightballs");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void seticelightballs()
    {
        spezialtext.text = "Icelightballs";
        combospell = 10;
    }
    private void lightning()
    {
        Debug.Log("lightning");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setlightning()
    {
        spezialtext.text = "Lightning";
        combospell = 11;
    }
    private void darkmateriabeam()
    {
        Debug.Log("darkmateriabeam");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setdarkmateriabeam()
    {
        spezialtext.text = "Darkmateriabeam";
        combospell = 12;
    }
    private void blackhole()
    {
        Debug.Log("blackhole");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setblackhole()
    {
        spezialtext.text = "Blackhole";
        combospell = 13;
    }
    private void explosion()
    {
        Debug.Log("explosion");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setexplosion()
    {
        spezialtext.text = "Explosion";
        combospell = 14;
    }
    private void gainthammer()
    {
        Debug.Log("gainthammer");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setgainthammer()
    {
        spezialtext.text = "Gainthammer";
        combospell = 15;
    }
    private void meteor()
    {
        Debug.Log("meteor");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setmeteor()
    {
        spezialtext.text = "Meteor";
        combospell = 16;
    }
    public void checkfirestate(int newvalue)
    {
        ColorUtility.TryParseHtmlString("#A41D1D", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setspezialnull();
                break;
            case 2:
                setspezialnull();
                break;
            case 3:
                setburn();
                break;
            case 4:
                setspezialnull();
                break;
            case 5:
                setspezialnull();
                break;
            case 6:
                setfirestorm();
                break;
            case 7:
                setexplosion();
                break;
            case 8:
                setmeteor();
                break;
        }
    }
    public void checkwaterstate(int newvalue)
    {
        ColorUtility.TryParseHtmlString("#1A19C5", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setspezialnull();
                break;
            case 2:
                setspezialnull();
                break;
            case 3:
                setposioncloud();
                break;
            case 4:
                seticerain();
                break;
            case 5:
                setspezialnull();
                break;
            case 6:
                setwaterdome();
                break;
            case 7:
                setspezialnull();
                break;
            case 8:
                setgeysir();
                break;
        }
    }
    public void checknaturestate(int newvalue)
    {
        ColorUtility.TryParseHtmlString("#1D9028", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setburn();
                break;
            case 2:
                setposioncloud();
                break;
            case 3:
                setspezialnull();
                break;
            case 4:
                setspezialnull();
                break;
            case 5:
                setsunflair();
                break;
            case 6:
                setspezialnull();
                break;
            case 7:
                setspezialnull();
                break;
            case 8:
                setthrontendril();
                break;
        }
    }
    public void checkicestate(int newvalue)
    {
        ColorUtility.TryParseHtmlString("#219C93", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setspezialnull();
                break;
            case 2:
                seticerain();
                break;
            case 3:
                setspezialnull();
                break;
            case 4:
                setspezialnull();
                break;
            case 5:
                seticelightballs();
                break;
            case 6:
                setspezialnull();
                break;
            case 7:
                seticegravity();
                break;
            case 8:
                setgainthammer();
                break;
        }
    }
    public void checklightstate(int newvalue)
    {
        ColorUtility.TryParseHtmlString("#F3C200", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setspezialnull();
                break;
            case 2:
                setspezialnull();
                break;
            case 3:
                setsunflair();
                break;
            case 4:
                seticelightballs();
                break;
            case 5:
                setspezialnull();
                break;
            case 6:
                setlightning();
                break;
            case 7:
                setblackhole();
                break;
            case 8:
                setspezialnull();
                break;
        }
    }
    public void checkstormstate(int newvalue)
    {
        ColorUtility.TryParseHtmlString("#5F138E", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setfirestorm();
                break;
            case 2:
                setwaterdome();
                break;
            case 3:
                setspezialnull();
                break;
            case 4:
                setspezialnull();
                break;
            case 5:
                setlightning();
                break;
            case 6:
                setspezialnull();
                break;
            case 7:
                setdarkmateriabeam();
                break;
            case 8:
                setspezialnull();
                break;
        }
    }
    public void checkdarkstate(int newvalue)
    {
        ColorUtility.TryParseHtmlString("#1D1414", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setexplosion();
                break;
            case 2:
                setspezialnull();
                break;
            case 3:
                setspezialnull();
                break;
            case 4:
                seticegravity();
                break;
            case 5:
                setblackhole();
                break;
            case 6:
                setdarkmateriabeam();
                break;
            case 7:
                setspezialnull();
                break;
            case 8:
                setspezialnull();
                break;
        }
    }
    public void checkearthstate(int newvalue)
    {
        ColorUtility.TryParseHtmlString("#4B2E11", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setmeteor();
                break;
            case 2:
                setgeysir();
                break;
            case 3:
                thorntendril();
                break;
            case 4:
                setgainthammer();
                break;
            case 5:
                setspezialnull();
                break;
            case 6:
                setspezialnull();
                break;
            case 7:
                setspezialnull();
                break;
            case 8:
                setspezialnull();
                break;
        }
    }
    void choosecombospell(int number)
    {
        switch (number)
        {
            case 0:
                break;
            case 1:
                firestorm();
                break;
            case 2:
                burn();
                break;
            case 3:
                waterdome();
                break;
            case 4:
                geysir();
                break;
            case 5:
                thorntendril();
                break;
            case 6:
                posioncloud();
                break;
            case 7:
                icerain();
                break;
            case 8:
                icegravity();
                break;
            case 9:
                sunflair();
                break;
            case 10:
                icelightballs();
                break;
            case 11:
                lightning();
                break;
            case 12:
                darkmateriabeam();
                break;
            case 13:
                blackhole();
                break;
            case 14:
                explosion();
                break;
            case 15:
                gainthammer();
                break;
            case 16:
                meteor();
                break;
        }
    }
}
