using UnityEngine;

[CreateAssetMenu(menuName = "Score/ScoreObjectData")]
public class ScoreObjectData : ScriptableObject
{
    [Min(1)] public uint scoreValue;
}
