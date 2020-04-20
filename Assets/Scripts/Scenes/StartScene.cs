using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
     public Text GameTitle;

     public Button ButtonStart;
     public Button ButtonOptions;

    private ILogger _logger;

    // Start is called before the first frame update
    void Start()
    {
        _logger = new Logger();

        GameTitle.text = GameInfoStatic.GameName;

        ButtonStart.GetComponentInChildren<Text>().text = GameInfoStatic.ButtonStart;
        ButtonOptions.GetComponentInChildren<Text>().text = GameInfoStatic.ButtonOptions;

        ButtonStart.onClick.AddListener(StartButtonClick);
        ButtonOptions.onClick.AddListener(OptionsButtonClick);

        for(int i =0; i < SceneManager.sceneCount; i++)
        {
            _logger.LogInfo($"Scene '{SceneManager.GetSceneAt(i).name}'");
        }

        GameInfo.Instance.GameState = GameState.Menu;

        //SceneManager.UnloadSceneAsync("PlayScene");
        //SceneManager.UnloadSceneAsync("OptionsScene");
    }

    void StartButtonClick()
    {

         StartCoroutine(LoadPlayScene());
    }

    void OptionsButtonClick()
    {
         StartCoroutine(LoadOptionsScene());
    }
    
    private IEnumerator LoadOptionsScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("OptionsScene", LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

        private IEnumerator LoadPlayScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("PlayScene", LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
