using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidController : MonoBehaviour
{
    //Establish a GO whose Transform will be used as the center point.
    [SerializeField]
    private GameObject center;

    //Establish the angle of rotation.
    [SerializeField]
    private float rotationAngle;

    void Start()
    {
        
    }

    void Update()
    {
        //Rotate the game objects around the center point at the designated angle.
        transform.RotateAround(center.transform.position, Vector3.up, rotationAngle * Time.deltaTime);
    }
}
