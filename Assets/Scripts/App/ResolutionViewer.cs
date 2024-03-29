using UnityEngine;

namespace App
{
    /// <summary>
    /// Parent gameObject, updates main rect transform size
    /// </summary>
    public class ResolutionViewer : MonoBehaviour
    {
        public static ResolutionViewer Instance;

        private Vector2 screenResolution;
        public Vector2 ScreenResolution => screenResolution;

        private RectTransform rect;
        private RectTransform parentRect;

        private void Awake()
        {
            Instance = this;
        
            rect = GetComponent<RectTransform>();
            parentRect = transform.parent.GetComponent<RectTransform>();

            screenResolution = rect.sizeDelta;

            screenResolution = parentRect.sizeDelta;
            rect.sizeDelta = screenResolution;
            rect.anchoredPosition = new Vector2(0f, -(Screen.height - screenResolution.y) / 2f);
        }
    }
}