using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/GamePort")]
public class GamePort : ScriptableObject
{
    public UnityAction<GameStage> OnGameEnd = delegate (GameStage gameStage){};
    //public UnityAction onGameCaughtEnd = delegate {};
    
    public enum GameStage
    {
        NextScene,
        Victory,
        Defeat
    };
}
