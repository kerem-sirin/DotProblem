using UnityEngine;

namespace Scene
{
    /// <summary>
    /// Presents <see cref="BlueDotBehaviour"/> on the scene
    /// </summary>
    public class BlueDotProvider : MonoBehaviour
    {
        public static BlueDotProvider Instance { get; private set; }

        [SerializeField] private BlueDotBehaviour blueDotBehaviour;

        public BlueDotBehaviour BlueDotBehaviour => blueDotBehaviour;
        
        private void Awake() 
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }
    }
}
