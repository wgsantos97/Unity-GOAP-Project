using GameDevLibrary.AI.GOAP;

namespace GOAP_Demo.Actions
{
    public class Research : GAction
    {
        public override bool PrePerform()
        {
            target = GWorld.Instance.GetQueue("offices").GetResource();
            if (target == null) return false;
            inventory.AddItem(target);
            GWorld.Instance.worldStates.ModifyState("FreeOffice", -1);
            return true;
        }

        public override bool PostPerform()
        {
            GWorld.Instance.GetQueue("offices").AddResource(target);
            inventory.RemoveItem(target);
            GWorld.Instance.worldStates.ModifyState("FreeOffice", 1);
            return true;
        }
    }
}
