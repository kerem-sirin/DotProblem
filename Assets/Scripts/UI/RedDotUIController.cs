using Scene;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Listens <see cref="ValueBox"/>> events and executes actions with <see cref="RedDotSizer"/>
    /// and <see cref="RedDotGenerator"/> public methods
    /// </summary>
    public class RedDotUIController : MonoBehaviour
    {
        [Header("Button References")] 
        [SerializeField] private ValueBox changeRedDotCountValueBox;
        [SerializeField] private ValueBox changeRedDotSizeValueBox;

        private void Awake()
        {
            changeRedDotCountValueBox.DecreasePressed += OnDecreaseRedDotCountPressed;
            changeRedDotCountValueBox.IncreasePressed += OnIncreaseRedDotCountPressed;
            
            changeRedDotSizeValueBox.DecreasePressed += OnDecreaseRedDotSizePressed;
            changeRedDotSizeValueBox.IncreasePressed += OnIncreaseRedDotSizePressed;
        }

        private void OnDestroy()
        {
            changeRedDotCountValueBox.DecreasePressed -= OnDecreaseRedDotCountPressed;
            changeRedDotCountValueBox.IncreasePressed -= OnIncreaseRedDotCountPressed;
            
            changeRedDotSizeValueBox.DecreasePressed -= OnDecreaseRedDotSizePressed;
            changeRedDotSizeValueBox.IncreasePressed -= OnIncreaseRedDotSizePressed;
        }

        private void OnIncreaseRedDotSizePressed()
        {
            RedDotSizer.Instance.ChangeRedDotSize(true);
            changeRedDotSizeValueBox.UpdateButtonInteraction(RedDotSizer.Instance.IsMinSizeReached,
                RedDotSizer.Instance.IsMaxSizeReached);
        }

        private void OnDecreaseRedDotSizePressed()
        {
            RedDotSizer.Instance.ChangeRedDotSize(false);
            changeRedDotSizeValueBox.UpdateButtonInteraction(RedDotSizer.Instance.IsMinSizeReached,
                RedDotSizer.Instance.IsMaxSizeReached);
        }

        private void OnIncreaseRedDotCountPressed()
        {
            RedDotGenerator.Instance.AddRedDotToList();
            changeRedDotCountValueBox.UpdateButtonInteraction(RedDotGenerator.Instance.IsMinCountReached,
                RedDotGenerator.Instance.IsMaxCountReached);
        }

        private void OnDecreaseRedDotCountPressed()
        {
            RedDotGenerator.Instance.RemoveRedDot();
            changeRedDotCountValueBox.UpdateButtonInteraction(RedDotGenerator.Instance.IsMinCountReached,
                RedDotGenerator.Instance.IsMaxCountReached);
        }
        
    }
}
