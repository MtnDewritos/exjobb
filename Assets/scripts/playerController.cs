using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(playerMotor))]
[RequireComponent(typeof(CapsuleCollider))]
public class playerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float sensitivity = 3f;

    private float nextPlay = 0f;
    

    AudioSource audioSource;

    private playerMotor motor;
    void Start()
    {
        motor = GetComponent<playerMotor>();
        
        audioSource = GetComponent<AudioSource>();
        
    }

    IEnumerator Footsteps(Vector3 velocity) //måste på nått sätt få den konstanta ändringen i velocity
    {
        if (velocity.x > 0.1 || velocity.z > 0.1)
        {
            float time = 1 / (velocity.x + velocity.y); 
            yield return new WaitForSeconds(time);
            Debug.Log("played sound");
            audioSource.Play();
        }
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

        if (audioSource != null)
        {
            //play footsteps with interval based on player speed
            float timeInterval = 0f;
            if (!(motor.Velocity.x == 0f && motor.Velocity.z == 0f))
            {
                float x = motor.Velocity.x;
                float z = motor.Velocity.z;
                if (x < 0)
                {
                    x *= -1;
                }
                if (z < 0)
                {
                    z *= -1;
                }
                if (x > 0.001 && z > 0.001) //so you don't take steps when basically not moving at all
                {

                    timeInterval = 1 / (x + z);
                    if (timeInterval > 2f) //so you don't end up with more than 2s delay between steps
                    {
                        timeInterval = 2f;
                    }
                }
                else
                {
                    timeInterval = 0f;
                }
            }
            else
            {
                timeInterval = 0f;
            }
            float time = Time.time;
            if (time >= nextPlay && timeInterval != 0f)
            {

                Debug.Log("playing footstep");
                nextPlay = time + timeInterval;
                audioSource.Play();
            }
        }
    }
    
   
    }


