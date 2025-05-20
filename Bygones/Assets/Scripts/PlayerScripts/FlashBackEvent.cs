using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FlashBackEvent : MonoBehaviour
{
    [SerializeField] PostProcessLayer postProcessLayer;
    [SerializeField] LowSanityTimer lowSanityTimer;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask defaultLayer;
    [SerializeField] LayerMask greyLayer;
    [SerializeField] GameObject flashbackPanel;
    [SerializeField] public TMP_Text flashbackText;
    private bool isFlashBack = false;
    [SerializeField] float flashbackTimer = 0f;
    private TriggerFlashBack currentTriggerFlashback;
    public int currentTextIndex = 0;
    private Animator flashbackAnimator;

    void Start()
    {
        if (flashbackPanel != null)
        {
            flashbackPanel.SetActive(false);
            flashbackAnimator = flashbackText.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashBack && currentTriggerFlashback != null)
        {

            flashbackTimer += Time.deltaTime;
            if (flashbackTimer >= currentTriggerFlashback.textDisplayDuration)
            {
                AdvanceFlashbackText();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AdvanceFlashbackText();
                flashbackAnimator.SetTrigger("ResetFlashback");
            }

        }
    }

    public void StartFlashback(TriggerFlashBack triggerFlashback)
    {
        isFlashBack = true;
        currentTriggerFlashback = triggerFlashback;
        triggerFlashback.hasTriggerdFlashback = true;
        playerMovement.SetMovementState(false);
        currentTextIndex = 0;

        if (postProcessLayer != null && greyLayer != null)
        {
            postProcessLayer.volumeLayer = greyLayer;
        }

        if (lowSanityTimer != null)
        {
            lowSanityTimer.SanityDrainChecker(false);
        }
        flashbackAnimator.SetTrigger("ResetFlashback");
        DisplayCurrentText();
        flashbackTimer = 0f;
    }
    public void AdvanceFlashbackText()
    {
        currentTextIndex++;
        if (currentTextIndex < currentTriggerFlashback.flashbackTexts.Count)
        {
            DisplayCurrentText();
            flashbackTimer = 0f;
        }
        else
        {
            EndFlashBack();
        }
    }

    public void DisplayCurrentText()
    {
        if (flashbackPanel != null && flashbackText != null && currentTextIndex < currentTriggerFlashback.flashbackTexts.Count)
        {
            flashbackText.text = currentTriggerFlashback.flashbackTexts[currentTextIndex];
            flashbackPanel.SetActive(true);

        }
        else
        {
            EndFlashBack();
        }
    }
    public void EndFlashBack()
    {
        if (!isFlashBack)
        {
            return;
        }

        isFlashBack = false;
        playerMovement.SetMovementState(true);
        currentTextIndex = 0;

        if (postProcessLayer != null && defaultLayer != null)
        {
            postProcessLayer.volumeLayer = defaultLayer;
        }
        if (lowSanityTimer != null)
        {
            lowSanityTimer.SanityDrainChecker(true);
        }
        if (flashbackPanel != null)
        {
            flashbackPanel.SetActive(false);
        }
        currentTriggerFlashback = null;
    }
}
