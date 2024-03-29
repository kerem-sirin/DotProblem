using System;
using App;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scene
{
    /// <summary>
    /// Handles RedDot.prefab scale changes on runtime.
    /// </summary>
    public class RedDotSizer : MonoBehaviour
    {
        private const float MinCircleRadius = 50f;
        private const float MaxCircleRadius = 350f;
        private const float RadiusChangeAmount = 50f;
        
        private float currentRadius = 100f;
        public bool IsMinSizeReached { get; private set; }
        public bool IsMaxSizeReached { get; private set; }
        public float CurrentRadius => currentRadius;
        public static RedDotSizer Instance { get; private set; }
        
        private ResolutionViewer resolutionViewer;
        
        private void Awake() 
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }

        private void Start()
        {
            resolutionViewer = ResolutionViewer.Instance;
        }

        public void ChangeRedDotSize(bool increaseRadius)
        {
            SetRadius(increaseRadius);
        }
        
        public Vector2 GetRandomVectorOnScreenspace()
        {
            float randomRedDotPositionX = Random.Range(-resolutionViewer.ScreenResolution.x / 2f + currentRadius / 2f, 
                resolutionViewer.ScreenResolution.x / 2f - currentRadius / 2f);
            float randomRedDotPositionY = Random.Range(-resolutionViewer.ScreenResolution.y / 2f + currentRadius / 2f,
                resolutionViewer.ScreenResolution.y / 2f - currentRadius / 2f);
            Vector2 randomRedDotPosition = new Vector3(randomRedDotPositionX, randomRedDotPositionY);

            return randomRedDotPosition;
        }

        private void SetRadius(bool increaseRadius)
        {
            if (increaseRadius)
            {
                float newRadius = currentRadius + RadiusChangeAmount;
                if (newRadius < MaxCircleRadius || Math.Abs(newRadius - MaxCircleRadius) < 1f)
                {
                    currentRadius = newRadius;
                }
            }
            else
            {
                float newRadius = currentRadius - RadiusChangeAmount;
                if (newRadius > MinCircleRadius || Math.Abs(newRadius - MinCircleRadius) < 1f)
                {
                    currentRadius = newRadius;
                }
            }

            IsMinSizeReached = Mathf.Approximately(MinCircleRadius, currentRadius);
            IsMaxSizeReached = Mathf.Approximately(MaxCircleRadius, currentRadius);
            
            UpdateDotRadius();
        }

        private void UpdateDotRadius()
        {
            foreach (RedDotBehaviour redDotBehaviour in RedDotStorage.Instance.RedDotBehaviourList)
            {
                redDotBehaviour.SetRadius(currentRadius);
            }
        }
    }
}
