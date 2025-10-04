using System;
using UnityEngine;

namespace Guard
{
    [CreateAssetMenu(menuName = "Game/Guard/GuardData")]
    public class GuardData : ScriptableObject
    {
        public Parameters patrol;
        public Parameters run;

        public Sight sight;
    }

    [Serializable]
    public struct Parameters
    {
        [Min(0)] public float speed;
        [Min(0)] public float angularSpeed;
        [Min(0)] public float acceleration;
        
    }
    [Serializable]
    public struct Sight
    {
        [Min(0)]public float totalSightAngle;
        [Min(0)]public float sightRange;
    }
    
}
