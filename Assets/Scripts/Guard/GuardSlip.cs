using System;

namespace Guard
{
    [Serializable]
    public class GuardSlip : GuardState
    {
        public override void Awake(GuardBehaviour guardBehaviour)
        {
            this.guardBehaviour = guardBehaviour;
        }

        public override void Enter()
        {
            //Play Animation
        }

        public override void Update()
        {
            //Check if done animation
        }

        public override void Exit()
        {
            //CheckConditions for player
        }
    }
}
