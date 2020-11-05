using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Bool to determine if the game is currently paused.
    public bool isPaused;

    //Bool used to determine if the player has started the game.
    public bool isStarted;

    //Bools to track if the player has won or lost.
    public bool playerVictory;
    public bool playerLoss;

    //Gather the necessary GameObjects to enable and disable them as needed.
    public GameObject startUI;
    public GameObject pauseUI;
    public GameObject deathUI;
    public GameObject victoryUI;
    public AudioSource backgroundMusic;

    void Start()
    {
        //Because the game hasn't started yet, pause it and deactivate all but the initial starting UI.
        isPaused = true;
        Time.timeScale = 0;
        isStarted = false;
        pauseUI.SetActive(false);
        playerLoss = false;
        deathUI.SetActive(false);
        playerVictory = false;
        victoryUI.SetActive(false);
    }

    void Update()
    {
        //Check if the game is paused or not, and pause or unpause it as needed.
        if (Input.GetKeyDown(KeyCode.Escape) && (!isPaused) && (isStarted))
        {
            Time.timeScale = 0;
            backgroundMusic.Pause();
            isPaused = true;
            pauseUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (isPaused) && (isStarted))
        {
            Time.timeScale = 1;
            isPaused = false;
            backgroundMusic.Play();
            pauseUI.SetActive(false);
        }

        //Press space to move past the initial instructions that appear upon start.
        if (Input.GetKeyDown(KeyCode.Space) && (!isStarted))
        {
            startUI.SetActive(false);
            isPaused = false;
            isStarted = true;
            Time.timeScale = 1;
        }

        //Display the correct UI if the player has lost.
        if (playerLoss)
        {
            isStarted = false;
            deathUI.SetActive(true);
            Time.timeScale = 0;
        }

        //Display the correct UI if the player has won.
        if (playerVictory)
        {
            isStarted = false;
            victoryUI.SetActive(true);
            Time.timeScale = 0;
        }

        //Controls for when the game is paused, you've lost, or you've won.
        if ((isPaused) || (playerVictory) || (playerLoss))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Prototype");
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
