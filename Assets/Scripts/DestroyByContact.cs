﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    private ILogger _logger;

    // Start is called before the first frame update
    void Start()
    {
          _logger = new Logger();    
    }

    void OnTriggerEnter(Collider other)
	{
        if (other.tag != "GameController")
        {
            _logger.LogInfo($"DestroyByContact gameObject'{gameObject.tag}' & other'{other.tag}'");

            if (other.tag == GameInfoStatic.TagPlayer) 
            {
                if (GameInfo.Instance.GameState == GameState.Running)
                {
                    GameInfo.Instance.GameState = GameState.PlayerLoose;
                    other.gameObject.SetActive(false);
                }
            }
            else if (other.tag == GameInfoStatic.TagEnemy) 
            {
                if (GameInfo.Instance.GameState == GameState.Running)
                {
                    GameInfo.Instance.GameState = GameState.PlayerWin;
                    other.gameObject.SetActive(false);
                }
            }
            else
            {
                Destroy(other.gameObject); // Shot, Player or Enemy
            }
            
		    Destroy(gameObject); // Asteroid
        }
	}
}