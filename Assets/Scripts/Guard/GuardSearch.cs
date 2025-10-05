using System;
using UnityEngine;
using UnityEngine.AI;
using Color = System.Drawing.Color;

namespace Guard
{
    [Serializable]
    public class GuardSearch : GuardState
    {
        public override void Awake(GuardBehaviour guardBehaviour)
        {
            this.guardBehaviour = guardBehaviour;
        }

        public override void Enter()
        {
            guardBehaviour.debugStates = States.Search;
            Parameters searchParameters = guardBehaviour.GuardData.patrol;
            SetUpStateValuesInAgent(searchParameters.speed, searchParameters.angularSpeed, searchParameters.acceleration);
        }

        public override void Update() {}

        public override void FixedUpdate()
        {
            if (guardBehaviour.Agent.remainingDistance <= 0.01f)
            {
                guardBehaviour.Transition(guardBehaviour.guardPatrol);
                return;
            }
        
            //Eyes
            GuardState newState = Check4Player(guardBehaviour.Eyes, guardBehaviour.GuardData.sight.sightRange,
                guardBehaviour.GuardData.sight.totalSightAngle);
            if (newState != this)
            {
                guardBehaviour.Transition(newState);
                return;
            }
        }

        public override void Exit() {}
    
        private GuardState Check4Player(Transform eyes, float range, float angle)
        {
            if (guardBehaviour.Target == null) return guardBehaviour.guardPatrol;
            if (!CheckTargetWithinAngleOfSight(eyes, angle/2f)) return guardBehaviour.guardSearch;
            if (!CheckIfRaycastHit(eyes, range)) return guardBehaviour.guardSearch;

            return guardBehaviour.guardRun;
        }
    }
}
