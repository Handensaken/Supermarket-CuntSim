using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Guard
{
    public class GuardBehaviour : MonoBehaviour
    {
        [SerializeField] private GuardData guardData;
        [SerializeField] private Transform eyes;

        [SerializeField] private Transform childVisual;
        public GuardData GuardData => guardData;
        public Transform Eyes => eyes;
        
        #region States
        
        public GuardPatrol guardPatrol = new GuardPatrol();
        public GuardRun guardRun = new GuardRun();
        public GuardSlip guardSlip = new GuardSlip();
        public GuardAttack guardAttack = new GuardAttack();
        public GuardSearch guardSearch = new GuardSearch();
        
        private GuardState _currentState;
        [SerializeField]public States debugStates = States.Null;

        #endregion
        
        private Transform _target;
        public Transform Target => _target;

        #region NavMesh

        [SerializeField] private NavMeshAgent agent;
        [Space,SerializeField] private Vector3[] patrolPoints;
        
        private Vector3 startPos;

        public NavMeshAgent Agent => agent;
        public Vector3[] PatrolPoints => patrolPoints;
        public Vector3 StartPos => startPos;

        #endregion

        [SerializeField] private Animator animator;

        public Animator GetAnimator => animator;

        private void OnEnable()
        {
            startPos = transform.position;
        }

        private void Awake()
        {
            startPos = transform.position;
            
            guardPatrol.Awake(this);
            guardRun.Awake(this);
            guardAttack.Awake(this);
            guardSlip.Awake(this);
            guardSearch.Awake(this);
        }

        private void Start()
        {
            _target = FindAnyObjectByType<PlayerMovement>()?.transform;
            Initialize();
        }

        private void Initialize()
        {
            _currentState = guardPatrol;
            _currentState.Enter();
        }
        
        public void Transition(GuardState nextState)
        {
            _currentState.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }

        /*private void Update()
        {
            _currentState.Update();
        }*/
        
        private void FixedUpdate()
        {
            _currentState.FixedUpdate();
            FixRotationChild();
        }

        private void FixRotationChild()
        {
            childVisual.transform.right = -agent.transform.forward;
        }
        
        #region Gizmos

        private void OnDrawGizmos()
        {
            VisualiseSight(eyes.position, guardData.sight);
            guardPatrol.OnDrawGizmos(this);
        }
        
        private void VisualiseSight(Vector3 sightPoint, Sight sightData) //Shame
        {
            //Only need x, z
            Vector3 worldPos = sightPoint;
            Vector3 forward = transform.forward;
            Gizmos.color = Color.red;

            //LeftSide
            Vector2 valuesForLeftSide = RotateVectorCounter(new Vector2(forward.x,forward.z), sightData.totalSightAngle);
            Vector3 leftSide = new Vector3(valuesForLeftSide.x, 0, valuesForLeftSide.y)*sightData.sightRange;

            Vector3 currentCubePos = worldPos + leftSide;
            Gizmos.DrawLine(worldPos, currentCubePos);
            Gizmos.DrawCube(currentCubePos, new Vector3(.1f,.1f,.1f));
            Vector3 pastCubePos = currentCubePos;
        
            //Points on frontline
            //LeftPoint
            Vector2 values4LeftPoint = RotateVectorCounter(new Vector2(forward.x,forward.z), sightData.totalSightAngle/2);
            Vector3 leftSidePoint = new Vector3(values4LeftPoint.x, 0, values4LeftPoint.y)*sightData.sightRange;
            currentCubePos = worldPos + leftSidePoint;
        
            Gizmos.DrawCube(currentCubePos, new Vector3(.1f,.1f,.1f));
            Gizmos.DrawLine(pastCubePos, currentCubePos);
            pastCubePos = currentCubePos;
        
            //CenterPoint
            currentCubePos = worldPos + forward * sightData.sightRange;
            Gizmos.DrawCube(currentCubePos, new Vector3(.1f,.1f,.1f));
            Gizmos.DrawLine(pastCubePos, currentCubePos);
            pastCubePos = currentCubePos;
        
            //RightPoint
            Vector2 values4RightPoint = RotateVectorClock(new Vector2(forward.x,forward.z), sightData.totalSightAngle/2);
            Vector3 rightSidePoint = new Vector3(values4RightPoint.x, 0, values4RightPoint.y)*sightData.sightRange;
        
            currentCubePos = worldPos + rightSidePoint;
            Gizmos.DrawCube(currentCubePos, new Vector3(.1f,.1f,.1f));
            Gizmos.DrawLine(pastCubePos, currentCubePos);
            pastCubePos = currentCubePos;
        
            //RightSide
            Vector2 valuesForRightSide = RotateVectorClock(new Vector2(forward.x,forward.z), sightData.totalSightAngle);
            Vector3 rightSide = new Vector3(valuesForRightSide.x, 0, valuesForRightSide.y)*sightData.sightRange;

            currentCubePos = worldPos + rightSide;
            Gizmos.DrawLine(pastCubePos, currentCubePos);
            Gizmos.DrawCube(currentCubePos, new Vector3(.1f,.1f,.1f));
            pastCubePos = currentCubePos;
        
            //LastLineRight
            currentCubePos = worldPos;
            Gizmos.DrawLine(pastCubePos, currentCubePos);
        }
    
        private Vector2 RotateVectorCounter(Vector2 inputVector, float angle)
        {
            if (angle <= 0) throw new ArgumentException("RotateVectorCounter can't and shouldn't handle angle less or equal to 0");
        
            float vectorX = inputVector.x * Mathf.Cos(Mathf.Deg2Rad * angle) +
                            inputVector.y * -Mathf.Sin(Mathf.Deg2Rad * angle);
            float vectorY = inputVector.x * Mathf.Sin(Mathf.Deg2Rad * angle) +
                            inputVector.y * Mathf.Cos(Mathf.Deg2Rad * angle);

            return new Vector2(vectorX, vectorY);
        }
        private Vector2 RotateVectorClock(Vector2 inputVector, float angle)
        {
            if (angle <= 0) throw new ArgumentException("RotateVectorCounter can't and shouldn't handle angle less or equal to 0");
        
            float vectorX = inputVector.x * Mathf.Cos(Mathf.Deg2Rad * angle) +
                            inputVector.y * Mathf.Sin(Mathf.Deg2Rad * angle);
            float vectorY = inputVector.x * -Mathf.Sin(Mathf.Deg2Rad * angle) +
                            inputVector.y * Mathf.Cos(Mathf.Deg2Rad * angle);

            return new Vector2(vectorX, vectorY);
        }

        #endregion
    }

    public enum States
    {
        Null,
        Patrol,
        Run,
        Attack,
        Slip,
        Search
    }
}
