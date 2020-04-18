using System.Collections;
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
            _logger.LogInfo($"DestroyByContact {gameObject.tag} & {other.tag}");

            if (other.tag == GameInfoStatic.TagPlayer) 
            {
                GameInfo.Instance.GameState = GameState.PlayerLoose;
                //other.gameObject.transform.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
            }
            else if (other.tag == GameInfoStatic.TagEnemy) 
            {
                GameInfo.Instance.GameState = GameState.PlayerWin;
                //other.gameObject.transform.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
            }
            else
            {
                Destroy(other.gameObject); // Shot, Player or Enemy
            }
            
		    Destroy(gameObject); // Asteroid
        }
	}
}
