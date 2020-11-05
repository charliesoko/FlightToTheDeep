using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Establish the speed, health, and attack values of the Player object. Set as serialized to make quick changes in the editor.
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float playerHealth;
    private float maxHealth;
    [SerializeField]
    public float attackDamage;

    //Establish that we need to get both horizontal and vertical input to move the Player.
    private float horizontalInput;
    private float verticalInput;

    //Establish boundaries to keep the Player within the game space. Set as serialized to make quick changes in the editor.
    [SerializeField]
    private float leftBound;
    [SerializeField]
    private float rightBound;
    [SerializeField]
    private float forwardBound;
    [SerializeField]
    private float backBound;

    //Establish the projectile prefab to be used for firing, as well as the transform used to establish where the projectile is fired from.
    public GameObject projectilePrefab;
    public Transform firingPoint;

    //Get the health bar UI image to change when the player is damaged.
    public Image healthBar;

    //Establish the overall Game Manager to handle a Player loss.
    public GameObject gameManager;

    //Get the SFX for the Player.
    public AudioSource shotFired;
    public AudioSource damageTaken;

    void Start()
    {
        //Set the max health to current health (always max at start).
        maxHealth = playerHealth;
    }

    void Update()
    {
        var gameScript = gameManager.GetComponent<GameController>();

        //Get the current input values.
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Get the Player's rigidbody component.
        Rigidbody rb = GetComponent<Rigidbody>();

        //Check the Player's position relative to the boundaries, and stop them from moving out of the game space.
        if (transform.position.x < leftBound)
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        if (transform.position.x > rightBound)
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
        if (transform.position.z > forwardBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, forwardBound);
        if (transform.position.z < backBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, backBound);

        //Move the Player based on the horizontal and vertical input, as well as the speed value.
        rb.AddForce(horizontalInput * Time.deltaTime * playerSpeed, 0, verticalInput * Time.deltaTime * playerSpeed, ForceMode.Impulse);

        //Instantiate a projectile object if the Space key is pressed; set its parent to the Player.
        if (Input.GetKeyDown(KeyCode.Space) && (gameScript.isPaused == false))
        {
            GameObject projectile;
            projectile = Instantiate(projectilePrefab, firingPoint.position, projectilePrefab.transform.rotation);
            projectile.transform.parent = gameObject.transform;
            shotFired.Play();
        }

        //If the Player's health reaches zero, stop them from moving, destroy the Game Object, and inform the Game Manager that they have lost.
        if (playerHealth <= 0)
        {
            playerHealth = -1;
            playerSpeed = 0;
            MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer render in meshes)
            {
                render.enabled = false;
            }
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 1.5f);
            gameScript.playerLoss = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //When hit by an Enemy projectile, play the corresponding SFX, reduce the Player's health by the Enemy's attack damage, and adjust the UI.
        if (collision.gameObject.CompareTag("Projectile"))
        {
            damageTaken.Play();
            EnemyController enemyScript = collision.collider.GetComponentInParent<EnemyController>();
            float damage = enemyScript.enemyDamage;

            if (playerHealth > 0)
                playerHealth -= damage;
                healthBar.fillAmount = playerHealth / maxHealth;
        }
    }
}
