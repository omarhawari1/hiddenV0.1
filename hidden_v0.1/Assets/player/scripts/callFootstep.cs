using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callFootstep : MonoBehaviour
{
    private player_main player_Main;


    private void Start()
    {
        player_Main = GameObject.FindAnyObjectByType<player_main>();
    }
    public void step()
    {
        player_Main.handle_Footsteps();
    }
}
