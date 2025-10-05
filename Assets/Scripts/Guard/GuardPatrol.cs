using System;
using UnityEngine;
using UnityEngine.AI;

namespace Guard
{
    [Serializable]
    public class GuardPatrol : GuardState
    {
        [SerializeField, Min(0)] private float speed;
        [SerializeField, Min(0)] private int patrolPointIndex;
        public override void Awake(GuardBehaviour guardBehaviour)
        {
            this.guardBehaviour = guardBehaviour;
        }

        public override void Enter()
        {
            guardBehaviour.debugStates = States.Patrol;
            guardBehaviour.GetAnimator.SetBool("Walking", true);
            
            UpdateTargetPoint(patrolPointIndex);
            Parameters patrolValues = guardBehaviour.GuardData.patrol;
            SetUpStateValuesInAgent(patrolValues.speed,patrolValues.angularSpeed, patrolValues.acceleration);
        }

        public override void Update() {}

        public override void FixedUpdate()
        {
            //eyes
            GuardState newState = Check4Player(guardBehaviour.Eyes, guardBehaviour.GuardData.sight);
            if (newState != this)
            {
                guardBehaviour.Transition(newState);
                return;
            }
            
            CheckSwapPatrolPoint();
        }

        public override void Exit()
        {
            guardBehaviour.GetAnimator.SetBool("Walking", false);
        }
    
        private void SetTargetPoint(int currentPatrolIndex)
        {
            int nextPointIndex = currentPatrolIndex % guardBehaviour.PatrolPoints.Length;
            guardBehaviour.Agent.SetDestination(guardBehaviour.PatrolPoints[nextPointIndex]+guardBehaviour.StartPos);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eyes"></param>
        /// <param name="range"></param>
        /// <param name="angle"></param>
        /// <returns>true if swap state</returns>
        private GuardState Check4Player(Transform eyes, Sight sight)
        {
            if (guardBehaviour.Target == null) return guardBehaviour.guardPatrol;
            
            if (!CheckTargetInRange(eyes,sight.sightRange)) return guardBehaviour.guardPatrol;
            if (!CheckTargetWithinAngleOfSight(eyes,sight.totalSightAngle/2f)) return guardBehaviour.guardPatrol;

            if (CheckIfRaycastHit(eyes,sight.sightRange))
            {
                return guardBehaviour.guardRun;
            }

            return guardBehaviour.guardPatrol;
        }
    
        private void CheckSwapPatrolPoint()
        {
            if (guardBehaviour.PatrolPoints.Length <= 0)
            {
                Debug.Log("Missing patrolPoints :"+guardBehaviour.name);
                return;
            }
            if (guardBehaviour.Agent.remainingDistance <= 0.01f)
            {
                patrolPointIndex = (patrolPointIndex+1)%guardBehaviour.PatrolPoints.Length;
                UpdateTargetPoint(patrolPointIndex);
            }
        }

        public void OnDrawGizmos(GuardBehaviour guardBehaviour)
        {
            VisualizePoints(guardBehaviour);
            
        }
        
        private void VisualizePoints(GuardBehaviour guardBehaviour)
        {
            if (guardBehaviour.PatrolPoints.Length < 1) return;
            if (guardBehaviour.PatrolPoints.Length == 1)
            {
                if (!Application.isPlaying)
                {
                    Gizmos.DrawCube( guardBehaviour.transform.position+guardBehaviour.PatrolPoints[0], new Vector3(.5f,.5f,.5f));
                }
                else
                {
                    Gizmos.DrawCube( guardBehaviour.StartPos+guardBehaviour.PatrolPoints[0], new Vector3(.5f,.5f,.5f));
                }
                return;
            }
        
            for (int i = 0; i < guardBehaviour.PatrolPoints.Length; i++)
            {
                if (!Application.isPlaying)
                {
                    Gizmos.DrawCube(guardBehaviour.transform.position+guardBehaviour.PatrolPoints[i], new Vector3(.5f,.5f,.5f));
                    Gizmos.DrawLine(guardBehaviour.transform.position+guardBehaviour.PatrolPoints[i], guardBehaviour.transform.position+guardBehaviour.PatrolPoints[(i+1)%guardBehaviour.PatrolPoints.Length]);
                }
                else
                {
                    Gizmos.DrawCube(guardBehaviour.StartPos+guardBehaviour.PatrolPoints[i], new Vector3(.5f,.5f,.5f));
                    Gizmos.DrawLine(guardBehaviour.StartPos+guardBehaviour.PatrolPoints[i], guardBehaviour.StartPos+guardBehaviour.PatrolPoints[(i+1)%guardBehaviour.PatrolPoints.Length]);
                }
                
            }
        }

        public void UpdateTargetPoint(int newPatrolIndex)
        {
            SetTargetPoint(newPatrolIndex);
        }
    }
}
