using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Housecontroller : MonoBehaviour
{
    public int setlantern;
    public int neededlantern;
    public TextMeshProUGUI numbertext;
    public bool finish;

    public GameObject reward;

    private void OnEnable()
    {
        neededlantern = setlantern;
        numbertext.text = neededlantern.ToString();
    }

    public void addlantern()
    {
         neededlantern--;
         numbertext.text = neededlantern.ToString();
         if(neededlantern == 0)
         {
             finish = true;
            reward.GetComponent<Rewardinterface>().addrewardcount();
         }
         if(neededlantern < 0)
         {
             if(finish == true)
             {
                reward.GetComponent<Rewardinterface>().removerewardcount();
             }
             finish = false;
         }
    }
    public void removelantern()
    {
         neededlantern++;
         numbertext.text = neededlantern.ToString();
         if(neededlantern == 0)
         {
             finish = true;
             reward.GetComponent<Rewardinterface>().addrewardcount();
        }

        if (neededlantern != 0)
        {
            if (finish == true)
            {
                reward.GetComponent<Rewardinterface>().removerewardcount();
            }
            finish = false;
        }
    }
}
