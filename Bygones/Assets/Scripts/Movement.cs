using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody characterRB;
    Vector3 movementInput;
    Vector3 movementVector;
    [SerializeField] float movementSpeed;

    void Start()
    {
        characterRB = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        ApplyMovement();
    }
    private void OnMovement(InputValue value)
    {
        movementInput = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
    }
    private void OnMovementStop()
    {
        movementVector = Vector3.zero;
        characterRB.velocity = Vector3.zero;
    }
    private void ApplyMovement()
    {
        if (movementInput != Vector3.zero)
        {
            //movementVector = movementInput.x * transform.right + movementInput.z * transform.forward;
            movementVector = transform.right * movementInput.x + transform.forward * movementInput.z;
            movementVector.y = 0;

            characterRB.velocity = (movementVector * (float)Time.fixedDeltaTime * movementSpeed);
        }
    }
}