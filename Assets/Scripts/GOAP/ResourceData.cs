using UnityEngine;

namespace GameDevLibrary.AI.GOAP
{
    [CreateAssetMenu(fileName = "resourcedata", menuName = "Resource Data", order = 51)]
    public class ResourceData : ScriptableObject
    {
        [SerializeField] string resourceTag = "";
        [SerializeField] string resourceQueue = "";
        [SerializeField] string resourceState = "";

        public string ResourceTag => resourceTag;
        public string ResourceQueue => resourceQueue;
        public string ResourceState => resourceState;
    }
}
