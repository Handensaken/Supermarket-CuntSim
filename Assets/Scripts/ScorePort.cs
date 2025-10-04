using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Score/ScorePort")]
public class ScorePort : ScriptableObject
{
    public UnityAction<int> OnScore = delegate(int scoreAmount) {  };
}
