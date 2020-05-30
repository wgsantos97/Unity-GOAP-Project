using UnityEngine;

namespace GameDevLibrary.AI.GOAP
{
    public class GActionInventory : MonoBehaviour
    {
        [SerializeField] GameObject inventory = null;
        public GAction[] actions { get; protected set; }

        void Awake()
        {
            actions = inventory.GetComponents<GAction>();
        }
    }
}
