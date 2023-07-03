using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactables : MonoBehaviour
{
    [SerializeField]private GameObject crossHairInteractable;
    [SerializeField] private LayerMask interactablesLayer;
    [SerializeField] private float interactableDistance;
    [SerializeField] private player_main player_Main;

    private Transform playerCamera;
    private KeyCode k_Interact;

    private void Start()
    {
        playerCamera = Camera.main.transform;
        k_Interact = player_Main.k_Interact;
    }
    private void Update()
    {
        if (player_Main.canOpenDoors)
        {
            handle_Interactables();
        }
    }
    private void handle_Interactables()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactableDistance, interactablesLayer))
        {
            crossHairInteractable.SetActive(true);
            if (hit.transform.tag == "Door")
            {
                if (Input.GetKeyDown(k_Interact))
                {
                    //open door
                }
            }
            if (hit.transform.tag == "lockedDoor")
            {
                //doorLocked
            }
        }
        else
        {
            crossHairInteractable.SetActive(false);
        }
    }
}
