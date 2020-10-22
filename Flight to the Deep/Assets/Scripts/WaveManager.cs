using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Establish array containing each Wave game object, to be set in the editor.
    [SerializeField]
    private GameObject[] wavesArray;

    //Establish a variable to track the current wave.
    [SerializeField]
    private int waveNumber;

    //Boolean used to determine if a wave is currently active (there are still enemies left).
    private bool isWaveActive;

    void Start()
    {
        //Set the current wave to 0 (to correspond with the array's index) and establish that there currently isn't wave active.
        waveNumber = 0;
        isWaveActive = false;
    }


    void Update()
    {
        if (!isWaveActive)
        {
            //If there isn't a wave currently active, instantiate the correct wave from the array.
            Instantiate(wavesArray[waveNumber], transform.position, transform.rotation);
            isWaveActive = true;
        }

        if (allDead())
        {
            //If all enemies are dead, end the current wave and bump the current wave up to the next.
            isWaveActive = false;
            waveNumber++;
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
