using UnityEngine;
using GameDevLibrary.AI.GOAP;

namespace GOAP_Demo.Agents
{
    public class Janitor : GAgent
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            SubGoal s1 = new SubGoal("clean", 1, false);
            goals.Add(s1, 2);

            SubGoal s2 = new SubGoal("rested", 1, false);
            goals.Add(s2, 1);

            Invoke("GetTired", Random.Range(10, 20));
        }

        void GetTired()
        {
            beliefs.ModifyState("exhausted", 0);
            Invoke("GetTired", Random.Range(10, 20));
        }
    }
}
