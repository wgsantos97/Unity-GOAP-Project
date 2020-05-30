using GameDevLibrary.AI.GOAP;

namespace GOAP_Demo.Actions
{
    public class GoToToilet : GAction
    {
        public override bool PrePerform()
        {
            target = GWorld.Instance.GetQueue("toilets").GetResource();
            if (target == null) return false;
            inventory.AddItem(target);
            GWorld.Instance.worldStates.ModifyState("FreeToilet", -1);
            return true;
        }

        public override bool PostPerform()
        {
            GWorld.Instance.GetQueue("toilets").AddResource(target);
            inventory.RemoveItem(target);
            GWorld.Instance.worldStates.ModifyState("FreeToilet", 1);
            beliefs.RemoveState("busting");
            return true;
        }
    }
}
