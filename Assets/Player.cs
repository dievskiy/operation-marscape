﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float gravity = -9.81f;
    public float jumpSpeed;
    public float accRate = 2.0f;
    public float decRate = -450f;
    float currentSpeed = 0.0f;
    float minSpeed = 0.0f;
    float boostSpeed;
   

    private CharacterController characterController;
    private Vector3 movement = new Vector3();
 
     // Start is called before the first frame update
       void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = currentSpeed * Time.deltaTime;

        if (currentSpeed < speed)
        {
            currentSpeed = currentSpeed + (accRate * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += -transform.right * (currentSpeed / 0.8f) * Time.deltaTime;         
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            boostSpeed = currentSpeed * 1.5f;
            movement += transform.right * boostSpeed * Time.deltaTime;
        }

        if (characterController.isGrounded)
        {
            movement.y = -0.1f;

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                movement.y = jumpSpeed;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                currentSpeed = currentSpeed + (decRate * Time.deltaTime);
            }
        }

        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, speed);

        movement.y += gravity * Time.deltaTime;

        characterController.Move(movement * Time.deltaTime);
    }

}
