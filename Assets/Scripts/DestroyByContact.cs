using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosionEffect;
    private bool hasExploded = false;
    private float explosionRadius;
    private float explosionForce;

    private ILogger _logger;

    // Start is called before the first frame update
    void Start()
    {
        _logger = new Logger();
        explosionRadius = 5;
        explosionForce = 700;
    }

    void OnTriggerEnter(Collider other)
    {
        _logger.LogInfo($"Collider ==> '{gameObject.tag}' X '{other.tag}'");

        if (other.tag != "GameController")
        {
            if (other.tag == GameInfoStatic.TagPlayer)
            {
                if (GameInfo.Instance.GameState == GameState.Running)
                {
                    Explode();
                    GameInfo.Instance.GameState = GameState.PlayerLoose;
                    other.gameObject.SetActive(false);
                }
            }
            else if (other.tag == GameInfoStatic.TagEnemy)
            {
                if (GameInfo.Instance.GameState == GameState.Running)
                {
                    Explode();
                    GameInfo.Instance.GameState = GameState.PlayerWin;
                    other.gameObject.SetActive(false);
                }
            }
            else
            {
                // Shot, Player or Enemy
                Destroy(other.gameObject); 
            }

            // Asteroid
            Destroy(gameObject); 
        }
    }

    private void Explode()
    {
        // Show effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // Get  nearby objects
        // add force

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObj in colliders)
        {
            var body = nearbyObj.GetComponent<Rigidbody>();
            if (body != null)
            {
                body.AddExplosionForce(explosionForce, transform.position, explosionRadius);;
            }
        }

        // Remove
        //Destroy(gameObject);
    }
}
