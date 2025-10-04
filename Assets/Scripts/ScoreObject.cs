using UnityEngine;

public class ScoreObject : MonoBehaviour
{
    [SerializeField] private ScoreObjectData scoreObjectData;

    public uint ScoreValue => scoreObjectData.scoreValue;
}
