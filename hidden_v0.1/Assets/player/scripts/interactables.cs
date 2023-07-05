using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactables : MonoBehaviour
{
    [SerializeField]private GameObject crossHairInteractable;
    [SerializeField] private LayerMask interactablesLayer;
    [SerializeField] private LayerMask doorLayer;
    [SerializeField] private float interactableDistance;
    [SerializeField] private eventManager eventManager;
    [SerializeField] private player_main player_Main;
    [SerializeField] private inventoryManager inventoryManager;
    [SerializeField] private AudioClip doorOpenAudio;


    [Header("preview panel:")]
    [SerializeField] private GameObject itemPreviewPanel;
    [SerializeField] private RawImage itemPreviewImage;

    [Header("keys:")]
    [SerializeField] private keys sideDoorKey;

    [Header("doors:")]
    [SerializeField] private doors sideDoor;

    private Transform playerCamera;
    private KeyCode k_Interact;

    private bool gamePaused = false;


    private void Start()
    {
        playerCamera = Camera.main.transform;
        k_Interact = player_Main.k_Interact;

    }
    private void Update()
    {
        if (player_Main.canUseInteractables)
        {
            handle_Interactables();
        }

        if(gamePaused == true)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                itemPreviewPanel.SetActive(false);
                itemPreviewImage = null;
                gamePaused = false;
            }
        }
    }
    private void handle_Interactables()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactableDistance, interactablesLayer))
        {
            crossHairInteractable.SetActive(true);

            // keys:
            if (hit.transform.tag == "Key")
            {
                if (Input.GetKeyDown(k_Interact))
                {
                    Destroy(hit.transform.gameObject);
                    inventoryManager.Add(hit.transform.GetComponent<assignData>().key);

                    //paralize player while inspecting item:
                    gamePaused = true;
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0;
                    itemPreviewImage.texture = hit.transform.GetComponent<assignData>().key.keyPreview;
                    itemPreviewPanel.SetActive(true);
                }
            }
        }
        else if(Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactableDistance, doorLayer))
        {
            crossHairInteractable.SetActive(true);
            if (hit.transform.tag == "unlockedDoor")
            {
                if (Input.GetKeyDown(k_Interact))
                {
                    hit.transform.GetComponent<Animator>().Play("doorOpenAnim");
                }
            }
            if (hit.transform.tag == "lockedDoor")
            {
                lockedDoors(hit);
            }
        }
        else
        {
            crossHairInteractable.SetActive(false);
        }
    }

    private void lockedDoors(RaycastHit hit)
    {
        if (hit.transform.parent.GetComponent<assignData>().door.name == sideDoor.name)
        {
            if (Input.GetKeyDown(k_Interact))
            {
                foreach(keys key in inventoryManager.keys)
                {
                    if(key.name == sideDoorKey.name)
                    {
                        eventManager.event1();
                        hit.transform.tag = "unlockedDoor";
                    }
                }
            }
        }
    }
}
