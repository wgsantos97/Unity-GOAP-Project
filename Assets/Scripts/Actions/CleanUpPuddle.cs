using GameDevLibrary.AI.GOAP;

namespace GOAP_Demo.Actions
{
    public class CleanUpPuddle : GAction
    {
        public override bool PrePerform()
        {
            target = GWorld.Instance.GetQueue("puddles").GetResource();
            if (target == null) return false;
            inventory.AddItem(target);
            GWorld.Instance.worldStates.ModifyState("FreePuddle", -1);
            return true;
        }

        public override bool PostPerform()
        {
            inventory.RemoveItem(target);
            Destroy(target);
            return true;
        }
    }
}
