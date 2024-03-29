using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    /// <summary>
    /// A component of ButtonAudioPlayer.prefab
    /// This generic class can be used for any buttons on the scene
    /// </summary>
    public class ButtonAudioPlayer : AudioPlayer
    {
        private Button parentButton;

        protected override void Awake()
        {
            base.Awake();

            if(transform.parent.TryGetComponent(out Button component))
            {
                parentButton = component;
            }
            else
            {
                Debug.LogWarning(this + " is missing parent.Button component");
            }
        }

        private void OnEnable()
        {
            parentButton.onClick.AddListener(PlayRandomAudio);
        }

        private void OnDestroy()
        {
            parentButton.onClick.RemoveListener(PlayRandomAudio);
        }
    }
}
