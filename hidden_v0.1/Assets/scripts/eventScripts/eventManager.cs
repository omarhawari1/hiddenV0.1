using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManager : MonoBehaviour
{
    [SerializeField] private GameObject secondPartOfHouse;
    //event 1 is activating the second part of the house after unlocking the door
    public void event1()
    {
        secondPartOfHouse.SetActive(true);
    }
}
