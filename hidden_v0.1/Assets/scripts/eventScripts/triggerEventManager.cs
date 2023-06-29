using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider))]
public class triggerEventManager : MonoBehaviour
{
    [SerializeField] private string triggeredByTag;
    [SerializeField] private UnityEvent triggerEnterEvent;
    [SerializeField] private UnityEvent triggerExitEvent;
    [SerializeField] private UnityEvent triggerStayEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == triggeredByTag)
        {
            triggerEnterEvent.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == triggeredByTag)
        {
            triggerExitEvent.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == triggeredByTag)
        {
            triggerStayEvent.Invoke();
        }
    }
}
