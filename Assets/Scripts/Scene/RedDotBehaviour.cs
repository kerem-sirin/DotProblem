using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scene
{
    /// <summary>
    /// Attached to RedDot.prefab. Handles gameObjects instantiation and size on runtime.
    /// Speed is changed by <see cref="RedDotUIController"/>
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class RedDotBehaviour : MonoBehaviour
    {
        public event EventHandler Interacting;
    
        [Header("Preferences")]
        [SerializeField] private Color intersectionActiveColor;
    
        [Header("Prefabs")]
        [SerializeField] private Transform interactEffect;
    
        private Color defaultColor;
        private float intersectionDistance;
        private bool isIntersecting;
        private float distanceToBlueCircle;
        private bool canPlayIntersectionEffect = true;
        
        private BlueDotBehaviour blueDotBehaviour;
        private RectTransform rect;
        private Image image;
        
        private void Awake()
        {
            blueDotBehaviour = FindObjectOfType<BlueDotBehaviour>();
            rect = GetComponent<RectTransform>();
            image = GetComponent<Image>();

            intersectionDistance = (rect.sizeDelta.x + blueDotBehaviour.CircleRadius) / 2f;
            defaultColor = image.color;
        }

        private void Start()
        {
            SetRadius(RedDotSizer.Instance.CurrentRadius);
            SetPosition(RedDotSizer.Instance.GetRandomVectorOnScreenspace());
        }
        
        private void Update()
        {
            Vector2 selfPos = new Vector2(rect.position.x, rect.position.y);
            distanceToBlueCircle = Vector2.Distance(selfPos, blueDotBehaviour.CirclePos);
            isIntersecting = distanceToBlueCircle < intersectionDistance;
        
            if (isIntersecting)
            {
                float distanceToBlueDotNormalized = distanceToBlueCircle / intersectionDistance;
                LerpColorOnDistance(distanceToBlueDotNormalized);

                if (!canPlayIntersectionEffect) return;
                
                canPlayIntersectionEffect = false;
                Interacting?.Invoke(this, EventArgs.Empty);
                PlayIntersectionEffect();
            }
            else
            {
                image.color = defaultColor;
                canPlayIntersectionEffect = true;
            }
        }
        
        public void SetRadius(float currentRadius)
        {
            rect.sizeDelta = new Vector2(currentRadius, currentRadius);
            intersectionDistance = (rect.sizeDelta.x + blueDotBehaviour.CircleRadius) / 2f;
        }
        
        public void SelfDestroy()
        {
            Destroy(gameObject);
        }

        private void LerpColorOnDistance(float distanceToBlueDotNormalized)
        {
            image.color = Color.Lerp(intersectionActiveColor, defaultColor, distanceToBlueDotNormalized);
        }
        
        private void SetPosition(Vector2 newPositionVector2)
        {
            rect.anchoredPosition = newPositionVector2;
            
        }

        private void PlayIntersectionEffect()
        {
            Transform intersectionEffectTransform = Instantiate(interactEffect, transform.position, Quaternion.identity, transform);
            intersectionEffectTransform.gameObject.GetComponent<RectTransform>().sizeDelta =
                new Vector2(rect.sizeDelta.x, rect.sizeDelta.y);
        }
    }
}