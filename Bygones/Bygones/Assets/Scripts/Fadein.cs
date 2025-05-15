using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadein : MonoBehaviour
{
    [SerializeField] Renderer targetRenderer;
    [SerializeField] float fadeDuration = 2f;
    bool isFading = false;
    Coroutine fadeCoroutine;

    Material material;

    void Awake()
    {
        if (targetRenderer != null)
        {
            material = targetRenderer.material;
            SetAlpha(0f); // Start invisible
            SetupMaterialForTransparency();
        }
        else
        {
            Debug.LogWarning("No Renderer assigned to ItemFadeIn!");
        }
    }
    public void StartFadeIn()
    {
        if (material != null && !isFading)
        {
            fadeCoroutine = StartCoroutine(FadeInCoroutine());
        }
    }

    //public void StopFadeIn()
    //{
    //        if (material != null)
    //        {
    //            if (fadeCoroutine != null)
    //            {
               
    //                StopCoroutine(fadeCoroutine);
    //                fadeCoroutine = null;
    //            }
    //            isFading = false;
    //        }
    //}

    public void ResetFade()
    {
        if(material != null)
        {
            if(fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
                fadeCoroutine = null;
            }
            SetAlpha(0f);
            isFading = false;
        }
    }
    IEnumerator FadeInCoroutine()
    {
        isFading = true;
        float elapsedTime = 0f;
        float startAlpha = material.color.a;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 1f, elapsedTime / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(1f);
        isFading = false;
        fadeCoroutine = null;   
    }

    public void SetAlpha(float alpha)
    {
        if (material != null)
        {
            if (!material.HasProperty("_Color"))
            {
                Debug.LogWarning("Material does not have '_Color' property");
                return;
            }
            Color color = material.color;
            color.a = alpha;
            material.color = color;
        }
    }

    private void SetupMaterialForTransparency()
    {
        if (material != null)
        {
            material.SetFloat("_Mode", 2f);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
        }
    }
}
