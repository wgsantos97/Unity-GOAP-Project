using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameDevLibrary.AI.GOAP
{
    public abstract class GAction : MonoBehaviour
    {
        [SerializeField] string actionName = "Action";
        public string ActionName => actionName;
        public float cost = 1.0f;
        public GameObject target;
        public string targetTag { get; protected set; }
        public float duration { get; protected set; } = 0f;
        [SerializeField] WorldState[] preConditions = null;
        [SerializeField] WorldState[] afterEffects = null;
        public NavMeshAgent agent { get; protected set; }

        public Dictionary<string, int> preconditions { get; protected set; }
        public Dictionary<string, int> effects { get; protected set; }

        public GInventory inventory { get; protected set; }
        public WorldStates beliefs { get; protected set; }

        public bool running;

        void Awake()
        {
            preconditions = new Dictionary<string, int>();
            effects = new Dictionary<string, int>();

            agent = transform.parent.GetComponent<NavMeshAgent>();

            if(preConditions!=null)
            {
                foreach(WorldState w in preConditions)
                {
                    preconditions.Add(w.key, w.value);
                }
            }

            if (afterEffects != null)
            {
                foreach (WorldState w in afterEffects)
                {
                    effects.Add(w.key, w.value);
                }
            }

            inventory = transform.parent.GetComponent<GAgent>().inventory;
            beliefs = transform.parent.GetComponent<GAgent>().beliefs;
        }

        public virtual bool IsAchievable()
        {
            return true;
        }

        public bool IsAchievableGiven(Dictionary<string, int> conditions)
        {
            foreach(KeyValuePair<string,int> condition in preconditions)
            {
                if (!conditions.ContainsKey(condition.Key))
                {
                    return false;
                }
            }
            return true;
        }

        public abstract bool PrePerform();
        public abstract bool PostPerform();
    }
}
