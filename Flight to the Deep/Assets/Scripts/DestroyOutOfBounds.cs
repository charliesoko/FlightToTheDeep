using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    //Establish the top and bottom boundaries.
    [SerializeField]
    private float topBound;
    [SerializeField]
    private float bottomBound;

    void Start()
    {
        
    }

    void Update()
    {
        //Check the object's position relative to the boundaries, and destroy it if it's out of the game space.
        if ((transform.position.z > topBound) || (transform.position.z < bottomBound))
            Destroy(gameObject);
    }
}
