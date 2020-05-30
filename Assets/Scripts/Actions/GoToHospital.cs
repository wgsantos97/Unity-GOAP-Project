using GameDevLibrary.AI.GOAP;

namespace GOAP_Demo.Actions
{
    public class GoToHospital : GAction
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            return true;
        }
    }
}
