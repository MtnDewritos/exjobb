using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> nodes = new List<GameObject>();
    private float nextMove = 0;
    private float timeInterval = 3f;
    private int currentNode = 0;
    private float rayDist = 10f;
    private float viewDist = 6f;
    private float backViewDist = 1f;
    private float speed = 3f;
    private bool chase = false;
    private bool ret = false;
    //private float chaseSpeed; //kanske ge spelaren en sprint också

    private int layerMask = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Look();
        float time = Time.time;
        if (ret)
        {
            MoveToNode();
        }
        if(time > nextMove && !chase && !ret)
        {
            MoveToNextNode();
            nextMove = time + timeInterval;
        }
        else if(time < nextMove && !chase && !ret)
        {
            IdleRotate();
        }
                     
    }
    private void MoveToNode()
    {
        Vector3 position = nodes[currentNode].transform.position;
        
        if (transform.position.normalized != position.normalized)
        {
            if(transform.position.normalized.x != position.normalized.x)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (transform.position.normalized.z != position.normalized.z)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }            
        }
        else
        {
            ret = false;
        }
        transform.Rotate(nodes[currentNode].transform.rotation.eulerAngles);
        Debug.Log(nodes[currentNode].transform.rotation.eulerAngles);


    }
    private void MoveToNextNode()
    {
        MoveToNode();
        if(currentNode < nodes.Count -1)
        {
            currentNode++;
        }
        else
        {
            currentNode = 0;
        }
        
    }
    private void Chase(Vector3 position)
    {
        if (transform.position.normalized != position.normalized)
        {
            if (transform.position.normalized.x != position.normalized.x)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (transform.position.normalized.z != position.normalized.z)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
        }
                       
    }
    private void RotateToFace(Vector3 position)
    {
        float rotationSpeed = 45f;
        Vector3 direction = position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
    private void IdleRotate() //rotate when idle to look 10 degrees to the left and the right quickly so that the player can't just hug a wall to avoid detection
    {
        float rotationSpeed = 45f; //change if too fast to detect player
         
       // Vector3 direction = something - transform.position;
      //  Quaternion lookRotation = Quaternion.LookRotation(direction);
     //   transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "player")
        {
            chase = false;
            ret = true;
        }
        
    }
    private void Look()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDist, layerMask))
        {
            Debug.Log("raycast hit");
            if (hit.distance < viewDist)
            {
                Debug.Log("hit within view distance");
                Vector3 position = hit.collider.transform.position;
                chase = true;
                Chase(position);
            }
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, backViewDist, layerMask))
        {
            Debug.Log("hit within back view distance");
            Vector3 position = hit.collider.transform.position;
            RotateToFace(position);
        }
        else
        {
            ret = true;
            chase = false;
        }


    }
}
