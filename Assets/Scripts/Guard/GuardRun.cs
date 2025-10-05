using System;
using UnityEngine;
using UnityEngine.AI;

namespace Guard
{
    [Serializable]
    public class GuardRun : GuardState
    {
        private float currentTime = 0f;

        public override void Awake(GuardBehaviour guardBehaviour)
        {
            this.guardBehaviour = guardBehaviour;
        }
        
        public override void Enter()
        {
            guardBehaviour.debugStates = States.Run;
            guardBehaviour.GetAnimator.SetBool("Running", true);
            guardBehaviour.Agent.SetDestination(guardBehaviour.Target.position);

            Parameters run = guardBehaviour.GuardData.run;
            SetUpStateValuesInAgent(run.speed,run.angularSpeed, run.acceleration);
        }

        public override void Exit()
        {
            guardBehaviour.GetAnimator.SetBool("Running", false);
        }

        public override void FixedUpdate()
        {
            //TrollEyes
            GuardState newState = Check4Player(guardBehaviour.Eyes, guardBehaviour.GuardData.sight.sightRange);
            if (newState != this)
            {
                guardBehaviour.Agent.SetDestination(guardBehaviour.Target.position);
                guardBehaviour.Transition(newState);
                return;
            }

            guardBehaviour.Agent.SetDestination(guardBehaviour.Target.position);
        }
        public override void Update() {}

        private GuardState Check4Player(Transform eyes, float range)
        {
            if (guardBehaviour.Target == null) return guardBehaviour.guardPatrol;

            bool inRangeOfAggression = CheckTargetInRange(eyes, range);
            if (!inRangeOfAggression) return guardBehaviour.guardPatrol;

            if (!CheckIfRaycastHit(eyes, range))
            {
                return guardBehaviour.guardSearch;
            }

            if (CheckTargetInRange(guardBehaviour.transform, guardBehaviour.GuardData.attackRange))// insight and close enough for attack
            {
                Debug.Log("Attack");
                return guardBehaviour.guardAttack;
            }

            return guardBehaviour.guardRun;
        }
    }
}
