using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisioncontroller : MonoBehaviour
{
    private void Awake()
    {
        //player
        Physics.IgnoreLayerCollision(17, 6);

        //items
        Physics.IgnoreLayerCollision(8, 12);
        Physics.IgnoreLayerCollision(11, 12);
        Physics.IgnoreLayerCollision(15, 12);
        Physics.IgnoreLayerCollision(17, 12);

        //ignoreobjects
        Physics.IgnoreLayerCollision(8, 17);
        Physics.IgnoreLayerCollision(11, 17);
        Physics.IgnoreLayerCollision(15, 17);
    }
}
