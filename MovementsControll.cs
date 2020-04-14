using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementsControll : MonoBehaviour
{

    public Joystick joystick;
    public float speed = 4f;
    private Vector3 velocityVector = Vector3.zero; //initial velocity

    public float maxVelocityChange = 2f;
    private Rigidbody rb;

    public float tiltAMount = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Joystick inputs
        float _xMovementInput = joystick.Horizontal;
        float _yMovementInput = joystick.Vertical;

        //Calculating velocity vectors
        Vector3 _movementHorizontal = transform.right * _xMovementInput;
        Vector3 _movementVertical = transform.forward * _yMovementInput;

        //Final movements vector
        Vector3 _movementVelocityVector = (_movementVertical + _movementHorizontal).normalized * speed;

        //Apply movement
        Move(_movementVelocityVector);
        transform.rotation = Quaternion.Euler(joystick.Vertical * speed * tiltAMount, 0, -1 * joystick.Horizontal * speed * tiltAMount);
    }
    void Move(Vector3 movementVelocityVector)
    {
        velocityVector = movementVelocityVector;
    }

    private void FixedUpdate()
    {
        if(velocityVector != Vector3.zero)
        {
            //Get Rigidbody current velocity
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (velocityVector - velocity);

            //Apply a force by the amount of velocity change to reach the target velocity   
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0f;

            rb.AddForce(velocityChange, ForceMode.Acceleration);
        }

       
    }
}
