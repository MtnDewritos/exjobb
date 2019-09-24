﻿using System.Collections;
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
                int nr = GameObject.Find("geometry").GetComponent<counter>().Nr;
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Time taken: " + Time.time + "s");
                    sw.WriteLine("Wall hits: " + nr);
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
