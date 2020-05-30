using System.Collections.Generic;
using UnityEngine;

namespace GameDevLibrary.AI.GOAP
{
    public class GInventory
    {
        List<GameObject> items = new List<GameObject>();
        public List<GameObject> Items => items;

        public void AddItem(GameObject i)
        {
            items.Add(i);
        }

        public GameObject FindItemWithTag(string tag)
        {
            foreach(GameObject i in items)
            {
                if (i == null) break;
                if(i.tag == tag)
                {
                    return i;
                }
            }
            return null;
        }

        public void RemoveItem(GameObject i)
        {
            items.Remove(i);
        }
    }
}
