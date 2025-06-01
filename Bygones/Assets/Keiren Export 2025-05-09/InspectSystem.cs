//using UnityEngine.UIElements;

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using TMPro;
using System.Collections;

/// <summary>
/// All of this code was designed and coded by Keiren Wall Stewart
/// </summary>
public class InspectSystem : MonoBehaviour
{
    
    [Header("Prefabs för inspect från inventory")]
    public GameObject keyDecorativePrefab;
    public GameObject keyGenPrefab;
    public GameObject keyOfficePrefab;
    public GameObject keyFrontPrefab;
    public GameObject flashlightPrefab;
    public GameObject keyMasterbedroomPrefab;
    public GameObject keyGatePrefab;
    public GameObject matchesPrefab;
    public GameObject newspaperPrefab;
    public GameObject keyBasementPrefab;
    public GameObject diaryPrefab;  

    public GameObject Note1Prefab;
    public GameObject Note2Prefab;
    public GameObject Note3Prefab;
    public GameObject Note4Prefab;
    public GameObject Note5Prefab;


    [Header("Interaction Prompt")]
    public GameObject interactPromptText;
    public GameObject doorPromptText;



    private bool inspectingFromInventory = false;

    [Header("Interact grejer")]
    public Camera mainCamera;
    public Camera inspectCamera;
    public Transform InteractorSource;
    public float InteractRange = 2f;
    public LayerMask interactableLayer;
    public LayerMask DoorLayer;
    public CursorManager cursorManager;
    public GameObject BackgroundUI;
    public GameObject PickupUI;
    public GameObject crosshair;
    public GameObject hoverBox;
    public TMP_Text hoverText;
    public GameObject readUI; // not added by Keiren
    public ProgressSystem progressSystem; // not added by Keiren

    [Header("Inventory Gameobjects")]
    public GameObject inventoryCanvas;  // Referens till inventory Canvas
    private bool isInventoryOpen = false;  // Håller koll på om inventory är öppet
    public GameObject inventoryGrid; // Grid där ikonerna ska visas
    public GameObject inventoryIconPrefab; // Prefab för ikonen
    public Sprite Key_Icon, Note_Icon, flashlightIcon, defaultIcon, Matches_Icon; // Ikoner för objekt

    


    private bool isLocked;
    public float rotationSpeed = 50f;
    public float zoomSpeed = 0.5f;
    private float minZoom = 0.75f;
    private float maxZoom = 2f;

    private Transform objectToInspect;
    private Vector3 previousMousePos;
    public bool isInspecting = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public float currentZoom = 0.5f;

    private PlayerMovement playerMovement;
    private PlayerLook playerLook;

    public PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;

    
    public CameraShake cameraShaker;



    [SerializeField] public List<string> inventory = new List<string>();

    
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
        UpdateInteractionPrompt();

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

    GameObject GetItemPrefab(string itemName)
    {
        switch (itemName)
        {
            case "Key_Decorative_Inspect": return keyDecorativePrefab;
            case "Key_Gen_Inspect": return keyGenPrefab;
            case "Key_Office_Inspect": return keyOfficePrefab;
            case "Key_Front_Inspect": return keyFrontPrefab;
            case "Flashlight_Inspect": return flashlightPrefab;
            case "Note_1_Inspect": return Note1Prefab;
            case "Key_Gate_Inspect": return keyGatePrefab;
            case "Key_Masterbedroom_Inspect": return keyMasterbedroomPrefab;
            case "Matches_Inspect": return matchesPrefab;
            case "Newspaper_Inspect": return newspaperPrefab;
            case "Key_Basement_Inspect": return keyBasementPrefab;
            case "Note_2_Inspect":  return Note2Prefab;
            case "Note_3_Inspect": return Note3Prefab;
            case "Note_4_Inspect": return Note4Prefab;
            case "Note_Diary_Inspect": return diaryPrefab;
            case "Note_Final_Inspect": return Note5Prefab;
            default: return null;
        }
    }


    

        void OpenInventory()
        {
            
            inventoryCanvas.SetActive(true);

            
            if (crosshair != null)
            {
                crosshair.SetActive(false);  
            }

            TogglePlayerControl(false);

        SetCursorVisibility(true);

        if (interactPromptText != null)
            interactPromptText.SetActive(false);


        isInventoryOpen = true;
        }


        void CloseInventory()
        {
            hoverText.text = "";
            inventoryCanvas.SetActive(false);

        
            if (crosshair != null)
            {
                crosshair.SetActive(true);  
            }

        
            TogglePlayerControl(true);

        if (interactPromptText != null)
            interactPromptText.SetActive(false);

        SetCursorVisibility(false);

        
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
                //if (cameraShaker != null)
                //{
                //    cameraShaker.TriggerShake();
                //}
                StartInspectMode(hitInfo.transform);

                // not added by Keiren
                if (objectToInspect != null) // PROGRESS NOTES ADDED WHEN INSPECTING AN ITEM 
                {
                    ProgressNoteData noteData = objectToInspect.GetComponent<ProgressNoteData>();

                    if (noteData != null && !noteData.noteAlreadyAdded)
                    {
                        foreach (string line in noteData.noteLines)
                        {
                            progressSystem.AddNote(line);

                        }
                        noteData.noteAlreadyAdded = true;
                    }
                }
                //--------
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }

        // not added by Keiren
        if (Input.GetKeyDown(KeyCode.R))
        {
            NoteReader noteReader = objectToInspect?.GetComponent<NoteReader>();
            if (noteReader != null)
            {
                noteReader.ToggleNoteUI();
            }
        }
        // -------
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

        if (interactPromptText != null)
            interactPromptText.SetActive(false);

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

      
        UpdateUIForItem(objectToInspect.name, objectToInspect.gameObject);

        //if (playerLook != null)
        //{
        //    playerLook.enabled = true; // Återaktivera musstyrningen när inspektionsläget startar
        //}
    }

    void SetCursorVisibility(bool visible)
    {
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
    }


    public void ExitInspectMode()
    {
        isInspecting = false;
        SetCursorVisibility(false);  // <-- använd istället för de två raderna

        objectToInspect.position = originalPosition;
        objectToInspect.rotation = originalRotation;

        inspectCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        if (depthOfField != null)
        {
            depthOfField.active = false;
        }

        TogglePlayerControl(true);

        // ---- not added by Keirer-----
        NoteReader noteReader = objectToInspect?.GetComponent<NoteReader>();
        if (noteReader != null)
        {
            noteReader.HideNote();
        }

        //--------

        if (cursorManager != null)
        {
            cursorManager.gameObject.SetActive(true);
        }

        foreach (var ui in uiElements)
        {
            ui.SetActive(false);
        }
       

        resetUI();

        if (inspectingFromInventory && objectToInspect != null)
        {
            Destroy(objectToInspect.gameObject);
            inspectingFromInventory = false;

            OpenInventory();  
        }
    }

    void LateUpdate()
    {
        if (Time.timeScale == 0) { return; }

        if (isInspecting || isInventoryOpen)
            SetCursorVisibility(true);
        else
            SetCursorVisibility(false);
    }

    void UpdateInteractionPrompt()
    {
        Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, InteractRange, interactableLayer))
        {
            if (interactPromptText != null && !interactPromptText.activeSelf)
            {
                interactPromptText.SetActive(true);
            }
        }
        else
        {
            if (interactPromptText != null && interactPromptText.activeSelf)
            {
                interactPromptText.SetActive(false);
            }
        }

        if (Physics.Raycast(ray, out hitInfo, InteractRange, DoorLayer))
        {
            if (doorPromptText != null && !doorPromptText.activeSelf)
            {
                doorPromptText.SetActive(true);
            }
        }
        else
        {
            if (doorPromptText != null && doorPromptText.activeSelf)
            {
                doorPromptText.SetActive(false);
            }
        }
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

        // === Koppla in hover-scriptet ===
        InventorySlotHover hoverScript = newIconObject.GetComponent<InventorySlotHover>();
        if (hoverScript != null)
        {
            // Skapa ett temporärt objekt med rätt namn så att switch-satsen fungerar
            GameObject tempItem = new GameObject(itemName);
            hoverScript.currentItem = tempItem;

            // Sätt hoverBox och hoverText från ditt InventoryManager eller drag in i Inspector
            hoverScript.hoverBox = this.hoverBox;
            hoverScript.hoverText = this.hoverText;
        }

        // Hämta Button-komponenten på prefab-instansen
        Button iconButton = newIconObject.GetComponent<Button>();

        // Lägg till en onClick-listener
        if (iconButton != null)
        {
            string capturedItemName = itemName;
            iconButton.onClick.AddListener(() => OnInventoryItemClicked(capturedItemName));
        }
    }


    void OnInventoryItemClicked(string itemName)
    {
        Debug.Log("Klickade på: " + itemName);

        
        switch (itemName)
        {
            case "Key_Office_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Key_Decorative_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Key_Gen_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Key_Front_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Note_1_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Key_Gate_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Flashlight_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Key_Masterbedroom_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Matches_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Newspaper_Inspect":
                InspectFromInventory (itemName);
                break;
            case "Key_Basement_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Note_2_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Note_3_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Note_4_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Note_Diary_Inspect":
                InspectFromInventory(itemName);
                break;
            case "Note_Final_Inspect":
                InspectFromInventory(itemName);
                break;

            default:
                Debug.Log("");
                break;
        }
    }



    public void InspectFromInventory(string itemName)
    {
        
        CloseInventory();

        
        GameObject prefab = GetItemPrefab(itemName);
        if (prefab == null) return;

        
        Vector3 spawnPos = inspectCamera.transform.position + inspectCamera.transform.forward * currentZoom;
        GameObject spawned = Instantiate(prefab, spawnPos, Quaternion.identity);
        inspectingFromInventory = true;

        
        StartInspectMode(spawned.transform);

        UpdateUIForItem(itemName, spawned.gameObject );
    }



    Sprite GetItemIcon(string itemName)
    {
        // Returnera rätt ikon baserat på objektets namn
        switch (itemName)
        {
            case "Flashlight_Inspect":
                return flashlightIcon; 
            case "Key_Decorative_Inspect":
                return Key_Icon;
            case "Key_Gen_Inspect":
                return Key_Icon;
            case "Key_Front_Inspect":
                return Key_Icon;
            case "Key_Gate_Inspect":
                return Key_Icon;
            case "Key_Office_Inspect":
                return Key_Icon;
            case "Note_1_Inspect":
                return Note_Icon;
            case "Key_Masterbedroom_Inspect":
                return Key_Icon;
            case "Matches_Inspect":
                return Matches_Icon;
            case "Newspaper_Inspect":
                return Note_Icon;
            case "Key_Basement_Inspect":
                return Key_Icon;
            case "Note_2_Inspect":
                return Note_Icon;
            case "Note_3_Inspect":
                return Note_Icon;
            case "Note_4_Inspect":
                return Note_Icon;
            case "Note_Diary_Inspect":
                return Note_Icon;
            case "Note_Final_Inspect":
                return Note_Icon;
            default:
                return defaultIcon; 
        }
    }


    // Kollar om spelaren har ett specifikt objekt
    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }


    public void resetUI()
    {
        BackgroundUI.SetActive(false);
        PickupUI.SetActive(false);
        readUI.SetActive(false);
    }

    public bool HasKey(string keyName)
    {
        return inventory.Contains(keyName);
    }

    void UpdateUIForItem(string itemName, GameObject inspectedItem)
    {
        foreach (var ui in uiElements)
            ui.SetActive(false);

        BackgroundUI.SetActive(true);

        PickupUI.SetActive(false);
        readUI.SetActive(false);

        switch (itemName)
        {
            case "Book_A_Inspect":
                uiElements[0].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.75f;
                maxZoom = 1f;
                break;
            case "Book_B_Inspect":
                uiElements[1].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.5f;
                maxZoom = 1f;
                break;
            case "Key_Front_Inspect":
                uiElements[2].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.4f;
                maxZoom = 0.5f;
                break;
            case "Key_Decorative_Inspect":
                uiElements[3].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.4f;
                maxZoom = 0.5f;
                break;
            case "Key_Gen_Inspect":
                uiElements[4].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.45f;
                maxZoom = 0.55f;
                break;
            case "Key_Office_Inspect":
                uiElements[5].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.45f;
                maxZoom = 0.55f;
                break;
            case "Flashlight_Inspect":
                uiElements[6].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.55f;
                maxZoom = 0.8f;
                break;
            case "Note_1_Inspect":
                uiElements[7].SetActive(true);
                readUI.SetActive(true);
                minZoom = 0.7f;
                maxZoom = 1f;
                break;
            case "Globe_Inspect":
                uiElements[8].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.65f;
                maxZoom = 0.9f;
                break;
            case "Wine_Inspect":
                uiElements[9].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.7f;
                maxZoom = 0.9f;
                break;
            case "Key_Gate_Inspect":
                uiElements[10].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.5f;
                maxZoom = 0.8f;
                break;
            case "Key_Masterbedroom_Inspect":
                uiElements[11].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.5f;
                maxZoom = 0.8f;
                break;
            case "Matches_Inspect":
                uiElements[12].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.5f;
                maxZoom = 0.7f;
                break;
            case "Newspaper_Inspect":
                uiElements[13].SetActive(true);
                readUI.SetActive(true);
                minZoom = 0.8f;
                maxZoom = 1.2f;
                break;
            case "Pills1F_Inspect":
                uiElements[14].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.5f;
                maxZoom = 0.5f;
                break;
            case "Pills2F_Inspect":
                uiElements[14].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.5f;
                maxZoom = 0.5f;
                break;
            case "PillsKitchen_Inspect":
                uiElements[14].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.5f;
                maxZoom = 0.5f;
                break;
            case "Key_Basement_Inspect":
                uiElements[15].SetActive(true);
                readUI.SetActive(false);
                minZoom = 0.5f;
                maxZoom = 0.7f;
                break;
            case "Note_2_Inspect":
                uiElements[16].SetActive(true);
                readUI.SetActive(true);
                minZoom = 0.7f;
                maxZoom = 1.0f;
                break;
            case "Note_3_Inspect":
                uiElements[17].SetActive(true);
                readUI.SetActive(true);
                minZoom = 0.7f;
                maxZoom = 1.0f;
                break;
            case "Note_4_Inspect":
                uiElements[18].SetActive(true);
                readUI.SetActive(true);
                minZoom = 0.7f;
                maxZoom = 1.0f;
                break;
            case "Note_Diary_Inspect":
                uiElements[19].SetActive(true);
                readUI.SetActive(true);
                minZoom = 0.7f;
                maxZoom = 1.0f;
                break;
            case "Note_Final_Inspect":
                uiElements[20].SetActive(true);
                readUI.SetActive(true);
                minZoom = 0.7f;
                maxZoom = 1.0f;
                break;


            default:
                break;
        }

        
        if (inspectedItem != null && inspectedItem.CompareTag("Pickable"))
        {
            PickupUI.SetActive(true);
            
        }
        else
        {
            PickupUI.SetActive(false);
            

        }

        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        PositionObjectForInspection();
    }


}