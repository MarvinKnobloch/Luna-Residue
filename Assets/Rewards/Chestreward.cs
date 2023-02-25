using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chestreward : MonoBehaviour
{
    [SerializeField] private Firstarea firstarea;

    [SerializeField] private GameObject closedchest;
    [SerializeField] private GameObject openchest;
    public int rewardcount;
    public int rewardcountneeded;

    [SerializeField] private GameObject[] rewards;

    private void OnEnable()
    {

        if(firstarea.startingzonechest == true)
        {
            closedchest.SetActive(false);
            openchest.SetActive(true);
        }
        else
        {
            rewardcount = 0;
            closedchest.SetActive(true);
            openchest.SetActive(false);
        }
    }
    public void checkforreward()
    {
        rewardcount++;
        if(rewardcount >= rewardcountneeded)
        {
            closedchest.SetActive(false);
            openchest.SetActive(true);
            firstarea.startingzonechest = true;
            foreach (GameObject reward in rewards)
            {
                Instantiate(reward, transform.position + Vector3.up * 2, transform.rotation);
            }
            firstarea.autosave();
        }
    }

}
