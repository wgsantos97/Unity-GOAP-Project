using GameDevLibrary.AI.GOAP;

namespace GOAP_Demo.Actions
{
    public class Rest : GAction
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            beliefs.RemoveState("exhausted");
            return true;
        }
    }
}
