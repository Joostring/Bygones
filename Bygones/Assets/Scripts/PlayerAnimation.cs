using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMovement(InputValue input)
    {
        animator.SetBool("IsMoving", true);
    }
    private void OnMovementStop(InputValue input)
    {
        animator.SetBool("IsMoving", false);
    }

   
}
