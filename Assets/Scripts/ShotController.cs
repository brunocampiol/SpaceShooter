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
    }

    public void SetDirection(Vector3 direction)
    {
        _rigidBody.velocity = direction * GameInfoStatic.DefaultShotSpeed;
    }
}