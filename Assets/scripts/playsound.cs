using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playsound : MonoBehaviour
{

    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Play()
    {
        audioSource.Play();
        Debug.Log("Audiosource playing");
    }



}
