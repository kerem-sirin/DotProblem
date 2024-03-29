using Scene;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Listens <see cref="ValueBox"/>> events and executes actions with <see cref="BlueDotProvider"/> public methods
    /// </summary>
    public class BlueDotUIController : MonoBehaviour
    {
        [SerializeField] private ValueBox changeBlueDotSpeedValueBox;

        private void Awake()
        {
            changeBlueDotSpeedValueBox.DecreasePressed += OnDecreaseBlueDotSpeedPressed;
            changeBlueDotSpeedValueBox.IncreasePressed += OnIncreaseBlueDotSpeedPressed;
        }

        private void OnDestroy()
        {
            changeBlueDotSpeedValueBox.DecreasePressed -= OnDecreaseBlueDotSpeedPressed;
            changeBlueDotSpeedValueBox.IncreasePressed -= OnIncreaseBlueDotSpeedPressed;
        }

        private void OnIncreaseBlueDotSpeedPressed()
        {
            BlueDotProvider.Instance.BlueDotBehaviour.ChangeSpeed(true);
            changeBlueDotSpeedValueBox.UpdateButtonInteraction(BlueDotProvider.Instance.BlueDotBehaviour.IsMinSpeedReached, 
                BlueDotProvider.Instance.BlueDotBehaviour.IsMaxSpeedReached);
        }

        private void OnDecreaseBlueDotSpeedPressed()
        {
            BlueDotProvider.Instance.BlueDotBehaviour.ChangeSpeed(false);
            changeBlueDotSpeedValueBox.UpdateButtonInteraction(BlueDotProvider.Instance.BlueDotBehaviour.IsMinSpeedReached, 
                BlueDotProvider.Instance.BlueDotBehaviour.IsMaxSpeedReached);
        }
    }
}
