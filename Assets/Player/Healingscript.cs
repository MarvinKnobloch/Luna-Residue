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

    public GameObject healanzeige;
    private Movescript movementscript;

    private bool readinputs;
    public InputAction[] hotkeys;
    public Text[] buttons;
    public Text[] WASD;
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
    private float singleheal = 20;

    public bool grouphealcast;
    private int grouphealbuttonamount = 4;
    private float groupheal = 12;

    public bool resurrectioncast;
    private int resurrectionbuttonamount = 6;

    const string healstart = "Healstart";
    const string healloop = "Healloop";
    const string singlehealend = "Singlehealend";
    const string grouphealend = "Grouphealend";

    private void Awake()
    {
        movementscript = GetComponent<Movescript>();
        controlls = Keybindinputmanager.inputActions;
        animator = GetComponent<Animator>();
        attributecontroller = GetComponent<Attributecontroller>();
    }

    private void Start()
    {
        WASD[0].text = controlls.SpielerHeal.Randomhotkey1.GetBindingDisplayString();
        WASD[1].text = controlls.SpielerHeal.Randomhotkey2.GetBindingDisplayString();
        WASD[2].text = controlls.SpielerHeal.Randomhotkey3.GetBindingDisplayString();
        WASD[3].text = controlls.SpielerHeal.Randomhotkey4.GetBindingDisplayString();
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
    }
    public void heal()
    {
        if (Statics.healcdbool == false && LoadCharmanager.disableattackbuttons == false)
        {
            //if (movementscript.amBoden == true)
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
                    if (controlls.SpielerHeal.Target1.WasPerformedThisFrame())
                    {
                        healtarget = 1;
                        Debug.Log("res1");
                    }
                    if (controlls.SpielerHeal.Target2.WasPerformedThisFrame() && LoadCharmanager.Overallthirdchar != null)
                    {
                        healtarget = 2;
                        Debug.Log("res2");
                    }
                    if (controlls.SpielerHeal.Target3.WasPerformedThisFrame() && LoadCharmanager.Overallforthchar != null)
                    {
                        healtarget = 3;
                        Debug.Log("res3");
                    }
                }
            }
            if (controlls.Player.Heal.WasReleasedThisFrame() && strgwaspressed == true) //&& animator.GetCurrentAnimatorStateInfo(0).IsName("Healend") == false)
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
        buttons[0].text = WASD[randomkeys[0]].text;
        buttons[1].text = WASD[randomkeys[1]].text;
        buttons[2].text = WASD[randomkeys[2]].text;
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
            buttons[3].text = WASD[randomkeys[3]].text;
            buttons[4].text = WASD[randomkeys[4]].text;
            buttons[5].text = WASD[randomkeys[5]].text;
            buttons[6].text = WASD[randomkeys[6]].text;
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
    public void castsingleheal()
    {
        currentcombo = 0;
        singlehealcast = false;
        grouphealcast = false;
        resurrectioncast = false;
        strgwaspressed = false;
        readinputs = false;
        healanzeige.SetActive(false);
        Statics.otheraction = false;
        //float basicheal = singleheal + Statics.charcurrentlvl;
        float healamount = Mathf.Round(singleheal + (Statics.groupstonehealbonus + GetComponent<Attributecontroller>().stoneclassbonusheal) * 0.01f * singleheal * Statics.charcurrentlvl);
        if(healtarget == 1)
        {
            LoadCharmanager.Overallmainchar.GetComponent<SpielerHP>().castheal(healamount);
        }
        if(healtarget == 2)
        {
            LoadCharmanager.Overallthirdchar.GetComponent<SpielerHP>().castheal(healamount);
        }
        if (healtarget == 3)
        {
            LoadCharmanager.Overallforthchar.GetComponent<SpielerHP>().castheal(healamount);
        }
        healtarget = 0;
        GlobalCD.starthealingcd();
        movementscript.state = Movescript.State.Ground;
    }

    private void castgroupheal()
    {
        currentcombo = 0;
        singlehealcast = false;
        grouphealcast = false;
        resurrectioncast = false;
        strgwaspressed = false;
        readinputs = false;
        healanzeige.SetActive(false);
        Statics.otheraction = false;
        float healamount = Mathf.Round(groupheal + (Statics.groupstonehealbonus + GetComponent<Attributecontroller>().stoneclassbonusheal) * 0.01f * groupheal * Statics.charcurrentlvl);
        LoadCharmanager.Overallmainchar.GetComponent<SpielerHP>().castheal(healamount);
        if (LoadCharmanager.Overallthirdchar != null)
        {
            LoadCharmanager.Overallthirdchar.GetComponent<SpielerHP>().castheal(healamount);
        }
        if (LoadCharmanager.Overallforthchar != null)
        {
            LoadCharmanager.Overallforthchar.GetComponent<SpielerHP>().castheal(healamount);
        }
        GlobalCD.starthealingcd();
        movementscript.state = Movescript.State.Ground;
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

//float basicheal = groupheal + Statics.charcurrentlvl;
//float healamount = Mathf.Round(basicheal * (GetComponent<Attributecontroller>().overallstonehealbonus * 0.01f * basicheal));
