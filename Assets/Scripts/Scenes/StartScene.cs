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
    }

    void StartButtonClick()
    {
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
    }

    void OptionsButtonClick()
    {
        SceneManager.LoadScene("OptionsScene", LoadSceneMode.Single);
    }
    
}
