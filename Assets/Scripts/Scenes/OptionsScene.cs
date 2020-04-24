using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScene : MonoBehaviour
{
    public Text OptionsTitle;
    public Button ButtonBack;

    private ILogger _logger;


    public OptionsScene()
    {
        _logger = new Logger();

    }

    // Start is called before the first frame update
    void Start()
    {
        OptionsTitle.text = GameInfoStatic.OptionsTitle;

        ButtonBack.GetComponentInChildren<Text>().text = GameInfoStatic.ButtonBack;

        ButtonBack.onClick.AddListener(BackButtonClick);

        GameInfo.Instance.GameState = GameState.Options;
    }

    void BackButtonClick()
    {
        StartCoroutine(LoadStartScene());
    }

    private IEnumerator LoadStartScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
