using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Isaveload
{
    public bool savedata<T>(string dataname, T data);

    public T loaddata<T>(string dataname);
}
