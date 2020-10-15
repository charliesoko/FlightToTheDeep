using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Vector3 startPos;
    private float repeatLength;

    void Start()
    {
        startPos = transform.position;
        repeatLength = GetComponent<BoxCollider>().size.z / 2;
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
