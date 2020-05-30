using UnityEngine;

namespace GameDevLibrary.AI.GOAP
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] ResourceData data = null;
        public ResourceData Data => data;
    }
}
