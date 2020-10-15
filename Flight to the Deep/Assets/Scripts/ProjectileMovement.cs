using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    //Establish projectile variables.
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private float projectileDamage;

    [SerializeField]
    private float destroyDelay;

    //Establish bool to track if the projectile has hit anything.
    private bool noHit;
    void Start()
    {
        noHit = true;
    }

    void Update()
    {
        if (noHit)
            if (transform.parent.tag == "Player")
                transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
            else if (transform.parent.tag == "Enemy")
                transform.Translate(Vector3.back * Time.deltaTime * projectileSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            noHit = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            Destroy(gameObject, destroyDelay);
        }
    }
}
