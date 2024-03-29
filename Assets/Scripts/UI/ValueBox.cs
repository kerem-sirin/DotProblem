using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Component of ValueBox.prefab
    /// Used for add/remove, increase/decrease type paired functionalities
    /// </summary>
    public class ValueBox : MonoBehaviour
    {
        [SerializeField] private Button decreaseButton;
        [SerializeField] private Button increaseButton;

        public event Action DecreasePressed;
        public event Action IncreasePressed;
        
        private void Awake()
        {
            decreaseButton.onClick.AddListener(()=>DecreasePressed?.Invoke());
            increaseButton.onClick.AddListener(()=>IncreasePressed?.Invoke());
        }

        private void OnDestroy()
        {
            decreaseButton.onClick.RemoveListener(()=>DecreasePressed?.Invoke());
            increaseButton.onClick.RemoveListener(()=>IncreasePressed?.Invoke());
        }

        public void UpdateButtonInteraction(bool canDecrease, bool canIncrease)
        {
            decreaseButton.interactable = !canDecrease;
            increaseButton.interactable = !canIncrease;
        }
    }
}
