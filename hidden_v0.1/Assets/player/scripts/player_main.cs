using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;


public class player_main : MonoBehaviour
{
    [Header("functional options: ")]
    public bool canUseHeadBob;
    public bool canSprint;
    public bool canUseFlashlight;
    public bool useFootSteps;
    public bool canPause;
    public bool canMove;
    public bool canLook;
    public bool flashlightLerp;

    [Header("player settings:")]
    public float mouseSens;
    [SerializeField]private float normalSpeed;
    [SerializeField]private float sprintSpeed;
    [SerializeField]private float gravityValue;
    [SerializeField] private float flashlightSlerpSpeed;

    [Header("controls: ")]
    [SerializeField]private KeyCode K_flashlight;
    [SerializeField]private KeyCode k_sprint;
    [SerializeField]private KeyCode k_pause;

    [Header("set componenets: ")]
    [SerializeField]private GameObject flashLight;
    [SerializeField]private CharacterController playerController;
    [SerializeField]private GameObject pauseMenu;
    [SerializeField]private GameObject settingsMenu;
    [SerializeField] private Animator headbobAnimator;
    [SerializeField] private Transform flashlightHolder;
    [SerializeField] private LayerMask groundLayer;

    [Header("footsteps: ")]
    [SerializeField]private float baseStepSpeed;
    [SerializeField]private float sprintStepSpeed;
    [SerializeField]private AudioSource footstepSource = default;
    [Header("Grass:")]
    [SerializeField]private AudioClip[] grassFootstepAudio = default;
    [Header("Wood:")]
    [SerializeField] private AudioClip[] woodFootstepAudio = default;

    private AudioClip lastFootstepAudio;
    private AudioClip newFootStepAudio;

    private float mouseX;
    private float mouseY;
    private float horizontal;
    private float vertical;
    private float xRotation = 0f;
    private Vector3 velocity;
    private Transform playerCamera;
    private float speed;
    private Vector3 flashLightVectOffset;


    //public
    [HideInInspector]
    public bool paused;
    [HideInInspector]
    public bool flashLightState = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = Camera.main.transform;
        flashLightVectOffset = flashlightHolder.position - playerCamera.position;
    }

    private void Update() 
    {

        if (canUseHeadBob)
        {
            headbobAnimator.SetFloat("isMoving", (Mathf.Abs(horizontal) + Mathf.Abs(vertical)));
        }
        if(canUseFlashlight)
        {
            handle_Flashlight();
        }
        if(canSprint)
        {
            handle_Sprint();
        }
        else
        {
            speed = normalSpeed;
        }
        if(canPause)
        {
            handle_pauseMenu();
        }
        gravity();
        if(canLook)
        {
            mouseLook();
        }
        if(canMove)
        {
            movement();
        }
        inputs();
    }

    private void movement()
    {
        //wasd movement:
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        playerController.Move(move * speed * Time.deltaTime);

    }
    private void gravity()
    {
        //gravity
        if(playerController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravityValue * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }
    private void mouseLook()
    {
        //rotate x-axis:
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


        //rotate y-axis:
        transform.Rotate(Vector3.up * mouseX);
    }
    public void handle_Footsteps()
    {
        if (useFootSteps)
        {
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), Vector3.down, out hit, 0.5f, groundLayer))
            {
                if (hit.transform.tag == "Grass")
                {
                    callFootstep(grassFootstepAudio);
                }
                else if (hit.transform.tag == "Wood")
                {
                    callFootstep(woodFootstepAudio);
                }
            }
        }

    }
    private void callFootstep(AudioClip[] audioClip)
    {
        newFootStepAudio = audioClip[Random.Range(0, audioClip.Length - 1)];
        while (newFootStepAudio == lastFootstepAudio)
        {
            newFootStepAudio = audioClip[Random.Range(0, audioClip.Length - 1)];
        }
        if (footstepSource.isPlaying) { return; }
        footstepSource.PlayOneShot(newFootStepAudio);
        lastFootstepAudio = newFootStepAudio;
    }
    private void handle_Sprint()
    {
        //sprint:
        if(Input.GetKey(k_sprint))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
    }
    private void handle_Flashlight()
    {
        //flashLight:
        if(Input.GetKeyDown(K_flashlight) && flashLightState == false)
        {
            flashLight.SetActive(true);
            flashLightState = true;
        }
        else if(Input.GetKeyDown(KeyCode.F) && flashLightState == true)
        {
            flashLight.SetActive(false);
            flashLightState = false;
        }

        //flashlight lerp:
        flashlightHolder.position = playerCamera.position - flashLightVectOffset;
        flashlightHolder.rotation = Quaternion.Slerp(flashlightHolder.rotation, playerCamera.transform.rotation, flashlightSlerpSpeed * Time.deltaTime);

    }
    private void handle_pauseMenu()
    {
        //pause
        if(Input.GetKeyDown(k_pause) && !paused)
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            paused = true;
        }
        //unpause
        else if(Input.GetKeyDown(k_pause) && paused)
        {
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            paused = false;
        }
    }

    private void inputs()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
}
