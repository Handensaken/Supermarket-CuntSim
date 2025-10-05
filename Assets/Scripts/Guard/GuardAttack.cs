using System;

namespace Guard
{
    [Serializable]
    public class GuardAttack : GuardState
    {
        public override void Awake(GuardBehaviour guardBehaviour)
        {
            this.guardBehaviour = guardBehaviour;
        }

        public override void Enter()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }
    }
}
