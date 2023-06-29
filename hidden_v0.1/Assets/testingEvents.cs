using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingEvents : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            for(int i = 0; i <= (objects.Length -1); i++)
            {
                if(objects[i].activeSelf == true)
                {
                    objects[i].SetActive(false);
                }
                else
                {
                    objects[i].SetActive(true);
                }
            }
        }
    }
}
