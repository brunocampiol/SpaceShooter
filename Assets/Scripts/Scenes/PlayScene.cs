using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class PlayScene : MonoBehaviour
{
    public Text LevelTime;
    public Text PlayerScore;
    public Text PlayerLives;
    public Text CenterText;

    public GameObject PlayerObject;
    public GameObject EnemyObject;

    private ILogger _logger;
    private GameObject _player;
    private GameObject _enemy;

    // Start is called before the first frame update
    void Start()
    {
        _logger = new Logger();

        _enemy = GameObject.FindGameObjectWithTag(GameInfoStatic.TagEnemy);
        _player = GameObject.FindGameObjectWithTag(GameInfoStatic.TagPlayer);

        InitializeScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameInfo.Instance.GameState == GameState.Paused) return;  

        // Check for events
        if (GameInfo.Instance.GameState == GameState.PlayerLoose)
        {
            GameInfo.Instance.PlayerLives--;
            SetGameOver("You Died");
            InitializeScene();
        }
        else if (GameInfo.Instance.GameState == GameState.PlayerWin)
        {
            GameInfo.Instance.PlayerScore += GameInfoStatic.KillScore;
            SetGameOver("You Win");
            InitializeScene();
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

    private void SetGameOver(string centerText)
    {
        //PlayerScore.text = String.Empty;
        //PlayerLives.text = String.Empty;
        CenterText.text = centerText;

        CancelInvoke("UpdateTimeText_Tick");

        new WaitForSeconds(5);
    }

    private void InitializeScene()
    {
        GameInfo.Instance.LevelTime = GameInfoStatic.DefaultLevelTime;
        GameInfo.Instance.PlayerLives = GameInfoStatic.DefaultPlayerLives;
        GameInfo.Instance.PlayerScore = GameInfoStatic.DefaultPlayerScore;

        // Sets player in the middle
        _player.transform.position = Vector3.zero;
        // Sets enemy in the middle
        _enemy.transform.position = new Vector3(0,0,50);

        _player.SetActive(true);
        _enemy.SetActive(true);

        // CenterText.text = GameInfoStatic.StartCenterText;
        // new WaitForSeconds(2);
        // CenterText.text = GameInfoStatic.GoCenterText;
        // new WaitForSeconds(0.5F);
        
        CenterText.text = String.Empty;

        InvokeRepeating("UpdateTimeText_Tick", 0.0f, 1.0f);
        GameInfo.Instance.GameState = GameState.Running;
    }

    private void UpdateTimeText_Tick()
    {
        if (GameInfo.Instance.GameState != GameState.Running) return;  

        GameInfo.Instance.LevelTime--;
    }
}