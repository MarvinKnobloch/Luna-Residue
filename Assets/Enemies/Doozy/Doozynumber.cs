using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doozynumber : MonoBehaviour
{
    [SerializeField] private Doozycontroller doozycontroller;
    public int setnumber;

    private void OnDisable()
    {
        CancelInvoke();
        gameObject.GetComponent<Image>().color = Color.white;
        setnumber = 0;
    }
    public void confirm()
    {
        if(doozycontroller.canclick == true)
        {
            if (setnumber == doozycontroller.memoryclicknumber)
            {
                doozycontroller.memoryclicknumber++;
                gameObject.GetComponent<Image>().color = Color.green;
                if (doozycontroller.memoryclicknumber == doozycontroller.needednumbers)
                {
                    doozycontroller.success();
                }
            }
            else
            {
                doozycontroller.canclick = false;
                gameObject.GetComponent<Image>().color = Color.red;
                doozycontroller.fail();
            }
        }
    }
}
