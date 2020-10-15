using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Establish the speed and health values of the Player object. Set as serialized to make quick changes in the editor.
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float playerHealth;

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

    void Start()
    {

    }

    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile;
            projectile = Instantiate(projectilePrefab, firingPoint.position, projectilePrefab.transform.rotation);
            projectile.transform.parent = gameObject.transform;
        }
    }
}
