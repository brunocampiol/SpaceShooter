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


    // Start is called before the first frame update
    void Start()
    {
        _logger = new Logger();

        OptionsTitle.text = GameInfoStatic.OptionsTitle;

        ButtonBack.GetComponentInChildren<Text>().text = GameInfoStatic.ButtonBack;        

        ButtonBack.onClick.AddListener(BackButtonClick);
    }

    void BackButtonClick()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }
}
