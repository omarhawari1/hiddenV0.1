using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Door", menuName = "Doors/Create New Door")]

public class doors : ScriptableObject
{
    public string ID;
    public string doorName;
    public keys correspodingKey;
}
