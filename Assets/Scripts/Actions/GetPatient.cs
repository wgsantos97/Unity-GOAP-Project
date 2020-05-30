using GameDevLibrary.AI.GOAP;
using UnityEngine;

namespace GOAP_Demo.Actions
{
    public class GetPatient : GAction
    {
        GameObject resource;

        public override bool PrePerform()
        {
            target = GWorld.Instance.GetQueue("patients").GetResource();
            if (target == null) return false;

            resource = GWorld.Instance.GetQueue("cubicles").GetResource();
            if(resource!=null)
                inventory.AddItem(resource);
            else
            {
                GWorld.Instance.GetQueue("patients").AddResource(target);
                target = null;
                return false;
            }

            GWorld.Instance.worldStates.ModifyState("FreeCubicle",-1);
            return true;
        }

        public override bool PostPerform()
        {
            GWorld.Instance.worldStates.ModifyState("Waiting", -1);
            if (target)
                target.GetComponent<GAgent>().inventory.AddItem(resource);
            return true;
        }
    }
}
