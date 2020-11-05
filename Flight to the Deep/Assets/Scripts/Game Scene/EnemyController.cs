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

    //Float to determine the firing interval of the Enemy.
    [SerializeField]
    private float fireDelay;

    //Various variables to manage the Enemy attack sequence.
    private bool isReadyForCombat = false;
    private bool isFiring = false;
    private IEnumerator firing;

    //Establish the prefab for the projectile as well as the Transform used as a launch point for the projectile.
    public GameObject projectilePrefab;
    public Transform firingPoint;

    void Start()
    {
        //Establish the coroutine needed to put the Enemy in a firing state.
        firing = Attack(fireDelay);
    }


    void Update()
    {
        //If the Enemy is still moving down to the screen, keep moving until it reaches its lower boundary. Then set it as ready to attack.
        if (transform.position.z > lowerBound)
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            isReadyForCombat = true;
        }

        //If the Enemy is ready to attack, start strafing back and forth and firing.
        if (isReadyForCombat)
        {
            Strafe();
            if (!isFiring)
            {
                StartCoroutine(firing);
            }
        }

        //If the Enemy has been killed, stop firing, stop moving, and destroy the Enemy.
        if (enemyHealth == 0)
        {
            StopCoroutine(firing);
            enemyHealth = -1;
            strafeSpeed = 0;
            MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer render in meshes)
            {
                render.enabled = false;
            }
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
        //If the Enemy is hit by a projectile, subtract the Player's attack from the Enemy's health.
        if (collision.gameObject.CompareTag("Projectile"))
        {
            PlayerController playerScript = collision.collider.GetComponentInParent<PlayerController>();
            float damage = playerScript.attackDamage;

            if (enemyHealth > 0)
                enemyHealth -= damage;
        }
        //If the Enemy collides with another Enemy, reverse its strafe direction.
        else if (collision.collider.CompareTag("Enemy"))
        {
            strafeDirection *= -1;
        }
    }

    //Establish how the Enemy will operate in its firing sequence, including a delay in between shots.
    private IEnumerator Attack(float fireDelay)
    {
        while (true)
        {
            isFiring = true;
            yield return new WaitForSeconds(fireDelay);
            var bullet = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
            bullet.transform.SetParent(gameObject.transform, true);
            yield return new WaitForSeconds(fireDelay);
            isFiring = false;
        }
    }
}
