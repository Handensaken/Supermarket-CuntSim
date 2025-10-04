using UnityEngine;

[CreateAssetMenu(menuName = "Game/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private string defeatScene;
    [SerializeField] private string victoryScene;
}
