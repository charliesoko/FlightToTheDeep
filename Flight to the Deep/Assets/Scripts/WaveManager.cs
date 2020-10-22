using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wavesArray;

    [SerializeField]
    private int waveNumber;

    private bool isWaveActive;

    void Start()
    {
        waveNumber = 0;
        isWaveActive = false;
    }


    void Update()
    {
        if (!isWaveActive)
        {
            Instantiate(wavesArray[waveNumber], transform.position, transform.rotation);
            isWaveActive = true;
        }

        if (allDead())
        {
            isWaveActive = false;
            waveNumber++;
        }
    }

    private bool allDead()
    {
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
