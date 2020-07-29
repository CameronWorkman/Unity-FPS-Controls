using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;

    public float speed;
    public float sprintSpeedMultiplier;
    public float gravity;
    public float jumpHeight;
    private bool doubleJump;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isTouchingGround;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //this controls movement in the x and z directions based on input
        Vector3 move = transform.right * x + transform.forward * z;

        //instantiates a sphere at a given location using an empty object as a marker, tests if the sphere is touching anything tagged ground.
        //adjust bool to true if yes, false if no.
        isTouchingGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //noticed gravity acted odd if you didn't have a constant low velocity even when touching ground.
        if (isTouchingGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        //multiplies speed by a public float sprintSpeedMultiplier when LeftShift is held down.
        //if it isn't, applies normal move * speed to the character controller.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerController.Move(move * speed * sprintSpeedMultiplier * Time.deltaTime);
        }
        else
        {
            playerController.Move(move * speed * Time.deltaTime);
        }

        //when Jump is pressed, jumps if touching ground. if yes, jumps and sets doubleJump bool to True. If no, checks if doubleJump is True and jumps.
        if (Input.GetButtonDown("Jump"))
        {
            if (isTouchingGround)
            {
                velocity.y = 0;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                doubleJump = true;
            } else if (doubleJump)
            {
                velocity.y = 0;
                velocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
                doubleJump = false;
            }
        }


        //applies gravity to the player at all times. momentum builds up even if touching the ground, hence the earlier velocity adjustment if touching ground.
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }
}