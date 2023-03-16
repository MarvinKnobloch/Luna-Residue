using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Healingscript : MonoBehaviour
{
    private SpielerSteu controlls;
    private Animator animator;
    private Attributecontroller attributecontroller;
    private Playerhp playerhp;

    public GameObject healanzeige;
    private Movescript movementscript;

    private bool readinputs;
    public InputAction[] hotkeys;
    public Text[] buttons;
    private string[] healhotkeys = new string[4];
    public Image[] buttonimage;
    public int[] randomkeys;
    private int currentinput;


    public int currentcombo;
    public bool strgwaspressed;

    private float failinputcd = 0.5f;
    private bool failinputbool;
    private float failinputtime;

    private int healtarget;
    private bool healcanceled;

    public bool singlehealcast;
    private int singlehealbuttonamount = 2;
    private float singleheal = 24;
    private float finalsingleheal;

    public bool grouphealcast;
    private int grouphealbuttonamount = 4;
    private float groupheal = 14;
    private float finalgroupheal;

    public bool resurrectioncast;
    private int resurrectionbuttonamount = 6;

    const string healloop = "Healloop";
    const string singlehealend = "Singlehealend";
    const string grouphealend = "Grouphealend";
    const string resurrectstate = "Resurrect";

    private void Awake()
    {
        movementscript = GetComponent<Movescript>();
        controlls = Keybindinputmanager.inputActions;
        animator = GetComponent<Animator>();
        attributecontroller = GetComponent<Attributecontroller>();
        playerhp = GetComponent<Playerhp>();
    }

    private void Start()
    {
        healhotkeys[0] = controlls.SpielerHeal.Randomhotkey1.GetBindingDisplayString();
        healhotkeys[1] = controlls.SpielerHeal.Randomhotkey2.GetBindingDisplayString();
        healhotkeys[2] = controlls.SpielerHeal.Randomhotkey3.GetBindingDisplayString();
        healhotkeys[3] = controlls.SpielerHeal.Randomhotkey4.GetBindingDisplayString();
        hotkeys[0] = controlls.SpielerHeal.Randomhotkey1;
        hotkeys[1] = controlls.SpielerHeal.Randomhotkey2;
        hotkeys[2] = controlls.SpielerHeal.Randomhotkey3;
        hotkeys[3] = controlls.SpielerHeal.Randomhotkey4;
    }
    private void OnEnable()
    {
        controlls.Enable();
        failinputbool = false;
        {
            if (healanzeige.activeSelf == true)
            {
                healanzeige.SetActive(false);
            }
        }
        StartCoroutine("healupdate");
    }
    IEnumerator healupdate()
    {
        yield return null;
        finalsingleheal = Globalplayercalculations.calculatecasthealing(singleheal, playerhp.maxhealth, attributecontroller.stoneclassbonusheal);
        finalgroupheal = Globalplayercalculations.calculatecasthealing(groupheal, playerhp.maxhealth, attributecontroller.stoneclassbonusheal);
    }
    public void heal()
    {
        if (Statics.healcdbool == false && LoadCharmanager.disableattackbuttons == false)
        {
            if (readinputs == true)
            {
                if (Input.anyKeyDown && failinputbool == false)
                {
                    if (hotkeys[currentinput].WasPressedThisFrame())
                    {
                        correcthotkey();
                    }
                    else
                    {
                        failinputbool = true;
                        StartCoroutine("wrongcd");
                    }
                }
                if (singlehealcast == true && resurrectioncast == false)
                {
                    if (controlls.SpielerHeal.Target1.WasPerformedThisFrame())
                    {
                        healtarget = 1;
                        movementscript.ChangeAnimationState(singlehealend);
                    }
                    if (controlls.SpielerHeal.Target2.WasPerformedThisFrame() && LoadCharmanager.Overallthirdchar != null)
                    {
                        healtarget = 2;
                        movementscript.ChangeAnimationState(singlehealend);
                    }
                    if (controlls.SpielerHeal.Target3.WasPerformedThisFrame() && LoadCharmanager.Overallforthchar != null)
                    {
                        healtarget = 3;
                        movementscript.ChangeAnimationState(singlehealend);
                    }             
                }
                if (grouphealcast == true)
                {
                    if (controlls.SpielerHeal.Groupheal.WasPerformedThisFrame())
                    {
                        movementscript.ChangeAnimationState(grouphealend);
                    }
                }
                if (resurrectioncast == true)
                {
                    if (controlls.SpielerHeal.Target2.WasPerformedThisFrame() && LoadCharmanager.Overallthirdchar != null)
                    {
                        healtarget = 2;
                        movementscript.ChangeAnimationState(resurrectstate);
                    }
                    if (controlls.SpielerHeal.Target3.WasPerformedThisFrame() && LoadCharmanager.Overallforthchar != null)
                    {
                        healtarget = 3;
                        movementscript.ChangeAnimationState(resurrectstate);
                    }
                }
            }
            if (controlls.Player.Heal.WasReleasedThisFrame() && strgwaspressed == true)
            {
                cancelhealing();
                movementscript.switchtogroundstate();
            }
            if (controlls.Player.Jump.WasPerformedThisFrame())
            {
                cancelhealing();
                movementscript.playerair.jump();
            }
        }
    }

    private void cancelhealing()
    {
        if (healanzeige.activeSelf == true)
        {
            Statics.otheraction = false;
        }
        resethealvalues();
    }
    public void resethealvalues()
    {
        strgwaspressed = false;
        readinputs = false;
        currentcombo = 0;
        healanzeige.SetActive(false);
        healcanceled = true;
        singlehealcast = false;
        grouphealcast = false;
        resurrectioncast = false;
    }

    public void strgpressed()
    {
        strgwaspressed = true;
        readinputs = true;
        healanzeige.SetActive(true);
        healcanceled = false;
        Statics.otheraction = true;
        buttons[0].color = Color.white;
        buttons[1].color = Color.white;
        buttons[2].color = Color.white;
        buttonimage[0].fillAmount = 1;
        buttonimage[1].fillAmount = 1;
        buttonimage[2].fillAmount = 1;
        randomkeys[0] = Random.Range(0, hotkeys.Length);                           // bei 1, 4 geht es nur von 1 bis 3, wieso auch immer
        randomkeys[1] = Random.Range(0, hotkeys.Length);
        randomkeys[2] = Random.Range(0, hotkeys.Length);
        buttons[0].text = healhotkeys[randomkeys[0]];
        buttons[1].text = healhotkeys[randomkeys[1]];
        buttons[2].text = healhotkeys[randomkeys[2]];
        if (attributecontroller.ishealerclassroll == true)
        {
            buttonimage[3].gameObject.SetActive(true);
            buttonimage[4].gameObject.SetActive(true);
            buttonimage[5].gameObject.SetActive(true);
            buttonimage[6].gameObject.SetActive(true);

            buttons[3].color = Color.white;
            buttons[4].color = Color.white;
            buttons[5].color = Color.white;
            buttons[6].color = Color.white;
            buttonimage[3].fillAmount = 1;
            buttonimage[4].fillAmount = 1;
            buttonimage[5].fillAmount = 1;
            buttonimage[6].fillAmount = 1;
            randomkeys[3] = Random.Range(0, hotkeys.Length);
            randomkeys[4] = Random.Range(0, hotkeys.Length);
            randomkeys[5] = Random.Range(0, hotkeys.Length);
            randomkeys[6] = Random.Range(0, hotkeys.Length);
            buttons[3].text = healhotkeys[randomkeys[3]];
            buttons[4].text = healhotkeys[randomkeys[4]];
            buttons[5].text = healhotkeys[randomkeys[5]];
            buttons[6].text = healhotkeys[randomkeys[6]];
        }
        else
        {
            buttonimage[3].gameObject.SetActive(false);
            buttonimage[4].gameObject.SetActive(false);
            buttonimage[5].gameObject.SetActive(false);
            buttonimage[6].gameObject.SetActive(false);
        }
        currentcombo = 0;
        currentinput = randomkeys[0];
    }
    public void correcthotkey()
    {
        if (currentcombo == singlehealbuttonamount)
        {
            singlehealcast = true;
        }
        if(attributecontroller.ishealerclassroll == true)
        {
            if (currentcombo == grouphealbuttonamount)
            {
                grouphealcast = true;
            }
            if (currentcombo == resurrectionbuttonamount)
            {
                resurrectioncast = true;
            }
            buttons[currentcombo].color = Color.green;
            if (currentcombo < resurrectionbuttonamount)
            {
                currentcombo++;
                currentinput = randomkeys[currentcombo];
            }
        }
        else
        {
            buttons[currentcombo].color = Color.green;
            if (currentcombo < singlehealbuttonamount)
            {
                currentcombo++;
                currentinput = randomkeys[currentcombo];
            }
        }
    }
    public void healidle()   //heal animation
    {
        if (healcanceled == false && Statics.dash == false)            //animationwechsel die von der animation getriggert werden, und keine requirement haben, können den dash buggen
        {
            movementscript.ChangeAnimationStateInstant(healloop);
        }
    }
    private void resetvaluesafterheal(int resurrectcd)
    {
        currentcombo = 0;
        singlehealcast = false;
        grouphealcast = false;
        resurrectioncast = false;
        strgwaspressed = false;
        readinputs = false;
        healanzeige.SetActive(false);
        Statics.otheraction = false;
        GlobalCD.starthealingcd(resurrectcd);
        movementscript.state = Movescript.State.Ground;
    }

    private void castsingleheal()
    {
        if(healtarget == 1) LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().addhealthwithtext(finalsingleheal);
        else if(healtarget == 2) LoadCharmanager.Overallthirdchar.GetComponent<Playerhp>().addhealthwithtext(finalsingleheal);
        else if (healtarget == 3) LoadCharmanager.Overallforthchar.GetComponent<Playerhp>().addhealthwithtext(finalsingleheal);
        healtarget = 0;
        resetvaluesafterheal(0);
    }

    private void castgroupheal()
    {
        LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().addhealthwithtext(finalgroupheal);
        if (LoadCharmanager.Overallthirdchar != null)
        {
            LoadCharmanager.Overallthirdchar.GetComponent<Playerhp>().addhealthwithtext(finalgroupheal);
        }
        if (LoadCharmanager.Overallforthchar != null)
        {
            LoadCharmanager.Overallforthchar.GetComponent<Playerhp>().addhealthwithtext(finalgroupheal);
        }
        resetvaluesafterheal(0);
    }
    private void castresurrection()
    {
        if (healtarget == 2) resurrect(LoadCharmanager.Overallthirdchar.gameObject);
        else if(healtarget == 3) resurrect(LoadCharmanager.Overallforthchar.gameObject);
        healtarget = 0;
        if(Statics.infight == true)
        {
            resetvaluesafterheal(Statics.infightresurrectcd);
            Statics.infightresurrectcd++;
        }
        else resetvaluesafterheal(0);
    }
    private void resurrect(GameObject character)
    {
        if (character.TryGetComponent(out Playerhp playerhp))
        {
            if (playerhp.playerisdead == true)
            {
                playerhp.playerisresurrected();
            }
            else
            {
                float healamount = Mathf.Round(playerhp.maxhealth * (0.2f + (Statics.groupstonehealbonus * 0.01f)));
                playerhp.addhealthwithtext(healamount);
            }
        }
    }
    IEnumerator wrongcd()
    {
        buttonimage[currentcombo].fillAmount = 0;
        failinputtime = 0;

        while(true)
        {
            failinputtime += Time.deltaTime;
            buttonimage[currentcombo].fillAmount = failinputtime / failinputcd;

            if(failinputtime >= failinputcd)
            {
                failinputbool = false;
                StopCoroutine("wrongcd");
            }
            yield return null;
        }     
    }
}
