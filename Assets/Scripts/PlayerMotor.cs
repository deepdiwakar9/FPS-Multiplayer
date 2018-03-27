using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 camRotation = Vector3.zero;
    private Rigidbody rb;
    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField]
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    //Taking ThrusterForce
    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    //Takes movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Runs Physics Iteration
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    //Performs Movement based on velocity
    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if(thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce*Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    //Takes rotation vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Performs Rotation
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            cam.transform.Rotate(camRotation);
        }
        
    }

    //Takes CameraRotation vector
    public void CamRotate(Vector3 _camRotation)
    {
        camRotation = _camRotation;
    }

   
}
