using GameDevLibrary.AI.GOAP;

namespace GOAP_Demo.Actions
{
    public class GetTreated : GAction
    {
        public override bool PrePerform()
        {
            target = inventory.FindItemWithTag("Cubicle");
            if (target == null) return false;
            return true;
        }

        public override bool PostPerform()
        {
            GWorld.Instance.worldStates.ModifyState("Treated",1);
            beliefs.ModifyState("isCured", 1);
            inventory.RemoveItem(target);
            return true;
        }
    }
}
