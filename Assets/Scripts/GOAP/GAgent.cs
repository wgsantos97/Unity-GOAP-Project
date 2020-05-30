using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameDevLibrary.AI.GOAP
{
    public class SubGoal
    {
        public Dictionary<string, int> sgoals;
        public bool remove;

        public SubGoal(string s, int i, bool r)
        {
            sgoals = new Dictionary<string, int>();
            sgoals.Add(s, i);
            remove = r;
        }
    }

    public class GAgent : MonoBehaviour
    {
        public List<GAction> actions = new List<GAction>();
        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
        public GInventory inventory = new GInventory();
        public WorldStates beliefs = new WorldStates();

        GPlanner planner;
        Queue<GAction> actionQueue;
        public GAction currentAction;
        SubGoal currentGoal;
        bool invoked = false;

        Vector3 destination = Vector3.zero;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            GAction[] acts = GetComponent<GActionInventory>().actions;
            foreach(GAction action in acts)
            {
                actions.Add(action);
            }
        }

        void LateUpdate()
        {
            if (IsCurrentActionRunning()) return;
            FormPlan();
            UpdateActionQueue();
        }

        private bool IsCurrentActionRunning()
        {
            if (currentAction != null && currentAction.running)
            {
                // NOTE: Mind the remaining distance threshold. Effects may change depending on agent size.
                // float distanceToTarget = Vector3.Distance(destination, transform.position);
                if (currentAction.agent.remainingDistance < 2f)
                {
                    if (!invoked)
                    {
                        Invoke("CompleteAction", currentAction.duration);
                        invoked = true;
                    }
                }
                return true;
            }
            return false;
        }

        private void FormPlan()
        {
            // If there is no plan or action queue
            if (planner == null || actionQueue == null)
            {
                planner = new GPlanner();

                // Sort the goals by importance
                var sortedGoals = from entry in goals orderby entry.Value descending select entry;

                // Form a plan from the first successful goal in the sorted dictionary of goals.
                foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
                {
                    actionQueue = planner.plan(actions, sg.Key.sgoals, beliefs);
                    if (actionQueue != null)
                    {
                        currentGoal = sg.Key;
                        break;
                    }
                }
            }
        }

        private void UpdateActionQueue()
        {
            // If no more actions, remove goal and planner
            if (actionQueue != null && actionQueue.Count == 0)
            {
                if(currentGoal.remove)
                {
                    goals.Remove(currentGoal);
                }
                planner = null;
            }

            // If there are more actions to take, send the next one
            if(actionQueue != null && actionQueue.Count > 0)
            {
                currentAction = actionQueue.Dequeue();
                if(currentAction.PrePerform())
                {
                    if(currentAction.target == null && currentAction.targetTag != "")
                    {
                        currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                    }
                    if(currentAction.target != null)
                    {
                        currentAction.running = true;

                        destination = currentAction.target.transform.position;
                        Transform dest = currentAction.target.transform.Find("Destination");
                        if (dest != null) destination = dest.position;
                        currentAction.agent.SetDestination(destination);
                    }
                }
                else
                {
                    actionQueue = null;
                }
            }
        }

        // Called by Invoke. Resets variables for the next action.
        private void CompleteAction()
        {
            currentAction.running = false;
            currentAction.PostPerform();
            invoked = false;
        }

    }
}
