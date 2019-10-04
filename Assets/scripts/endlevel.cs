using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class endlevel : MonoBehaviour
{
    [SerializeField]
    private string nextLevel;
    [SerializeField]
    private string filename;
    private bool isEnd = false;
    public void IsEnd(bool value)
    {
        this.isEnd = value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isEnd)
        {
            string path = @"c:\temp\" + filename;
            if (!File.Exists(path))
            {
                int nr = -1;
                if (SceneManager.GetActiveScene().name == "room" || SceneManager.GetActiveScene().name == "room_noraycast")
                {
                    nr = GameObject.Find("geometry").GetComponent<counter>().Nr;
                }
                else if (SceneManager.GetActiveScene().name == "tutorial_room")
                {
                    nr = 0;
                }
                else
                {
                    nr = GameObject.Find("Enemy (1)").GetComponent<counter>().Nr;
                    nr += GameObject.Find("Enemy (2)").GetComponent<counter>().Nr;
                    nr += GameObject.Find("Enemy (3)").GetComponent<counter>().Nr;
                    nr += GameObject.Find("Enemy (4)").GetComponent<counter>().Nr;
                }
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Time taken: " + Time.time + "s");
                    sw.WriteLine("Hits: " + nr);
                }
            }
            //write to file "Time taken " + Time.time
            if (nextLevel != "")
            {
                SceneManager.LoadScene(nextLevel);
            }
            
        }
        
    }
}
