using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameDevLibrary.AI.GOAP
{
    public sealed class GWorld
    {
        // Private Variables
        private static readonly GWorld instance = new GWorld();
        private static WorldStates _worldStates;
        private static ResourceQueue patients;
        private static ResourceQueue cubicles;
        private static ResourceQueue offices;
        private static ResourceQueue toilets;
        private static ResourceQueue puddles;
        private static Dictionary<string, ResourceQueue> resources => ResourceQueue.Resources;

        // Public Accessors
        public static GWorld Instance => instance;
        public WorldStates worldStates => _worldStates;

        static GWorld()
        {
            _worldStates = new WorldStates();
            patients = new ResourceQueue("", "", "patients", _worldStates);
            cubicles = new ResourceQueue("Cubicle", "FreeCubicle", "cubicles", _worldStates);
            offices = new ResourceQueue("Office", "FreeOffice", "offices", _worldStates);
            toilets = new ResourceQueue("Toilet", "FreeToilet", "toilets", _worldStates);
            puddles = new ResourceQueue("Puddle", "FreePuddle", "puddles", _worldStates);
        }

        private GWorld() {}

        public ResourceQueue GetQueue(string type)
        {
            if (!resources.ContainsKey(type))
            {
                Debug.LogError("Could not find key: " + type);
                return null;
            }
            return resources[type];
        }
    }

    public class ResourceQueue
    {
        public Queue<GameObject> queue = new Queue<GameObject>();
        public string tag;
        public string stateModifier;
        private static Dictionary<string, ResourceQueue> resources;
        public static Dictionary<string, ResourceQueue> Resources => resources;

        public ResourceQueue(string t, string s, string k, WorldStates w)
        {
            if (resources == null) resources = new Dictionary<string, ResourceQueue>();

            tag = t;
            stateModifier = s;
            if (tag != "")
            {
                GameObject[] resources = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject r in resources)
                    queue.Enqueue(r);
            }

            if (stateModifier != "")
            {
                w.ModifyState(stateModifier, queue.Count);
            }

            resources.Add(k, this);
        }

        public void AddResource(GameObject r)
        {
            queue.Enqueue(r);
        }

        public GameObject GetResource()
        {
            if (queue.Count == 0) return null;
            return queue.Dequeue();
        }

        public void RemoveResource(GameObject r)
        {
            queue = new Queue<GameObject>(queue.Where(p => p != r));
        }
    }
}
