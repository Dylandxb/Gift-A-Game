using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;                                   //Keeps variables private but can still be seen in the inspector
    [SerializeField] private float walkSpeed;                                   //Prevents any errors when trying to access the variable from different scripts
    [SerializeField] private float runSpeed;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;


    private Vector3 moveDirection;                                              //Direction the player is facing
    private Vector3 velocity;



    //REFERENCES

    private CharacterController controller;                                     //Reference to the character controller in the inspector
    private Animator anim;
    private void Start()
    {
        controller = GetComponent<CharacterController>();                       //Components taken from the Player object
        anim = GetComponentInChildren<Animator>();                              //Animator is equal to the player animator, getcomponent would return null because player object doesnt have animator (only model does)
    }

    private void Update()
    {
        CharacterMovement();
    }

    private void CharacterMovement()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);        //CheckSphere draws small sphere at bottom of feet and check if its on layer ground
        //Position of player, radius of sphere around ground check, groundmask is on a separate layer

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);                        //Uses players forward

        if (isGrounded)                                         //Only check for the if statements when player is grounded
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))                       //(0,0,0)        
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }
            moveDirection *= moveSpeed;

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }

        controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;               //Calculates gravity  
        controller.Move(velocity * Time.deltaTime); //Applies gravity
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1.0f, 0.1f,Time.deltaTime);              //Smooths out transition to run animation
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);         //Square root the height by gravity and -2 
        
        //DISPLAY JUMP ANIMATION
        //If !isGrounded, anim.SetFloat(
        //anim.setbool("isinair",true);
        //else ("isinair", false);
        //Make transition from Idle and Movement to a Jump
    }
}
