using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float gravity = -9.81f;
    public float jumpSpeed;

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
        movement.x = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += -transform.right * speed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement += transform.right * speed;
        }

        if (characterController.isGrounded)
        {
            movement.y = -0.1f;

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                movement.y = jumpSpeed;
            }
        }

        movement.y += gravity * Time.deltaTime;

        characterController.Move(movement * Time.deltaTime);
    }
}
