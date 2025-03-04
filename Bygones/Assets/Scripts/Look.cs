using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] int mouseSensitivity;
    [SerializeField] Transform playerCamera;
    float xRotation, yRotation;
    float mouseX, mouseY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        mouseX = (mouseX * (float)Time.deltaTime * mouseSensitivity);
        mouseY = (mouseY * (float)Time.deltaTime * mouseSensitivity);


        xRotation = xRotation - mouseY;
        xRotation = Mathf.Clamp(xRotation, -35, 40);
        yRotation = yRotation + mouseX;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerCamera.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void OnLook(InputValue input)
    {
        mouseX = input.Get<Vector2>().x;
        mouseY = input.Get<Vector2>().y;

    }
}
