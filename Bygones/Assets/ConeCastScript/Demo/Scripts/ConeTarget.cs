using UnityEngine;

namespace ConeCastScript.Demo
{
    public class ConeTarget : MonoBehaviour
    {
        private const string ColorParam = "_Color";

        private MeshRenderer meshRenderer;
        private float timeAccumulator;
        private float colorResetTime;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetColor(Color color)
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (!meshRenderer)
            {
                return;
            }

            meshRenderer.material.SetColor(ColorParam, color);
            timeAccumulator = 0.0f;
        }

        private void Update()
        {
            colorResetTime = 3 * Time.deltaTime;
            timeAccumulator += Time.deltaTime;
            if (timeAccumulator > colorResetTime)
            {
                if (meshRenderer)
                {
                    meshRenderer.material.SetColor(ColorParam, Color.white);
                }
                timeAccumulator = 4 * Time.deltaTime;
            }
        }
    }
}
