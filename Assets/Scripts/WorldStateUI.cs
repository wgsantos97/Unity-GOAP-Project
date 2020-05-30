using System.Collections;
using System.Collections.Generic;
using GameDevLibrary.AI.GOAP;
using UnityEngine;
using UnityEngine.UI;

namespace GOAP_Demo
{
    public class WorldStateUI : MonoBehaviour
    {
        public Text states;

        void LateUpdate()
        {
            Dictionary<string, int> worldstates = GWorld.Instance.worldStates.States;
            states.text = "";

            foreach(KeyValuePair<string,int> s in worldstates)
            {
                states.text += s.Key + ", " + s.Value + "\n";
            }    
        }
    }
}
