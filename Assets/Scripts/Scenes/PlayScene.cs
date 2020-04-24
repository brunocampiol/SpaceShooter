using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour
{
    public Text LevelTime;
    public Text PlayerScore;
    public Text PlayerLives;
    public Text CenterText;

    private ILogger _logger;
    private GameObject _player;
    private GameObject _enemy;

    // Holds the last game state, when it was changed
    // and if it was already handled once
    private GameState _lastGameState;
    private double _lastStateTicket;
    private bool _lastStateHandled;

    private static string _updateTimeText_Tick = "UpdateTimeText_Tick";
    private static string _updateGameTicke_Tick = "UpdateGameTicket_Tick";

    public PlayScene()
    {
        _logger = new Logger();

        // default on game start
        GameInfo.Instance.GameTicks = 0;
        GameInfo.Instance.PlayerLives = GameInfoStatic.DefaultPlayerLives;
        GameInfo.Instance.PlayerScore = GameInfoStatic.DefaultPlayerScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag(GameInfoStatic.TagEnemy);
        _player = GameObject.FindGameObjectWithTag(GameInfoStatic.TagPlayer);

        InitGameTimers();
        InitializeScene();
    }

    // Update is called once per frame
    void Update()
    {
        HandleGameState();

        if (GameInfo.Instance.GameState == GameState.Paused) return;

        switch (GameInfo.Instance.GameState)
        {
            case GameState.PlayerLoose:
                HandlePlayerLoose();
                break;
            case GameState.PlayerWin:
                HandlePlayerWin();
                break;
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Menu:
                Destroy(this);
                break;
            default:
                break;
        }

        // Update GUI
        UpdateGUI();
    }

    private void FixedUpdate()
    {

    }

    private void UpdateGUI()
    {
        LevelTime.text = GameInfo.Instance.LevelTime.ToString();
        PlayerScore.text = $"Score: {GameInfo.Instance.PlayerScore.ToString()}";
        PlayerLives.text = $"Lives: {GameInfo.Instance.PlayerLives.ToString()}";
    }

    private void InitializeScene()
    {
        // reset level time
        GameInfo.Instance.LevelTime = GameInfoStatic.DefaultLevelTime;

        // Sets player in the middle
        _player.transform.position = Vector3.zero;
        
        // Sets enemy in the middle
        _enemy.transform.position = new Vector3(0, 0, 50);

        _player.SetActive(true);
        _enemy.SetActive(true);

        GameInfo.Instance.GameState = GameState.Starting;
    }

    private void HandleGameState()
    {
        if (_lastGameState != GameInfo.Instance.GameState)
        {
            _lastStateTicket = GameInfo.Instance.GameTicks;
            _lastGameState = GameInfo.Instance.GameState;
            _lastStateHandled = false;
        }
    }

    private void HandleStarting()
    {
        if (!_lastStateHandled)
        {
            CenterText.text = GameInfoStatic.StartCenterText;
            _lastStateHandled = true;
        }

        // Shows center sequence text
        if (GameInfo.Instance.GameTicks > _lastStateTicket + 2)
        {
            CenterText.text = GameInfoStatic.GoCenterText;
        }
        if (GameInfo.Instance.GameTicks > _lastStateTicket + 3)
        {
            HandleStart();
        }
    }

    private void HandleStart()
    {
        CenterText.text = "";
        GameInfo.Instance.GameState = GameState.Running;
    }

    private void HandlePlayerLoose()
    {
        if (!_lastStateHandled)
        {
            _lastStateHandled = true;
            GameInfo.Instance.PlayerLives--;
            CenterText.text = GameInfoStatic.DiedCenterText;
        }

        if (GameInfo.Instance.GameTicks > _lastStateTicket + 2)
        {
            if (GameInfo.Instance.PlayerLives == 0)
            {
                CenterText.text = "";
                LevelTime.text = "";
                PlayerScore.text = "";
                PlayerLives.text = "";

                StartCoroutine(LoadStartScene());
            }

            InitializeScene();
        }
    }

    private void HandlePlayerWin()
    {
        if (!_lastStateHandled)
        {
            _lastStateHandled = true;
            GameInfo.Instance.PlayerScore += GameInfoStatic.KillScore;
            CenterText.text = GameInfoStatic.WinCenterText;
        }

        if (GameInfo.Instance.GameTicks > _lastStateTicket + 2)
        {
            InitializeScene();
        }
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

    private void UpdateGameTicket_Tick()
    {
        GameInfo.Instance.GameTicks += 1;
    }

    private void UpdateTimeText_Tick()
    {
        if (GameInfo.Instance.GameState != GameState.Running) return;

        GameInfo.Instance.LevelTime--;
    }

    private void InitGameTimers()
    {
        InvokeRepeating(_updateGameTicke_Tick, 0.0f, 1.0f);
        InvokeRepeating(_updateTimeText_Tick, 0.0f, 1.0f);
    }
}