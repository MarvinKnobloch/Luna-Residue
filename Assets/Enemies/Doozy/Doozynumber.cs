using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doozynumber : MonoBehaviour
{
    [SerializeField] private GameObject doozycontroller;
    public int setnumber;

    private void OnDisable()
    {
        CancelInvoke();
        gameObject.GetComponent<Image>().color = Color.white;
        setnumber = 0;
    }
    public void confirm()
    {
        if(Doozycontroller.canclick == true)
        {
            if (setnumber == Doozycontroller.memoryclicknumber)
            {
                Doozycontroller.memoryclicknumber++;
                gameObject.GetComponent<Image>().color = Color.green;
                if (Doozycontroller.memoryclicknumber == 5)
                {
                    doozycontroller.GetComponent<Doozycontroller>().success();
                }
            }
            else
            {
                Doozycontroller.canclick = false;
                gameObject.GetComponent<Image>().color = Color.red;
                doozycontroller.GetComponent<Doozycontroller>().fail();
            }
        }
    }
}
