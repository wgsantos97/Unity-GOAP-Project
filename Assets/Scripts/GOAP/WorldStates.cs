using System.Collections.Generic;
using System;

namespace GameDevLibrary.AI.GOAP
{
    [Serializable]
    public class WorldState
    {
        public string key;
        public int value;
    }

    public class WorldStates
    {
        private Dictionary<string, int> states;
        public Dictionary<string, int> States => states;

        public WorldStates()
        {
            states = new Dictionary<string, int>();
        }

        public bool hasState(string key)
        {
            return states.ContainsKey(key);
        }

        public void ModifyState(string key, int value)
        {
            if(states.ContainsKey(key))
            {
                states[key] += value;
                if(states[key] <= 0)
                {
                    RemoveState(key);
                }
            }
            else
            {
                AddState(key, value);
            }
        }

        public void AddState(string key, int value)
        {
            states.Add(key, value);
        }

        public void RemoveState(string key)
        {
            if(states.ContainsKey(key))
            {
                states.Remove(key);
            }
        }

        public void SetState(string key, int value)
        {
            if(states.ContainsKey(key))
            {
                states[key] = value;
            }
            else
            {
                AddState(key, value);
            }
        }
    }
}
