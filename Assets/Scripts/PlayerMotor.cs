using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float camRotation = 0f;
    private float currentCamRotation = 0f;
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
            currentCamRotation -= camRotation;
            currentCamRotation = Mathf.Clamp(currentCamRotation, -65, 65);

            cam.transform.localEulerAngles = new Vector3(currentCamRotation, 0f, 0f);
        }
        
    }

    //Takes CameraRotation vector
    public void CamRotate(float _camRotation)
    {
        camRotation = _camRotation;
    }

   
}
