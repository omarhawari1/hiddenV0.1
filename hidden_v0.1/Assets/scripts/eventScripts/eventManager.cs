using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManager : MonoBehaviour
{
    [Header("event1:")]
    [SerializeField] private GameObject secondPartOfHouse;
    [SerializeField] private Animator otherDoor1;

    //event 1 is activating the second part of the house after unlocking the door
    public void event1()
    {
        secondPartOfHouse.SetActive(true);
        otherDoor1.Play("doorCloseAnim");
    }
}
