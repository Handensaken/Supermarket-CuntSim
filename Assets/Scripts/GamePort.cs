using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/GamePort")]
public class GamePort : ScriptableObject
{
    public UnityAction onGameTimeEnd = delegate {};
    //public UnityAction onGameCaughtEnd = delegate {};
}
