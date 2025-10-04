using UnityEngine;
using UnityEngine.SceneManagement;
using GameStage = GamePort.GameStage;

public class GameManager : MonoBehaviour
{
    [Space]
    [SerializeField] private GamePort gamePort;
    [SerializeField] private GameData gameData;
    
    private void OnEnable()
    {
        gamePort.OnGameEnd += HandleSceneChange;
    }

    private void OnDisable()
    {
        gamePort.OnGameEnd -= HandleSceneChange;
    }

    private void HandleSceneChange(GameStage gameStage)
    {
        switch (gameStage)
        {
            case GameStage.NextScene:
                LoadNextScene();
                break;
            case GameStage.Victory:
                LoadNextScene();
                break;
            case GameStage.Defeat:
                LoadNextScene();
                break;
            default:
                Debug.LogWarning("missing implementation in sceneHandler");
                break;
        }
    }
    
    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}
