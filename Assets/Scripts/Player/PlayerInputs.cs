﻿using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    // Inputs
    public Vector2 direction;
    public bool possess;
    public bool unpossess;
    public bool jump;

    void Update()
    {
        GetInputs();
    }

    public void GetInputs()
    {
        // Get Movement Inputs
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Get Possess Inputs
        possess = (Input.GetMouseButtonDown(1) || Input.GetButtonDown("RightBumper"));
        unpossess = (Input.GetMouseButtonDown(0) || Input.GetButtonDown("LeftBumper"));

        // Get Jump Inputs
        jump = (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("GamepadJump"));
    }
}