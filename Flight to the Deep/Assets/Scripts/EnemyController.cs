using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Establish Enemy health and attack damage variables.
    [SerializeField]
    private float enemyHealth;
    [SerializeField]
    public float enemyDamage;
    [SerializeField]
    private float destroyDelay;

    //Establish two speed variables, one for when the Enemy is advancing and one for strafing left and right.
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float strafeSpeed;

    //Establish boundaries to prevent the Enemy from moving out of the game space or too close to the Player.
    [SerializeField]
    private float lowerBound;
    [SerializeField]
    private float leftBound;
    [SerializeField]
    private float rightBound;

    //Float to be used for determining if the Enemy is strafing left or right.
    private float strafeDirection = 0;

    void Start()
    {
        
    }


    void Update()
    {
        if (transform.position.z > lowerBound)
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            Strafe();
        }

        if (enemyHealth == 0)
        {
            enemyHealth = -1;
            strafeSpeed = 0;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, destroyDelay);
        }
    }
    
    //Method to manage strafing left and right.
    private void Strafe()
    {
        if (strafeDirection == 0)
        {
            strafeDirection = Random.Range(-1, 1);
        }

        if (transform.position.x < leftBound)
        {
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
            strafeDirection *= -1;
        }

        if (transform.position.x > rightBound)
        {
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
            strafeDirection *= -1;
        }

        if (strafeDirection == -1)
        {
            transform.Translate(Vector3.left * strafeSpeed * Time.deltaTime, Space.World);
        }

        if (strafeDirection == 1)
        {
            transform.Translate(Vector3.right * strafeSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            PlayerController playerScript = collision.collider.GetComponentInParent<PlayerController>();
            float damage = playerScript.attackDamage;

            if (enemyHealth > 0)
                enemyHealth -= damage;
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
            strafeDirection *= -1;
        }
    }
}
