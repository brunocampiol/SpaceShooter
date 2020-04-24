using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidBody;
    private ILogger _logger;

    public ShotController()
    {
        _logger = new Logger();
        
    }

    public void SetDirection(Vector3 direction)
    {
        _rigidBody = this.gameObject.GetComponent<Rigidbody>();
        _rigidBody.velocity = direction * GameInfoStatic.DefaultShotSpeed;
        // TODO: fix object roration
        //_rigidBody.rotation = new Quaternion(90,0,0,0);
    }
}