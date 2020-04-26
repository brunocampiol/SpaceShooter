using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public GameObject explosionEffect;
    private bool hasExploded = false;
    private float explosionRadius;
    private float explosionForce;

    public ExplosionController()
    {
        explosionRadius = 5;
        explosionForce = 700;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasExploded)
        {
            Explode();
            hasExploded = true;
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
        Destroy(gameObject);
    }
}
