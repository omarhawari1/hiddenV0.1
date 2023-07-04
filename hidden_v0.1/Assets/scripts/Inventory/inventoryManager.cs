using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
    public List<keys> keys = new List<keys>();

    public void Add(keys key)
    {
        keys.Add(key)
;   }
    public void Remove(keys key)
    {
        keys.Remove(key);
    }
}
