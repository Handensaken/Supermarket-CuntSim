using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Game/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] public SceneAsset defeatScene;
    [SerializeField] public SceneAsset victoryScene;
}
