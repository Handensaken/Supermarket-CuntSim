using System;
using UnityEngine;

namespace Guard
{
    [Serializable]
    public class GuardAttack : GuardState
    {
        [SerializeField] private GamePort gameport;
        public override void Awake(GuardBehaviour guardBehaviour)
        {
            this.guardBehaviour = guardBehaviour;
        }

        public override void Enter()
        {
            guardBehaviour.debugStates = States.Attack;
            gameport.OnGameEnd(GamePort.GameStage.Defeat);
        }

        public override void Update() {}
        public override void FixedUpdate(){}
        public override void Exit() {}
    }
}
