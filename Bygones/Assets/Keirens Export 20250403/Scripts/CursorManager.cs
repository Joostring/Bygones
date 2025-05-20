using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class CursorManager : MonoBehaviour
    {
        public static CursorManager instance;

        public Sprite defaultCursor;
        public Sprite lockedCursor;
        public Sprite doorCursor;
        public Sprite pickupCursor;

        private UnityEngine.UI.Image img;
        public float maxDistance = 5f; // Maximum distance to check
        public Camera cam; // Assign the player's camera
        public GameObject player; // Reference to the player
        public LayerMask interactableLayer; // Assign this to the "Interactable" layer in the Inspector

        void Awake()
        {
            instance = this;
            img = GetComponent<UnityEngine.UI.Image>();
        }

        void Update()
        {
            CursorHint();
        }

        void CursorHint()
        {
            if (player == null || cam == null) return;

            // Cast a ray from the center of the screen
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // Check if the object is on the "Interactable" layer
                if (((1 <<hit.collider.gameObject.layer) & interactableLayer) != 0)
                {
                    img.rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    SetCursorToPickup();
                //Debug.Log("HandCursor");
            }
                else if (hit.collider.CompareTag("Locked"))
                {
                    img.rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    SetCursorToLocked();
                //Debug.Log("LockedCursor");
            }
                else if (hit.collider.CompareTag("Open"))
                {
                    img.rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    SetCursorToDoor();
                //Debug.Log("DoorCursor");
            }
                else
                {
                    img.rectTransform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    SetCursorToDefault();
                //Debug.Log("DefaultCursor");
                }
            }
            else
            {
                img.rectTransform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                SetCursorToDefault();
            }
        }

        public void SetCursorToLocked()
        {
            img.sprite = lockedCursor;
        }

        public void SetCursorToDoor()
        {
            img.sprite = doorCursor;
        }

        public void SetCursorToDefault()
        {
            img.sprite = defaultCursor;
        }

        public void SetCursorToPickup()
        {
            img.sprite = pickupCursor;
        }
    }

