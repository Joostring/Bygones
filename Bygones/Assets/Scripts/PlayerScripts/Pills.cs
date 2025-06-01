// Author Mikael

using UnityEngine;

public class Pills : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LowSanityTimer lowSanityTimer;
    [SerializeField] int sanityGet = 5;
    [SerializeField] private InspectSystem inspectSystem;
    [SerializeField] bool hasBeenTaken1 = false;
    [SerializeField] bool hasBeenTaken2 = false;
    [SerializeField] bool hasBeenTaken3 = false;

    // Update is called once per frame
    void Update()
    {
        if (inspectSystem.HasItem("Pills1F_Inspect") && !hasBeenTaken1)
        {
            Debug.Log("Attempting to get pill sanity");
            lowSanityTimer.SanityGain(sanityGet);
            hasBeenTaken1 = true;
        }
        if (inspectSystem.HasItem("Pills2F_Inspect") && !hasBeenTaken2)
        {
            lowSanityTimer.SanityGain(sanityGet);
            hasBeenTaken2 = true;
        }
        if (inspectSystem.HasItem("PillsKitchen_Inspect") && !hasBeenTaken3)
        {
            lowSanityTimer.SanityGain(sanityGet);
            hasBeenTaken3 = true;
        }
    }


}

