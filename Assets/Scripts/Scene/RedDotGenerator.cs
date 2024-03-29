using UnityEngine;

namespace Scene
{
    /// <summary>
    /// Handles RedDot.prefab instantiation and deletion on runtime.
    /// </summary>
    public class RedDotGenerator : MonoBehaviour
    {
        private const int StartingRedDotAmount = 5;
        private const int MaxRedDotAmount = 20;

        public bool IsMinCountReached { get; private set; }
        public bool IsMaxCountReached { get; private set; }
        public static RedDotGenerator Instance { get; private set; }

        [Header("Scene References")] 
        [SerializeField] private Transform redDotParent;

        [Header("Prefabs")] [SerializeField] private Transform redDot;

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

        private void Start()
        {
            InstantiateStartingDots();
        }

        public void AddRedDotToList()
        {
            Transform newRedDotGo = Instantiate(redDot, redDotParent);
            RedDotBehaviour newRedDotBehaviour = newRedDotGo.GetComponent<RedDotBehaviour>();

            RedDotStorage.Instance.AddDot(newRedDotBehaviour);

            UpdateCountReachedProperties();
        }

        public void RemoveRedDot()
        {
            RedDotStorage.Instance.RemoveDot();

            UpdateCountReachedProperties();
            
        }
        private void InstantiateStartingDots()
        {
            for (int i = 0; i < StartingRedDotAmount; i++)
            {
                AddRedDotToList();
            }
        }
        
        private void UpdateCountReachedProperties()
        {
            IsMinCountReached = RedDotStorage.Instance.RedDotBehaviourList.Count == 0;
            IsMaxCountReached = RedDotStorage.Instance.RedDotBehaviourList.Count == MaxRedDotAmount;
        }
    }
}