using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerMovement : MonoBehaviour {

    ConfigurableJoint joint;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float mouseSentivity = 5f;

    [SerializeField]
    float thrustforce = 1200f;

    [Header("Spring Settings:")]
    /*
     [SerializeField]
    JointDriveMode jointMode = JointDriveMode.Position;
    */

    [SerializeField]
    float jointSpring = 20f; 
    [SerializeField]
    float jointMaxForce = 40f;
    

    PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        SetJointSettings(jointSpring);
    }

    private void Update()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;
        
        //Calculated final movement vector
        Vector3 _velocity = (movHorizontal + movVertical).normalized * speed;

        //Apply Movement
        motor.Move(_velocity);

        //Calculate PlayerRotation along yaxis
        float yRot = Input.GetAxisRaw("Mouse X");
        
        Vector3 _rotation = new Vector3(0, yRot, 0) * mouseSentivity;

        //Apply Rotation
        motor.Rotate(_rotation);

        //Calculate CameraRotation along xaxis
        float xRot = Input.GetAxisRaw("Mouse Y") * mouseSentivity;

        //Apply CameraRotaion
        motor.CamRotate(xRot);

        //Thruster Force
        Vector3 _thrusterForce = Vector3.zero;
        /*
         float thrust = Input.GetAxisRaw("Jump");
        Vector3 _thrusterForce = Vector3.up * thrust * thrustforce;
         */

        if (Input.GetButtonDown("Jump"))
        {
            _thrusterForce = Vector3.up * thrustforce;
            SetJointSettings(0);
        }
        else
        {
            SetJointSettings(jointSpring);
        }
        //Applying thrusterForce
        motor.ApplyThruster(_thrusterForce);
    }

    void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            //mode = jointMode,
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }   
}
