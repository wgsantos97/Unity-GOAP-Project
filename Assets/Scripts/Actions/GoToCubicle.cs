using GameDevLibrary.AI.GOAP;
using UnityEngine;

namespace GOAP_Demo.Actions
{
    public class GoToCubicle : GAction
    {
        GameObject resource;

        public override bool PrePerform()
        {
            target = inventory.FindItemWithTag("Cubicle");
            if (target == null) return false;
            return true;
        }

        public override bool PostPerform()
        {
            GWorld.Instance.worldStates.ModifyState("TreatingPatient",1);
            GWorld.Instance.GetQueue("cubicles").AddResource(target);
            inventory.RemoveItem(target);
            GWorld.Instance.worldStates.ModifyState("FreeCubicle", 1);
            return true;
        }
    }
}
