﻿using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform _shotSpawn;
    public GameObject _shot;

    private ILogger _logger;

    private EnemyAI _AI;
    private PlayerBoundary _boundry;
    private GameObject _player;
    private Rigidbody _rigidBody;
    private Rigidbody _playerRigidBody;
    private float _fireRate;
    private float _speed;
    private float _tilt;
    private float _nextFire;
    private float _myTime = 0.0F;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(GameInfoStatic.TagPlayer);

        _logger = new Logger();

        _fireRate = GameInfoStatic.PlayerFireRate;
        _speed = GameInfoStatic.PlayerSpeed;
        _tilt = GameInfoStatic.PlayerTilt;
        _nextFire = GameInfoStatic.PlayerNextFire;

        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.freezeRotation = true;

        _playerRigidBody = _player.GetComponent<Rigidbody>();

        _boundry = new PlayerBoundary();
        _boundry.xMax = GameInfoStatic.PlayableAreaBoundryX;
        _boundry.yMax = GameInfoStatic.PlayableAreaBoundryY;

        _AI = new EnemyAI();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameInfo.Instance.GameState != GameState.Running) return;  

        _myTime = _myTime + Time.deltaTime;
        
        if (_myTime > _nextFire)
        {
            //_logger.LogInfo($"Spanw {_shotSpawn.position.x},{_shotSpawn.position.y},{_shotSpawn.position.z}");
            _nextFire = _myTime + _fireRate;

            Quaternion shotRotation = new Quaternion(_shotSpawn.rotation.x,0,0,_shotSpawn.rotation.w);
            //GameObject shot = Instantiate(_shot, _shotSpawn.position, _shotSpawn.rotation) as GameObject;
            GameObject shot = Instantiate(_shot, _shotSpawn.position, shotRotation) as GameObject;
            ShotController shotController = shot.GetComponent<ShotController>();
            shotController.SetDirection(Vector3.back);

            _nextFire = _nextFire - _myTime;
            _myTime = 0.0F;
        }
    }

    private void FixedUpdate()
    {
        if (GameInfo.Instance.GameState != GameState.Running) return;  
        if (_playerRigidBody == null) return; // avoids errors on console

        _rigidBody.velocity = _AI.GetMovement(_rigidBody.position, _playerRigidBody.position, _speed);

        _rigidBody.rotation = Quaternion.Euler(
              90 + (_rigidBody.velocity.y * _tilt),
              0.0f,
              _rigidBody.velocity.x * _tilt
          );
    }

}
