using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float spinSpeed = 3600;
    public bool doSpin = false;

    private Rigidbody rb;

    public GameObject playerGrphics;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Rotation
    private void FixedUpdate()
    {
        if (doSpin)
        {
            playerGrphics.transform.Rotate(new Vector3(0, spinSpeed * Time.deltaTime, 0));
        }
    }
}
