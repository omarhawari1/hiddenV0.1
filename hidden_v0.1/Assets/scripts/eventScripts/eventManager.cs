using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class eventManager : MonoBehaviour
{
    [Header("event1:")]
    [SerializeField] private UnityEvent event1_event;

    //event 1 is activating the second part of the house after unlocking the door
    public void event1()
    {
        event1_event.Invoke();
    }
}
