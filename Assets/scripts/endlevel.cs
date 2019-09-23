using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endlevel : MonoBehaviour
{
    private bool isEnd = false;
    public void IsEnd(bool value)
    {
        this.isEnd = value; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isEnd)
        {
            Debug.Log("end this level");
            //write to file "Time taken " + Time.time
            //SceneManager.LoadScene("maze");
        }
        
    }
}
