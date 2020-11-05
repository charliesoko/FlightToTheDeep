using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    //Establish floats to use as the top and bottom boundaries for the ship object's movement.
    private float topBound;
    private float bottomBound;

    //Establish the range of movement of the ship object.
    [SerializeField]
    private float upRange;
    [SerializeField]
    private float downRange;

    //Get the initial position of the ship object.
    private Vector3 initialPosition;

    //Establish float to determine which direction the ship object should be moving, and one to determine how fast it'll move.
    private float moveDirection;
    [SerializeField]
    private float moveSpeed;

    //Establish a float to determine how fast the skybox will rotate.
    [SerializeField]
    private float skyboxSpeed;

    [SerializeField]
    private GameObject creditsUI;

    void Start()
    {
        //Get the initial ship object position, set the movement direction to 1 (up),
        //and establish the top and bottom boundaries by using their respective ranges.
        initialPosition = transform.position;
        moveDirection = 1;
        topBound = initialPosition.y + upRange;
        bottomBound = initialPosition.y - downRange;
        creditsUI.SetActive(false);
    }

    void Update()
    {
        //If the ship object is moving up and hasn't reached the top bound,
        //or moving down and hasn't reached the bottom, keep moving.
        if ((moveDirection == 1) && (transform.position.y < topBound))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
        }
        else if ((moveDirection == -1) && (transform.position.y > bottomBound))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
        }

        //If the ship object has reached either the top or bottom bound,
        //reverse the movement direction.
        if ((moveDirection == 1) && (transform.position.y >= topBound) || (moveDirection == -1) && (transform.position.y <= bottomBound))
        {
            moveDirection *= -1;
        }

        //Rotate the skybox based on the previously determined speed.
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxSpeed);
    }

    //Necessary functions to handle button behavior.
    public void StartClick()
    {
        //Load the main game scene.
        SceneManager.LoadScene("Prototype");
    }

    public void QuitClick()
    {
        //Quit the game.
        Application.Quit();
    }

    public void CreditsClick()
    {
        //Show the credits.
        creditsUI.SetActive(true);
    }

    public void CloseCreditsClick()
    {
        //Close the credits.
        creditsUI.SetActive(false);
    }
}
