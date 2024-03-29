using Scene;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// A component of BlueDot.prefab
    /// </summary>
    public class BlueDotAudioPlayer : AudioPlayer
    {
        private BlueDotBehaviour parentBlueDotBehaviour;

        protected override void Awake()
        {
            base.Awake();

            if(transform.parent.TryGetComponent(out BlueDotBehaviour component))
            {
                parentBlueDotBehaviour = component;
            }
            else
            {
                Debug.LogWarning(this + " is missing parent.BlueCircle component");
            }
        }
        
        private void OnEnable()
        {
            parentBlueDotBehaviour.Bouncing += PlayRandomAudio;
        }

        private void OnDestroy()
        {
            parentBlueDotBehaviour.Bouncing -= PlayRandomAudio;
        }
    }
}
