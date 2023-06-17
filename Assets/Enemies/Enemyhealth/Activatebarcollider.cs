using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatebarcollider : MonoBehaviour
{
    private EnemyHP enemyhpscript;
    private void Awake()
    {
        enemyhpscript = GetComponentInParent<EnemyHP>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.triggercollider)
        {
            enemyhpscript.addtocanvas();
            GameObject mainobj = transform.parent.gameObject;
            if (Infightcontroller.infightenemylists.Contains(mainobj))
            {
                if(mainobj.TryGetComponent(out EnemyHP enemyHP))
                {
                    enemyHP.healthbar.currenttargetimage.gameObject.SetActive(true);
                    enemyHP.setnewtarget(true);
                    if (Movescript.lockontarget.gameObject == mainobj)
                    {
                        enemyHP.marktarget();
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.triggercollider)
        {
            enemyhpscript.removefromcanvas();
        }
    }
}
