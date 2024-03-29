using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Used to show-hide top panel menu
    /// </summary>
    public class TopPanelController : MonoBehaviour
    {
        [Header("Button References")]
        [SerializeField] private Button toggleTopMenuButton;

        private static readonly int Show = Animator.StringToHash("Show");
        private Animator animator;
        private bool isTopPanelShowing = true;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            
            toggleTopMenuButton.onClick.AddListener(ToggleTopMenu);
        }
        
        private void OnDestroy()
        {
            toggleTopMenuButton.onClick.AddListener(ToggleTopMenu);
        }

        private void ToggleTopMenu()
        {
            isTopPanelShowing = !isTopPanelShowing;
            animator.SetBool(Show,isTopPanelShowing);
        }
    }
}