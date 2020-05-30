using GameDevLibrary.AI.GOAP;

namespace GOAP_Demo.Actions
{
    public class GoToReception : GAction
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            beliefs.ModifyState("atHospital", 0);
            return true;
        }
    }
}
