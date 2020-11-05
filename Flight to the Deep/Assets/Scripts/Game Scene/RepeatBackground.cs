using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    //Establish variables for how fast the background should move, where the starting position is, and when to repeat the background.
    [SerializeField]
    private float moveSpeed;
    private Vector3 startPos;
    private float repeatLength;

    void Start()
    {
        startPos = transform.position;
        repeatLength = GetComponent<BoxCollider>().size.z;
    }

    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);

        if (transform.position.z < startPos.z - repeatLength)
        {
            transform.position = startPos;
        }
    }
}
