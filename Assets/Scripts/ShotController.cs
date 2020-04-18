using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        //_speed = GameInfoStatic.DefaultShotSpeed;
        //_rigidBody = GetComponent<Rigidbody>();
        //_rigidBody.velocity = transform.up * _speed;

        // if (Direction == null || Direction != Vector3.zero)
        // {
        //     _rigidBody.velocity = transform.up * _speed;
        // }
        // else
        // {
        //     _rigidBody.velocity = Direction * _speed;
        // }
    }

    public void SetDirection(Vector3 direction)
    {
        //_rigidBody = GetComponent<Rigidbody>();
        
        _rigidBody.velocity = direction * GameInfoStatic.DefaultShotSpeed;
    }
}