using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;

public class InspectSystem : MonoBehaviour
{
    public Camera mainCamera;
    public Camera inspectCamera;
    public Transform InteractorSource;
    public float InteractRange = 3f;
    public LayerMask interactableLayer;
    public CursorManager cursorManager;
    public GameObject BackgroundUI;
    public GameObject PickupUI;
    public GameObject crosshair;

    public GameObject inventoryCanvas;  // Referens till inventory Canvas
    private bool isInventoryOpen = false;  // Håller koll på om inventory är öppet
    public GameObject inventoryGrid; // Grid där ikonerna ska visas
    public GameObject inventoryIconPrefab; // Prefab för ikonen
    public Sprite Key_DecorativeIcon, Key_GenIcon, Key_OfficeIcon, Key_FrontIcon, flashlightIcon, defaultIcon; // Ikoner för objekt




    private bool isLocked;
    public float rotationSpeed = 50f;
    public float zoomSpeed = 0.5f;
    private float minZoom = 0.75f;
    private float maxZoom = 2f;

    private Transform objectToInspect;
    private Vector3 previousMousePos;
    private bool isInspecting = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public float currentZoom = 0.5f;

    private PlayerMovement playerMovement;
    private PlayerLook playerLook;

    public PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;

    // INVENTORY SYSTEM
    [SerializeField] public List<string> inventory = new List<string>();

    // UI-element som ska aktiveras baserat på objekt som inspekteras
    public GameObject[] uiElements; 

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerLook = FindObjectOfType<PlayerLook>();

        isLocked = true;
        inspectCamera.gameObject.SetActive(false);

        if (postProcessVolume.profile.TryGetSettings(out depthOfField))
        {
            depthOfField.active = false;
        }

        // Se till att alla UI-element är avstängda från början
        foreach (var ui in uiElements)
        {
            ui.SetActive(false);
        }

        // Se till att inventory UI inte är synligt i början
        inventoryCanvas.SetActive(false);
    }


    void Update()
    {
        CheckCursor();

        if (!isInspecting && !isInventoryOpen)  // Endast om inte inspekterar eller öppnat inventory
        {
            HandleInteraction();
        }
        else if (isInventoryOpen)
        {
            //HandleInventory();
        }
        else
        {
            HandleInspection();
        }

        HandleInventoryToggle();  // Kolla om Tab trycks för att öppna/stänga inventory
    }

    void HandleInventoryToggle()
    {
        // Kontrollera att inventory inte öppnas när vi inspekterar ett objekt
        if (Input.GetKeyDown(KeyCode.Tab) && !isInspecting)
        {
            if (!isInventoryOpen)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }



    public void CheckCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isLocked = !isLocked;
        }

        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLocked;
    }

    void OpenInventory()
    {
        // Aktivera inventory UI
        inventoryCanvas.SetActive(true);

        // Dölj crosshair (förutsätter att du har ett GameObject som refererar till crosshair)
        if (crosshair != null)
        {
            crosshair.SetActive(false);  // Döljer crosshair
        }

        // Stäng av spelarens rörelse och kamera
        TogglePlayerControl(false);

        // Visa cursor och lås inte musen
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Sätt inventory till öppet
        isInventoryOpen = true;
    }


    void CloseInventory()
    {
        
        inventoryCanvas.SetActive(false);

        
        if (crosshair != null)
        {
            crosshair.SetActive(true);  
        }

        
        TogglePlayerControl(true);

        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        
        isInventoryOpen = false;
    }




    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, InteractRange, interactableLayer))
            {
                StartInspectMode(hitInfo.transform);
            }
        }
    }

    void HandleInspection()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (Input.GetMouseButtonDown(0))
        {
            previousMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePosition = Input.mousePosition - previousMousePos;

            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;
            float rotationX = -deltaMousePosition.y * rotationSpeed * Time.deltaTime; 

            
            objectToInspect.Rotate(Vector3.up, rotationY, Space.World);  
            objectToInspect.Rotate(Vector3.right, rotationX, Space.Self);

            previousMousePos = Input.mousePosition;
        }
    


    float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            currentZoom = Mathf.Clamp(currentZoom - scroll * zoomSpeed, minZoom, maxZoom);
            PositionObjectForInspection();
            UpdateDOFFocus();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitInspectMode();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PickupItem();
        }
    }

    void StartInspectMode(Transform target)
    {
        isInspecting = true;
        objectToInspect = target;
        originalPosition = objectToInspect.position;
        originalRotation = objectToInspect.rotation;
        currentZoom = 1.5f;
        PositionObjectForInspection();
        objectToInspect.rotation = Quaternion.identity;

        mainCamera.gameObject.SetActive(false);
        inspectCamera.gameObject.SetActive(true);

        if (depthOfField != null)
        {
            depthOfField.active = true;
            UpdateDOFFocus();
        }

        TogglePlayerControl(false);

       
        if (cursorManager != null)
        {
            cursorManager.gameObject.SetActive(false);
        }

      
        UpdateUIForItem(objectToInspect.name);
    }

    void ExitInspectMode()
    {
        isInspecting = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        objectToInspect.position = originalPosition;
        objectToInspect.rotation = originalRotation;

        inspectCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        if (depthOfField != null)
        {
            depthOfField.active = false;
        }

        TogglePlayerControl(true);

        
        if (cursorManager != null)
        {
            cursorManager.gameObject.SetActive(true);
        }

        // Stäng av alla UI-element när inspektionen är klar
        foreach (var ui in uiElements)
        {
            ui.SetActive(false);
        }

        resetUI();
    }

    void PickupItem()
{
    if (objectToInspect != null && objectToInspect.CompareTag("Pickable"))
    {
        string itemName = objectToInspect.name;
        AddToInventory(itemName);  
        Debug.Log("Plockade upp: " + itemName);

        
        UpdateInventoryUI(itemName);

        
        Destroy(objectToInspect.gameObject);

        ExitInspectMode();
    }
}




    void PositionObjectForInspection()
    {
        Vector3 inspectPosition = inspectCamera.transform.position + inspectCamera.transform.forward * currentZoom;
        objectToInspect.position = inspectPosition;
    }


    void UpdateDOFFocus()
    {
        if (depthOfField != null && objectToInspect != null)
        {
            float focusDistance = Vector3.Distance(inspectCamera.transform.position, objectToInspect.position);
            depthOfField.focusDistance.value = focusDistance;
            depthOfField.aperture.value = 2.8f;
            depthOfField.focalLength.value = 50f;
        }
    }

    void TogglePlayerControl(bool enable)
    {
        playerMovement.enabled = enable;
        playerLook.enabled = enable;
    }


    // INVENTORY-FUNKTIONER

   
    void AddToInventory(string itemName)
    {
        if (!inventory.Contains(itemName))
        {
            inventory.Add(itemName);
            Debug.Log("Lagt till i inventory: " + itemName);
        }
        else
        {
            Debug.Log(itemName + " finns redan i inventory.");
        }
    }

    void UpdateInventoryUI(string itemName)
    {
        // Hämta rätt ikon baserat på objektets namn
        Sprite itemIcon = GetItemIcon(itemName);

        // Instantiiera GameObject från prefab
        GameObject newIconObject = Instantiate(inventoryIconPrefab, inventoryGrid.transform);

        // Hämta Image-komponenten från det nya GameObjectet
        Image newIconImage = newIconObject.GetComponent<Image>();

        // Sätt sprite för bilden
        newIconImage.sprite = itemIcon;

    }


    Sprite GetItemIcon(string itemName)
    {
        // Returnera rätt ikon baserat på objektets namn
        switch (itemName)
        {
            case "Flashlight_Inspect":
                return flashlightIcon; 
            case "Key_Decorative_Inspect":
                return Key_DecorativeIcon;
            case "Key_Gen_Inspect":
                return Key_GenIcon;
            case "Key_Front_Inspect":
                return Key_FrontIcon;
            case "Key_Office_Inspect":
                return Key_OfficeIcon;
            
            default:
                return defaultIcon; // Standard ikon om objektet inte har någon specifik ikon
        }
    }


    // Kollar om spelaren har ett specifikt objekt
    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }

    // Skriver ut alla objekt i inventory
    public void PrintInventory()
    {
        Debug.Log("Inventory innehåller: " + string.Join(", ", inventory));
    }

    public void resetUI()
    {
        BackgroundUI.SetActive(false);
        PickupUI.SetActive(false);
    }

    public bool HasKey(string keyName)
    {
        return inventory.Contains(keyName);
    }

    void UpdateUIForItem(string itemName)
    {
        foreach (var ui in uiElements)
        {
            ui.SetActive(false); // Stäng av alla UI-element först

        }

        // Aktivera UI-element baserat på objektets namn och justera zoomnivåerna så de inte kolliderar med kamerafan
        switch (itemName)
        {
            case "Book_A_Inspect":
                uiElements[0].SetActive(true);
                BackgroundUI.SetActive(true);

                minZoom = 0.75f;
                maxZoom = 1.5f;
                break;
            case "Book_B_Inspect":
                uiElements[1].SetActive(true);
                BackgroundUI.SetActive(true);

                minZoom = 0.75f;
                maxZoom = 1.5f;
                break;
            case "Key_Front_Inspect":
                uiElements[2].SetActive(true);
                BackgroundUI.SetActive(true);
                PickupUI.SetActive(true);
                minZoom = 0.5f;
                maxZoom = 0.75f;
                break;
            case "Key_Decorative_Inspect":
                uiElements[3].SetActive(true);
                BackgroundUI.SetActive(true);
                PickupUI.SetActive(true);
                minZoom = 0.5f;
                maxZoom = 0.75f;
                break;
            case "Key_Gen_Inspect":
                uiElements[4].SetActive(true);
                BackgroundUI.SetActive(true);
                PickupUI.SetActive(true);
                minZoom = 0.5f;
                maxZoom = 0.75f;
                break;
            case "Key_Office_Inspect":
                uiElements[5].SetActive(true);
                BackgroundUI.SetActive(true);
                PickupUI.SetActive(true);
                minZoom = 0.5f;
                maxZoom = 0.75f;
                break;
            case "Flashlight_Inspect":
                uiElements[6].SetActive(true);
                BackgroundUI.SetActive(true);
                PickupUI.SetActive(true);
                minZoom = 0.55f;
                maxZoom = 0.8f;
                break;
            case "Note_1_Inspect":
                uiElements[7].SetActive(true);
                BackgroundUI.SetActive(true);
                PickupUI.SetActive(true);
                minZoom = 0.7f;
                maxZoom = 1f;
                break;

            default:
                break;
        }


        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        PositionObjectForInspection();
    }

}