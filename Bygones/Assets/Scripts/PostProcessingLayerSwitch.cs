using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingLayerSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PostProcessLayer postProcessLayer;
    [SerializeField] LayerMask defaultLayer;
    [SerializeField] LayerMask greyScaleLayer;
    [SerializeField] LayerMask sanityLayer;

    private bool isLayer = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            isLayer = !isLayer;
            postProcessLayer.volumeLayer = isLayer ? greyScaleLayer : defaultLayer;
        }
    }
}
