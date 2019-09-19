using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(playerMotor))]
[RequireComponent(typeof(BoxCollider))]
public class playerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float sensitivity = 3f;

    AudioSource audioSource;

    private BoxCollider collider;
    private playerMotor motor;
    void Start()
    {
        motor = GetComponent<playerMotor>();
        collider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();

    }



    void Update()
    {
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _yMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _yMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized;// * speed;

        motor.Move(_velocity);

        //left right turning
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * sensitivity;
        motor.Rotate(_rotation);

        //up down looking 
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * sensitivity;
        motor.RotateCamera(_cameraRotation);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log("collision");
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude < 10)
            Debug.Log("collision at speed");
             Debug.Log(collision.relativeVelocity.magnitude);
             audioSource.Play();
        }
    }


