using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;


public class Keybindinputmanager : MonoBehaviour
{
    public static SpielerSteu inputActions;

    public static event Action keyrebindfinished;                                    //für udatebinding UI
    public static event Action keyrebindcanceled;                                    //für udatebinding UI
    public static event Action disablecantclicklayer;
    //public static event Action<InputAction, int> keyrebindstarted;

    public static Text keyrebindtext;
    private void Start()
    {
        if (inputActions == null)
        {
            inputActions = new SpielerSteu();
        }
    }

    public static void startrebind(string actionname, int bindingindex, Text textwhilerebind)
    {
        InputAction action = inputActions.asset.FindAction(actionname);
        if (action == null || action.bindings.Count <= bindingindex)
        {
            disablecantclicklayer?.Invoke();
            Debug.Log("action nicht vorhanden");
            return;
        }
        if (action.bindings[bindingindex].isComposite)                                          // Composite = W/A/S/D mehrere hotkeys die gebindet werden müssen
        {
            var firstpartindex = bindingindex + 1;
            if (firstpartindex < action.bindings.Count && action.bindings[firstpartindex].isPartOfComposite)
            {
                dorebind(action, firstpartindex, textwhilerebind, true);
            }
        }
        else
        {
            dorebind(action, bindingindex, textwhilerebind, false);
        }

    }
    private static void dorebind(InputAction actiontorebind, int bindingindex, Text textwhilerebind, bool isacomposite)
    {
        if (actiontorebind == null || bindingindex < 0)
            return;
        textwhilerebind.color = Color.red;
        textwhilerebind.text = "Choose button";                          // "Press a {actiontorebind.expectedControlType}";
        actiontorebind.Disable();                                       // der Hotkey wird disabled, wieso auch immer

        var rebind = actiontorebind.PerformInteractiveRebinding(bindingindex);              //ist eine Inputsystem function, startet den rebind prozess

        //rebind.WithExpectedControlType("axis");
        rebind.OnComplete(functionisruning =>                                             // wegen memoryleak
        {
            actiontorebind.Enable();
            functionisruning.Dispose();                                                   //wegen memoryleak

            /*if (isacomposite == true)
            {
                var nextbindingindex = bindingindex + 1;
                if(nextbindingindex < actiontorebind.bindings.Count && actiontorebind.bindings[nextbindingindex].isPartOfComposite)
                {
                    dorebind(actiontorebind, nextbindingindex, textwhilerebind, isacomposite);
                }
            }*/

            InputBinding newbinding = actiontorebind.bindings[bindingindex];
            foreach (InputBinding binding in actiontorebind.actionMap.bindings)
            {
                if (binding.action == newbinding.action)
                {
                    inputActions.asset.FindBinding(binding, out InputAction action);
                    int newbindingindex = action.GetBindingIndex(binding);
                    inputActions.asset.FindBinding(newbinding, out InputAction oldaction);
                    int oldbindingindex = oldaction.GetBindingIndex(newbinding);
                    if (newbindingindex == oldbindingindex)
                    {
                        continue;
                    }
                }
                if (newbinding.effectivePath == binding.effectivePath)
                {
                    string overwritenbinding = binding.action;
                    if (binding.isPartOfComposite)
                    {
                        inputActions.asset.FindBinding(binding, out InputAction newaction);
                        int newbindingindex = newaction.GetBindingIndex(binding);
                        newaction.ApplyBindingOverride(newbindingindex, "");
                        savebindingsoverride(newaction);

                    }
                    else
                    {
                        bindingindex = 0;
                        InputAction newaction = inputActions.asset.FindAction(overwritenbinding);
                        newaction.ApplyBindingOverride(0, "");
                        savebindingsoverride(newaction);
                    }
                }
            }
            savebindingsoverride(actiontorebind);
            keyrebindfinished?.Invoke();
            disablecantclicklayer?.Invoke();
        });

        rebind.OnCancel(functionisruning =>                                             // wegen memoryleak
        {
            actiontorebind.Enable();
            functionisruning.Dispose();                                                   //löscht alles was in der Funktion passiert damit keine speicherfehler im hintergrund entstehen

            keyrebindcanceled?.Invoke();
            disablecantclicklayer?.Invoke();
        });

        rebind.WithCancelingThrough("<Keyboard>/escape");

        //keyrebindstarted?.Invoke(actiontorebind, bindingindex);
        rebind.Start();                                                               // startet die funktion rebind/macht das die rebind funktion ständig durchläuft
    }
    public static string getbindingname(string actionname, int bindingindex)                           // für textupdate in spielmodus
    {
        if (inputActions == null)
        {
            inputActions = new SpielerSteu();
        }
        InputAction action = inputActions.asset.FindAction(actionname);
        return action.GetBindingDisplayString(bindingindex);
    }
    /*private static void oldsavebindingoverride(InputAction action)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
    }*/
    private static void savebindingsoverride(InputAction action)
    {
        var rebinds = action.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(action.name, rebinds);
    }
    public static void loadbindingsoverride(string actionname)
    {
        //Debug.Log(actionname.ToString());
        InputAction action = inputActions.asset.FindAction(actionname);
        var rebinds = PlayerPrefs.GetString(actionname);
        action.LoadBindingOverridesFromJson(rebinds);
    }
    /*public static void oldloadbindingoverride(string actionname)
    {
        if (inputActions == null)
        {
            inputActions = new SpielerSteu();
        }
        InputAction action = inputActions.asset.FindAction(actionname);

        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
            {
                action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
            }
        }
    }*/
    public static void resetbinding(string actionname, int bindingindex)
    {
        InputAction action = inputActions.asset.FindAction(actionname);

        if (action == null || action.bindings.Count <= bindingindex)
        {
            Debug.Log("reset binding fail");
            return;
        }
        if (action.bindings[bindingindex].isComposite)
        {
            for (int i = bindingindex; i < action.bindings.Count && action.bindings[i].isComposite; i++)
            {
                action.RemoveBindingOverride(i);
            }
        }
        else
        {
            action.RemoveBindingOverride(bindingindex);
        }
        InputBinding newbinding = action.bindings[bindingindex];
        foreach (InputBinding binding in action.actionMap.bindings)
        {
            if (binding.action == newbinding.action)
            {
                inputActions.asset.FindBinding(binding, out InputAction newaction);
                int newbindingindex = newaction.GetBindingIndex(binding);
                inputActions.asset.FindBinding(newbinding, out InputAction oldaction);
                int oldbindingindex = oldaction.GetBindingIndex(newbinding);
                if (newbindingindex == oldbindingindex)
                {
                    continue;
                }
            }
            if (newbinding.effectivePath == binding.effectivePath)
            {
                string overwritenbinding = binding.action;
                if (binding.isPartOfComposite)
                {
                    inputActions.asset.FindBinding(binding, out InputAction newaction);
                    int newbindingindex = newaction.GetBindingIndex(binding);
                    newaction.ApplyBindingOverride(newbindingindex, "");
                    savebindingsoverride(newaction);
                }
                else
                {
                    bindingindex = 0;
                    InputAction newaction = inputActions.asset.FindAction(overwritenbinding);
                    newaction.ApplyBindingOverride(0, "");
                    savebindingsoverride(newaction);
                }
            }
        }
        savebindingsoverride(action);
        keyrebindfinished?.Invoke();
    }
}
/*public static void checkforduplicates(InputAction action, int bindingindex)
{
    InputBinding newbinding = action.bindings[bindingindex];
    foreach (InputBinding binding in action.actionMap.bindings)
    {
        if (binding.action == newbinding.action)
        {
            continue;
        }
        if (newbinding.effectivePath == binding.effectivePath)
        {
            string overwritenbinding = binding.action;
            InputAction newaction = inputActions.asset.FindAction(overwritenbinding);
            newaction.ApplyBindingOverride(bindingindex, "");
            //newaction.ChangeBinding(0).Erase();
            //onduplicate(overwritenbinding, bindingindex, isacompsite);
            return true;
        }
    }
    return false;
}
}*/
/*private static void onduplicate(string actionname, int bindingindex, bool isacompisite)
{
    InputAction actiontorebind = inputActions.asset.FindAction(actionname);
    if (actiontorebind == null || bindingindex < 0)
    {
        Debug.Log("hallo");
        return;
    }
    Debug.Log("duplicate");
    Debug.Log(actionname.ToString());
    hallo = null;
    hallo = GameObject.Find(actionname);
    if (hallo != null) 
    {
        keyrebindtext = hallo.gameObject.GetComponent<Text>();
        keyrebindtext.color = Color.red;
        keyrebindtext.text = "choose button";
    }
    actiontorebind.Disable();                                       // der Hotkey wird disabled, wieso auch immer

    var rebind = actiontorebind.PerformInteractiveRebinding(bindingindex);              //ist eine Inputsystem function, startet den rebind prozess

    rebind.OnComplete(functionisruning =>                                             // wegen memoryleak
    {
        actiontorebind.Enable();
        functionisruning.Dispose();                                                   //wegen memoryleak

        savebindingoverride(actiontorebind);
        keyrebindfinished?.Invoke();
        if (!checkforduplicates(actiontorebind, bindingindex, isacompisite))
        {
            disablecantclicklayer?.Invoke();
        }
    });
    rebind.Start();                                                               // startet die funktion rebind/macht das die rebind funktion ständig durchläuft
}
}*/
/*if (!checkforduplicates(actiontorebind, bindingindex, isacomposite))
{
disablecantclicklayer?.Invoke();
}*/
/*        InputBinding newbinding = action.bindings[bindingindex];
        foreach (InputBinding binding in action.actionMap.bindings)
        {
            if (binding.action == newbinding.action)
            {
                continue;
            }
            if (newbinding.effectivePath == binding.effectivePath)
            {
                Debug.Log("duplicate");
                string overwritenbinding = binding.action;
                InputAction newaction = inputActions.asset.FindAction(overwritenbinding);
                newaction.ApplyBindingOverride(bindingindex, "");
            }
        }*/
