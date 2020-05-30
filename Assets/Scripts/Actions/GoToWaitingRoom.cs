using GameDevLibrary.AI.GOAP;

namespace GOAP_Demo.Actions
{
    public class GoToWaitingRoom : GAction
    {

        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            GWorld.Instance.worldStates.ModifyState("Waiting", 1);
            GWorld.Instance.GetQueue("patients").AddResource(transform.parent.gameObject);
            beliefs.ModifyState("atHospital", 1);
            return true;
        }
    }
}
