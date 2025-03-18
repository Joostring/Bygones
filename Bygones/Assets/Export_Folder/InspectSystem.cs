using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class InspectSystem : MonoBehaviour
{
    public Camera mainCamera;             // Main Camera (regular gameplay)
    public Camera inspectCamera;          // Camera for inspection mode
    public Transform InteractorSource;    // Camera or player position
    public float InteractRange = 3f;      // Max distance for interaction
    public LayerMask interactableLayer;   // Filter for interactable objects

    //Cursor Variables
    private bool isLocked;

    public float rotationSpeed = 50f;     // Object rotation speed
    public float zoomSpeed = 0.5f;        // Speed of zooming in/out
    private float minZoom = 1f;           // Minimum zoom distance
    private float maxZoom = 1.5f;         // Maximum zoom distance

    private Transform objectToInspect;
    private Vector3 previousMousePos;
    private bool isInspecting = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float currentZoom = 1.5f;     // Default zoom distance

    private PlayerMovement playerMovement;
    private PlayerLook playerLook;

    public PostProcessVolume postProcessVolume; // Assign in Inspector
    private DepthOfField depthOfField;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerLook = FindObjectOfType<PlayerLook>();

        isLocked = true; //Locks cursor at start

        inspectCamera.gameObject.SetActive(false); // Ensure inspect camera is off

        // Get Depth of Field settings from PostProcessVolume
        if (postProcessVolume.profile.TryGetSettings(out depthOfField))
        {
            depthOfField.active = false; // Start disabled
        }
    }

    void Update()
    {
        CheckCursor();

        if (!isInspecting)
        {
            HandleInteraction();
        }
        else
        {
            HandleInspection();
        }
    }

    public void CheckCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isLocked)
            {
                isLocked = false;
            }
            else if (!isLocked)
            {
                isLocked = true;
            }
        }


        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (!isLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // **1. Handle Interaction**
    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange, interactableLayer))
            {
                StartInspectMode(hitInfo.transform);
            }
        }
    }

    // **2. Handle Inspection (Rotation & Zoom)**
    void HandleInspection()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Rotate Object
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePosition = Input.mousePosition - previousMousePos;
            float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);

            objectToInspect.rotation = rotation * objectToInspect.rotation;
            previousMousePos = Input.mousePosition;
        }

        // Zoom In/Out
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            currentZoom = Mathf.Clamp(currentZoom - scroll * zoomSpeed, minZoom, maxZoom);
            PositionObjectForInspection(); // Update object position based on zoom
            UpdateDOFFocus(); // Adjust Depth of Field dynamically
        }

        // Exit Inspect Mode
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitInspectMode();
            
        }
    }

    // **3. Start Inspect Mode**
    void StartInspectMode(Transform target)
    {
        isInspecting = true;
        objectToInspect = target;

        // Store original position & rotation
        originalPosition = objectToInspect.position;
        originalRotation = objectToInspect.rotation;

        // Set object to a fixed position in front of inspect camera
        currentZoom = 1.5f; // Reset zoom distance
        PositionObjectForInspection();

        // Reset rotation so objects always face forward
        objectToInspect.rotation = Quaternion.identity;

        // Enable inspect camera & disable main camera
        mainCamera.gameObject.SetActive(false);
        inspectCamera.gameObject.SetActive(true);

        // Enable Depth of Field Blur
        if (depthOfField != null)
        {
            depthOfField.active = true;
            UpdateDOFFocus(); // Set the initial focus on the object
        }

        // Disable player movement & look
        TogglePlayerControl(false);
    }

    // **4. Exit Inspect Mode**
    void ExitInspectMode()
    {
        isInspecting = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // Restore object to original position & rotation
        objectToInspect.position = originalPosition;
        objectToInspect.rotation = originalRotation;

        // Enable main camera & disable inspect camera
        inspectCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        // Disable Depth of Field Blur
        if (depthOfField != null)
        {
            depthOfField.active = false;
        }

        // Enable player movement & look
        TogglePlayerControl(true);
    }

    // **5. Position Object for Inspection (Handles Zoom)**
    void PositionObjectForInspection()
    {
        Vector3 inspectPosition = inspectCamera.transform.position + inspectCamera.transform.forward * currentZoom;
        objectToInspect.position = inspectPosition;
    }

    // **6. Update Depth of Field Focus to Keep Object Sharp**
    void UpdateDOFFocus()
    {
        if (depthOfField != null && objectToInspect != null)
        {
            float focusDistance = Vector3.Distance(inspectCamera.transform.position, objectToInspect.position);
            depthOfField.focusDistance.value = focusDistance; // Focus on the object
            depthOfField.aperture.value = 2.8f;
            depthOfField.focalLength.value = 50f;
        }
    }

    // **7. Enable/Disable Player Control**
    void TogglePlayerControl(bool enable)
    {
        playerMovement.enabled = enable;
        playerLook.enabled = enable;
    }
}
