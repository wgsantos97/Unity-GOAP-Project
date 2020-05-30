using UnityEngine;

namespace GameDevLibrary.AI.GOAP
{
    [ExecuteInEditMode]
    public class GAgentVisual : MonoBehaviour
    {
        public GAgent thisAgent;

        // Start is called before the first frame update
        void Start()
        {
            thisAgent = GetComponent<GAgent>();
        }
    }
}
