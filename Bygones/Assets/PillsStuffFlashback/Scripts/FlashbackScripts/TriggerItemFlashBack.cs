using UnityEngine;

public class TriggerItemFlashBack : MonoBehaviour
{
    [SerializeField] MoveObjectUp moveUp;
    [SerializeField] TriggerFlashBack triggerFlashBack;
    public Transform interactorSource;
    public float interactRange = 3f;
    public LayerMask interactableLayer;
    public bool hasTriggerdFlashback = false;


    void Update()
    {

        if (!hasTriggerdFlashback)
        {
            Ray ray = new Ray(interactorSource.position, interactorSource.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, interactRange, interactableLayer))
            {
                if (Input.GetKeyDown(KeyCode.E) && hitInfo.collider.gameObject == gameObject)
                {
                    moveUp.StartRise();
                    triggerFlashBack.enabled = true;
                    hasTriggerdFlashback = true;
                }
            }
        }
    }

}