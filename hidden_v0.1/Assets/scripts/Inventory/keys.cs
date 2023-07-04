using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Key", menuName ="Keys/Create New Key")]

public class keys : ScriptableObject
{
    public int ID;
    public string keyName;
    public Texture2D keyPreview;
}
