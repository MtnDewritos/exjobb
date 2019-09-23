using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endlevel : MonoBehaviour
{
    [SerializeField]
    private string nextLevel;
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
            if(nextLevel != null)
            {
                SceneManager.LoadScene(nextLevel);
            }
            
        }
        
    }
}
