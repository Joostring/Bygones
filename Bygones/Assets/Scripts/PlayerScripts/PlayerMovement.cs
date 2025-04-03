using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSmoothTime;
    public float GravityStrength;
    public float WalkSpeed;
    public float RunSpeed;
    public float JumpStrength;

    public Animator animator;

    private CharacterController controller;
    private Vector3 currentMoveVelocity;
    private Vector3 MoveDampVelocity;

    private Vector3 CurrentForceVelocity;

    private bool isPlayerMoving = true;
    private bool isReversedMoving = false;
    private bool isFlashbackOn = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerMoving && !isReversedMoving && !isFlashbackOn)
        {
            Vector3 PlayerInput = new Vector3
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = 0f,
                z = Input.GetAxisRaw("Vertical"),
            };

            if (PlayerInput.magnitude > 1f)

            {
                PlayerInput.Normalize();
            }

            Vector3 MoveVector = transform.TransformDirection(PlayerInput);
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed;

            currentMoveVelocity = Vector3.SmoothDamp(currentMoveVelocity, MoveVector * currentSpeed, ref MoveDampVelocity, MoveSmoothTime);

            controller.Move(currentMoveVelocity * Time.deltaTime);

            if (MoveVector == Vector3.zero)
            {
                animator.SetFloat("Speed", 0f);
            }
            else
            {
                animator.SetFloat("Speed", 0.5f);
            }

            Ray groundCheckRay = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(groundCheckRay, 1.1f))

            {
                CurrentForceVelocity.y = -2f;

                if (Input.GetKey(KeyCode.Space))

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

        else if (isReversedMoving && !isPlayerMoving && !isFlashbackOn)
        {
            Vector3 PlayerInput = new Vector3
            {
                x = Input.GetAxisRaw("Vertical"),
                y = 0f,
                z = Input.GetAxisRaw("Horizontal"),
            };

            if (PlayerInput.magnitude > 1f)

            {
                PlayerInput.Normalize();
            }

            Vector3 MoveVector = transform.TransformDirection(PlayerInput);
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed;

            currentMoveVelocity = Vector3.SmoothDamp(currentMoveVelocity, MoveVector * currentSpeed, ref MoveDampVelocity, MoveSmoothTime);

            controller.Move(currentMoveVelocity * Time.deltaTime);


            if (MoveVector == Vector3.zero)
            {
                animator.SetFloat("Speed", 0f);
            }
            else
            {
                animator.SetFloat("Speed", 0.5f);
            }

            Ray groundCheckRay = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(groundCheckRay, 1.1f))

            {
                CurrentForceVelocity.y = -2f;

                if (Input.GetKey(KeyCode.Space))

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
        else if (isFlashbackOn)
        {

        }
    }

    public void SetMovementState(bool value)
    {
        isPlayerMoving = value;
    }
    public void SetReversedMovementState(bool value)
    {
        isReversedMoving = value;
    }
    public void SetStopMovement(bool value)
    {
        isFlashbackOn = value;
    }
}
