using System;

namespace Guard
{
    [Serializable]
    public class GuardIdle : GuardState
    {
        public override void Awake(GuardBehaviour guardBehaviour)
        {
            this.guardBehaviour = guardBehaviour;
        }
        public override void Enter() {} //Never leave
        public override void Update() {}
        public override void Exit() {}
    }
}
