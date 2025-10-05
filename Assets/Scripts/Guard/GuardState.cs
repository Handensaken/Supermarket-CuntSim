using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Guard
{
    public abstract class GuardState
    {
        protected GuardBehaviour guardBehaviour;

        public abstract void Awake(GuardBehaviour guardBehaviour);
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
        
        protected void SetUpStateValuesInAgent(float speed, float angularSpeed, float acceleration)
        {
            SetAgentSpeed(speed);
            SetAgentAngularSpeed(angularSpeed);
            SetAgentAcceleration(acceleration);
        }
    
        protected void SetAgentSpeed(float speed)
        {
            guardBehaviour.Agent.speed = speed;
        }
        protected void SetAgentAngularSpeed(float speed)
        {
            guardBehaviour.Agent.angularSpeed = speed;
        }
        protected void SetAgentAcceleration(float speed)
        {
            guardBehaviour.Agent.acceleration = speed;
        }
    
        protected bool CheckTargetInRange(Transform eyes,float range)
        {
            float distance = Vector3.Distance(guardBehaviour.Target.position, eyes.position);
            return distance <= range;
        }

        /// <summary>
    /// Main axis comes from eyes forward. Matf.abs for only the differance in angles. will calculate according to a rotation axis Vector.up, so the other values only use x and z 
    /// </summary>
    /// <returns></returns>
    protected bool CheckTargetWithinAngleOfSight(Transform eyes, float angleOneSide)
    {
        Vector3 eyesPosition = eyes.position;
        Vector3 targetPos = guardBehaviour.Target.position;
        Vector3 directionToPlayer = (new Vector3(targetPos.x,0,targetPos.z) - new Vector3(eyesPosition.x,0,eyesPosition.z)).normalized;
        float dot = Mathf.Clamp(Vector3.Dot(guardBehaviour.transform.forward, directionToPlayer),-1,1);
        float angle = Mathf.Rad2Deg * Mathf.Acos(dot);
            
        return angle <= angleOneSide;
    }
    
    /// <summary>
    /// If true the it has hit the player
    /// </summary>
    /// <param name="eyes"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    protected bool CheckIfRaycastHit(Transform eyes,float range)
    {
        LayerMask layerMask = ~LayerMask.GetMask("InteractiveEnvironment", "Ignore Raycast");
        Transform target = guardBehaviour.Target;
        
        Vector3 directionToPlayer = (target.position+new Vector3(0,0.2f,0)  - eyes.position).normalized;
        Physics.Raycast(eyes.position,directionToPlayer ,out RaycastHit hit,range,layerMask);
            
        foreach (var playerCollider in guardBehaviour.Target.GetComponents<Collider>())
        {
            if (hit.collider == playerCollider)
            {
                return true;
            }
        }
        return false;
    }
    }
}
