using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    //Establish array containing each Wave game object, to be set in the editor.
    [SerializeField]
    private GameObject[] wavesArray;
    private int maxWaves;

    //Establish a variable to track the current wave.
    [SerializeField]
    private int waveNumber;

    //Boolean used to determine if a wave is currently active (there are still enemies left).
    private bool isWaveActive;

    //Text displayed on the UI to show the current wave.
    public Text waveText;

    //Reference to the overall Game Manager object; needed to inform the system that the player has won.
    public GameObject gameManager;

    void Start()
    {
        //Set the current wave to 0 (to correspond with the array's index) and establish that there currently isn't wave active.
        waveNumber = 0;
        isWaveActive = false;
        maxWaves = wavesArray.Length;
    }


    void Update()
    {
        var gameScript = gameManager.GetComponent<GameController>();

        //Set the UI text to display the current wave number.
        waveText.text = (waveNumber + 1).ToString();

        if (!isWaveActive)
        {
            //If there isn't a wave currently active, instantiate the correct wave from the array.
            var newWave = Instantiate(wavesArray[waveNumber], transform.position, transform.rotation);
            newWave.transform.SetParent(gameObject.transform);
            isWaveActive = true;
        }

        if (allDead() && ((waveNumber + 1) < maxWaves))
        {
            //If all enemies are dead, end the current wave and bump the current wave up to the next.
            isWaveActive = false;
            waveNumber++;
        }
        else if (allDead() && ((waveNumber + 1) >= maxWaves))
        {
            //If all enemies are dead and the player has beat all waves, display the victory UI.
            gameScript.playerVictory = true;
        }
    }

    private bool allDead()
    {
        //Check for any remaining GO's with an Enemy tag.
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
