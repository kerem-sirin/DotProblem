using System.Collections.Generic;
using UnityEngine;

namespace Scene
{
    /// <summary>
    /// Handles storing <see cref="RedDotBehaviour"/> to a list and presenting the list
    /// </summary>
    public class RedDotStorage : MonoBehaviour
    {
        public static RedDotStorage Instance { get; private set; }
        
        private List<RedDotBehaviour> redDotBehaviourList = new List<RedDotBehaviour>();
        public List<RedDotBehaviour> RedDotBehaviourList => redDotBehaviourList;
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

        public void AddDot(RedDotBehaviour redDotBehaviour)
        {
            redDotBehaviourList.Add(redDotBehaviour);
        }

        public void RemoveDot()
        {
            RedDotBehaviour redDotBehaviourToDelete = redDotBehaviourList[Random.Range(0, redDotBehaviourList.Count)];
            redDotBehaviourList.Remove(redDotBehaviourToDelete);
            redDotBehaviourToDelete.SelfDestroy();
        }
    }
}
