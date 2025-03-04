using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSmoothTime;
    public float GravityStrength;
    public float WalkSpeed;
    public float RunSpeed;
    public float JumpStrength;

    private CharacterController controller;
    private Vector3 currentMoveVelocity;
    private Vector3 MoveDampVelocity;

    private Vector3 CurrentForceVelocity;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerInput = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical"),
        };

        if(PlayerInput.magnitude > 1f)

        {
            PlayerInput.Normalize();
        }

        Vector3 MoveVector = transform.TransformDirection(PlayerInput);
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed;

        currentMoveVelocity = Vector3.SmoothDamp(currentMoveVelocity, MoveVector * currentSpeed, ref MoveDampVelocity, MoveSmoothTime);

        controller.Move(currentMoveVelocity * Time.deltaTime);

        Ray groundCheckRay = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(groundCheckRay, 1.1f))

        {
            CurrentForceVelocity.y = -2f;

            if(Input.GetKey(KeyCode.Space))

            {

                CurrentForceVelocity.y = JumpStrength;
            }
        }
        else
        {
            CurrentForceVelocity.y -= GravityStrength * Time.deltaTime;
        }

        controller.Move(CurrentForceVelocity * Time.deltaTime);

    }
}
