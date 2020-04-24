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

    public StartScene()
    {
        _logger = new Logger();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameTitle.text = GameInfoStatic.GameName;

        ButtonStart.GetComponentInChildren<Text>().text = GameInfoStatic.ButtonStart;
        ButtonOptions.GetComponentInChildren<Text>().text = GameInfoStatic.ButtonOptions;

        ButtonStart.onClick.AddListener(StartButtonClick);
        ButtonOptions.onClick.AddListener(OptionsButtonClick);

        GameInfo.Instance.GameState = GameState.Menu;
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
