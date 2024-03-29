using System;
using Scene;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// A component of RedDot.prefab
    /// </summary>
    public class RedDotAudioPlayer : AudioPlayer
    {
        private RedDotBehaviour parentRedDot;

        protected override void Awake()
        {
            base.Awake();

            if(transform.parent.TryGetComponent(out RedDotBehaviour component))
            {
                parentRedDot = component;
            }
            else
            {
                Debug.LogWarning(this + " is missing parent.BlueCircle component");
            }
        }
        
        private void OnEnable()
        {
            parentRedDot.Interacting += OnInteracting;
        }
        
        private void OnDestroy()
        {
            parentRedDot.Interacting -= OnInteracting;
        }
        
        private void OnInteracting(object sender, EventArgs e)
        {
            PlayRandomAudio();
        }
    }
}
