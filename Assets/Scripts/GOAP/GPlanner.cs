using System.Collections.Generic;
using UnityEngine;

namespace GameDevLibrary.AI.GOAP
{
    public class Node
    {
        public Node parent;
        public float cost;
        public Dictionary<string, int> states;
        public GAction action;

        public Node(Node parent, float cost, Dictionary<string, int> states, GAction action)
        {
            this.parent = parent;
            this.cost = cost;
            this.states = new Dictionary<string, int>(states);
            this.action = action;
        }

        public Node(Node parent, float cost, Dictionary<string, int> states, Dictionary<string, int> beliefStates, GAction action) : this(parent, cost, states, action)
        {
            foreach(KeyValuePair<string,int> b in beliefStates)
            {
                if (!this.states.ContainsKey(b.Key))
                    this.states.Add(b.Key, b.Value);
            }
        }
    }

    public class GPlanner
    {
        public Queue<GAction> plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates beliefStates)
        {
            List<GAction> usableActions = new List<GAction>();
            foreach (GAction action in actions)
            {
                if (action.IsAchievable())
                {
                    usableActions.Add(action);
                }
            }

            List<Node> leaves = new List<Node>();
            Node start = new Node(null, 0, GWorld.Instance.worldStates.States, beliefStates.States, null);

            bool success = BuildGraph(start, leaves, usableActions, goal);

            if (!success)
            {
                //Debug.Log("NO PLAN");
                return null;
            }

            Node cheapest = null;
            foreach (Node leaf in leaves)
            {
                if (cheapest == null)
                {
                    cheapest = leaf;
                }
                else
                {
                    if (leaf.cost < cheapest.cost)
                    {
                        cheapest = leaf;
                    }
                }
            }

            List<GAction> result = new List<GAction>();
            Node n = cheapest;
            while (n != null)
            {
                if (n.action != null)
                {
                    result.Insert(0, n.action);
                }
                n = n.parent;
            }

            Queue<GAction> queue = new Queue<GAction>();
            foreach (GAction action in result)
            {
                queue.Enqueue(action);
            }

            //ShowPlan(queue);
            return queue;
        }

        void ShowPlan(Queue<GAction> queue)
        {
            Debug.Log("The Plan is: ");
            foreach (GAction action in queue)
            {
                Debug.Log("Q: " + action.ActionName);
            }
        }

        private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
        {
            bool foundPath = false;
            foreach(GAction action in usableActions)
            {
                if (action.IsAchievableGiven(parent.states))
                {
                    Dictionary<string, int> currentState = new Dictionary<string, int>(parent.states);
                    foreach(KeyValuePair<string, int> e in action.effects)
                    {
                        if(!currentState.ContainsKey(e.Key))
                        {
                            currentState.Add(e.Key, e.Value);
                        }
                    }

                    // Accumulates cost to the last node. So we can look at the last node and know the cost immediately
                    Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                    if(GoalAchieved(goal, currentState))
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        List<GAction> subset = ActionSubset(usableActions, action);
                        bool found = BuildGraph(node, leaves, subset, goal);
                        if(found)
                        {
                            foundPath = true;
                        }
                    }
                }
            }

            return foundPath;
        }

        private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
        {
            foreach(KeyValuePair<string, int> g in goal)
            {
                if(!state.ContainsKey(g.Key))
                {
                    return false;
                }
            }
            return true;
        }

        private List<GAction> ActionSubset(List<GAction> actions, GAction targetRemoval)
        {
            List<GAction> subset = new List<GAction>();
            foreach(GAction a in actions)
            {
                if(!a.Equals(targetRemoval))
                {
                    subset.Add(a);
                }
            }
            return subset;
        }
    }
}
