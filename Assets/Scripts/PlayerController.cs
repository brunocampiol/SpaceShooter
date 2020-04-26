using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform _shotSpawn;
    public GameObject _shot;

    private ILogger _logger;
    private PlayerBoundary _boundry;
    private Rigidbody _rigidBody;
    private Transform _playerTransform;
    private float _fireRate;
    private float _speed;
    private float _tilt;
    private float _nextFire;
    private float _myTime = 0.0F;
    private string _horizontal;
    private string _vertical;

    public PlayerController()
    {
        _logger = new Logger();

        _fireRate = GameInfoStatic.PlayerFireRate;
        _speed = GameInfoStatic.PlayerSpeed;
        _tilt = GameInfoStatic.PlayerTilt;
        _nextFire = GameInfoStatic.PlayerNextFire;

        _horizontal = GameInfoStatic.Horizontal;
        _vertical = GameInfoStatic.Vertical;

        _boundry = new PlayerBoundary();
        _boundry.xMax = GameInfoStatic.PlayableAreaBoundryX;
        _boundry.yMax = GameInfoStatic.PlayableAreaBoundryY;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.freezeRotation = true;

        _playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameInfo.Instance.GameState != GameState.Running) return;

        _myTime = _myTime + Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && _myTime > _nextFire)
        {
            _nextFire = _myTime + _fireRate;

            Quaternion shotRotation = Quaternion.Euler(GameInfoStatic.DefaultShotRotation);
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
        // TODO: find better fix
        // Avoids enemy to keep moving after round finished
        if (GameInfo.Instance.GameState == GameState.Starting ||
            GameInfo.Instance.GameState == GameState.PlayerLoose ||
            GameInfo.Instance.GameState == GameState.PlayerWin)
        {
            SetPlayerPosition(0, 0);
        }

        if (GameInfo.Instance.GameState != GameState.Running) return;

        float moveHorizontal = Input.GetAxis(_horizontal);
        float moveVertical = Input.GetAxis(_vertical);

        SetPlayerPosition(moveVertical, moveHorizontal);
    }

    private void SetPlayerPosition(float vertical, float horizontal)
    {
        Vector3 movement = new Vector3(horizontal, vertical, 0.0f);
        _rigidBody.velocity = movement * _speed;

        _rigidBody.position = new Vector3(
            Mathf.Clamp(_rigidBody.position.x, _boundry.xMin, _boundry.xMax),
            Mathf.Clamp(_rigidBody.position.y, _boundry.yMin, _boundry.yMax),
            0.0f
        );

        // SETS PLAYER ROTATION EACH FRAME
        _rigidBody.rotation = Quaternion.Euler(
           270 + (_rigidBody.velocity.y * -_tilt),
           0.0f,
           _rigidBody.velocity.x * -_tilt
        );
    }
}
