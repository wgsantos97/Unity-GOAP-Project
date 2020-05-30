using UnityEngine;

namespace GameDevLibrary.AI.GOAP
{
    public class GStateMonitor : MonoBehaviour
    {
        [SerializeField] string state = "";
        [SerializeField] float stateStrength = 10f;
        [SerializeField] float stateDecayRate = 5f;
        [SerializeField] WorldStates beliefs = null;
        [SerializeField] GameObject resourcePrefab = null;
        [SerializeField] string queueName = "";
        [SerializeField] string worldState = "";
        [SerializeField] GAction action = null;

        bool stateFound = false;
        float initialStrength = 0f;

        void Awake()
        {
            beliefs = this.GetComponent<GAgent>().beliefs;
            initialStrength = stateStrength;
        }

        void LateUpdate()
        {
            if(action.running)
            {
                stateFound = false;
                stateStrength = initialStrength;
            }

            if (!stateFound && beliefs.hasState(state))
                stateFound = true;

            if(stateFound)
            {
                stateStrength -= stateDecayRate * Time.deltaTime;
                if(stateStrength <= 0)
                {
                    Vector3 location = new Vector3(transform.position.x, resourcePrefab.transform.position.y, transform.position.z);
                    GameObject p = Instantiate(resourcePrefab, location, resourcePrefab.transform.rotation);
                    stateFound = false;
                    stateStrength = initialStrength;
                    beliefs.RemoveState(state);
                    GWorld.Instance.GetQueue(queueName).AddResource(p);
                    GWorld.Instance.worldStates.ModifyState(worldState, 1);
                }
            }
        }
    }
}
