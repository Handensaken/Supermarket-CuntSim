using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [Space,SerializeField] private GamePort gamePort;
    
    
    private void OnEnable()
    {
        gamePort.onGameTimeEnd += LoadNextScene;
    }

    private void OnDisable()
    {
        gamePort.onGameTimeEnd -= LoadNextScene;
    }


    private void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
