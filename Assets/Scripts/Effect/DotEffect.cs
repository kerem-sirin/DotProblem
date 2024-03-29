using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Effect
{
    /// <summary>
    /// A component of DotEffect.prefab. Instantiated on runtime
    /// Changes the scale and opacity properties.
    /// Destroys gameObject, on tween completed
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class DotEffect : MonoBehaviour
    {
        [SerializeField] private float effectDuration;
        [SerializeField] private float finalDotScale;
        
        private Color initialColor;
        private Color finalColor;

        private Image image;
        private RectTransform rect;
    
        private void Awake()
        {
            image = GetComponent<Image>();
            rect = GetComponent<RectTransform>();
            
            initialColor = image.color;
            finalColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        }
        
        void Start()
        {
            image.DOColor(finalColor, effectDuration).SetEase(Ease.Linear);

            rect.DOScale(finalDotScale, effectDuration).SetEase(Ease.OutCirc).
                OnComplete(()=> Destroy(gameObject));
        }
    }
}
