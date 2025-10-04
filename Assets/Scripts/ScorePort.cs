using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Score/ScorePort")]
public class ScorePort : ScriptableObject
{
    public UnityAction<uint> OnScore = delegate(uint scoreAmount) {  };
}
