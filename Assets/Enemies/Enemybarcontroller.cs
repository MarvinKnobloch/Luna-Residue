using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybarcontroller : MonoBehaviour
{
    [SerializeField] private Enemyhealthbar healthbarprefab;                       // der direkt verweiß zu Enemyhealthbar sparrt mit Getcomponent                    (könnte auch Gameobject sein)

    private Dictionary<EnemyHP, Enemyhealthbar> healthbars = new Dictionary<EnemyHP, Enemyhealthbar>();

    private void Awake()
    {
        EnemyHP.addhealthbar += addbar;
        EnemyHP.removehealthbar += removebar;
    }
    private void addbar(EnemyHP enemyhealthbar)
    {
        if(healthbars.ContainsKey(enemyhealthbar) == false)
        {
            Enemyhealthbar healthbar = Instantiate(healthbarprefab, transform);
            healthbars.Add(enemyhealthbar, healthbar);
            healthbar.sethealthbar(enemyhealthbar);
        }
    }
    private void removebar(EnemyHP enemyhealthbar)
    {
        if (healthbars.ContainsKey(enemyhealthbar))
        {
            Destroy(healthbars[enemyhealthbar].gameObject);
            healthbars.Remove(enemyhealthbar);
        }
    }
    private void OnDisable()
    {
        EnemyHP.addhealthbar -= addbar;
        EnemyHP.removehealthbar -= removebar;
    }
}
