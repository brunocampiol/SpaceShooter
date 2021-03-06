﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float tilt;
    //public PlayerBoundary boundary;

    public Transform _shotSpawn;
    public GameObject _shot;

    private ILogger _logger;
    private PlayerBoundary _boundry;
    private Rigidbody _rigidBody;
    private float _fireRate;
    private float _speed;
    private float _tilt;
    private float _nextFire;
    private float _myTime = 0.0F;
    private string _horizontal;
    private string _vertical;

    // Start is called before the first frame update
    void Start()
    {
        _fireRate = GameInfoStatic.PlayerFireRate;
        _speed = GameInfoStatic.PlayerSpeed;
        _tilt = GameInfoStatic.PlayerTilt;
        _nextFire = GameInfoStatic.PlayerNextFire;

        _horizontal = GameInfoStatic.Horizontal;
        _vertical = GameInfoStatic.Vertical;

        _logger = new Logger();

        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.freezeRotation = true;

        _boundry = new PlayerBoundary();
        _boundry.xMax = GameInfoStatic.PlayableAreaBoundryX;
        _boundry.yMax = GameInfoStatic.PlayableAreaBoundryY;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameInfo.Instance.GameState != GameState.Running) return;  
         
        _myTime = _myTime + Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && _myTime > _nextFire)
        {
            _nextFire = _myTime + _fireRate;

            //_logger.LogInfo($"Spanw {_shotSpawn.rotation.x},{_shotSpawn.rotation.y},{_shotSpawn.rotation.z},{_shotSpawn.rotation.w}");

            //GameObject shot = Instantiate(_shot, _shotSpawn.position, _shotSpawn.rotation) as GameObject;
            Quaternion shotRotation = new Quaternion(_shotSpawn.rotation.x,0,0,_shotSpawn.rotation.w);
            GameObject shot = Instantiate(_shot, _shotSpawn.position, shotRotation) as GameObject;
            ShotController shotController = shot.GetComponent<ShotController>();
            shotController.SetDirection(Vector3.forward);

            //audioSource.Play();

            _nextFire = _nextFire - _myTime;
            _myTime = 0.0F;
        }
    }

    private void FixedUpdate()
    {
        if (GameInfo.Instance.GameState != GameState.Running) return;  
        
        float moveHorizontal = Input.GetAxis(_horizontal);
        float moveVertical = Input.GetAxis(_vertical);

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        _rigidBody.velocity = movement * _speed;

        _rigidBody.position = new Vector3(
            Mathf.Clamp(_rigidBody.position.x, _boundry.xMin, _boundry.xMax),
            Mathf.Clamp(_rigidBody.position.y, _boundry.yMin, _boundry.yMax),
            0.0f
        );

        _rigidBody.rotation = Quaternion.Euler(
           90 + (_rigidBody.velocity.y * -_tilt),
           0.0f,
           _rigidBody.velocity.x * -_tilt
        );
    }
}
