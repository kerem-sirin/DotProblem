using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// A component of AudioPlayer.prefab.
    /// Further required audio related methods can be implemented (ie PlayLoop, StopLoop, SetVolume, Mute etc)
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> audioClipList;

        private AudioSource audioSource;

        protected virtual void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        protected void PlayRandomAudio()
        {
            AudioClip audioClip = audioClipList[Random.Range(0, audioClipList.Count)];
            audioSource.PlayOneShot(audioClip);
        }
    }
}
