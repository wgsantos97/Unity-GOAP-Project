using GameDevLibrary.AI.GOAP;

namespace GOAP_Demo.Actions
{
    public class GoHome : GAction
    {
        public override bool PrePerform()
        {
            beliefs.RemoveState("atHospital");
            return true;
        }

        public override bool PostPerform()
        {
            Destroy(transform.parent.gameObject, 1f);
            return true;
        }
    }
}
