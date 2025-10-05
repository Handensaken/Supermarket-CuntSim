using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameStage = GamePort.GameStage;

public class GameManager : MonoBehaviour
{
    private static string victoryScene = "W";
    private static string loseScene = "L";


    [Space]
    [SerializeField] private GamePort gamePort;
    //[SerializeField] private GameData gameData;

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
                LoadScene(victoryScene);
                break;
            case GameStage.Defeat:
                LoadScene(loseScene);
                break;
            default:
                Debug.LogWarning("missing implementation in sceneHandler");
                break;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string s)
    {
        SceneManager.LoadScene(s);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
} 
