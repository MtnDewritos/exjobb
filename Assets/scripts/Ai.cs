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
    private float speed = 0.5f;
    private bool chase = false;
    private bool ret = false;
    private bool setTime = true;
    //private float chaseSpeed; //kanske ge spelaren en sprint också

    private int layerMask = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Look();
        MoveToNode();
       /* float time = Time.time;
        if (ret)
        {
            MoveToNode();
        }
        if(time > nextMove && !chase && !ret)
        {
            MoveToNode();
            nextMove = time + timeInterval;
        }
        else if(time < nextMove && !chase && !ret)
        {
            IdleRotate();
        }
        */
                     
    }
    private void MoveToNode()
    {
        Vector3 nodePosition = nodes[currentNode].transform.position;
        Vector3 position = transform.position;
        float nodeZ = Mathf.Round(nodePosition.z * 100f) / 100f; //rounding because floats suck
        float nodeX = Mathf.Round(nodePosition.x * 100f) / 100f;
        float z = Mathf.Round(position.z * 100f) / 100f;
        float x = Mathf.Round(position.x * 100f) / 100f;

        if (x < nodeX)
            {

            Debug.Log(x);
            Debug.Log(nodeX);
            transform.Translate(0, 0, speed * Time.deltaTime);
            }
            else if (x > nodeX)
            {
            Debug.Log(x);
            Debug.Log(nodeX);
            transform.Translate(0, 0, speed * Time.deltaTime);
            }
            else if (z < nodeZ)
            {
                Debug.Log(z);
                Debug.Log(nodeZ);
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (z > nodeZ)
            {
                Debug.Log(z);
                Debug.Log(nodeZ);
            transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (z == nodeZ && x == nodeX)
            {
            float rotation = Mathf.Round(transform.rotation.eulerAngles.y * 100f) / 100f;
            float angle; //NULLPOINTER EXCEPTION POSSIBLE!!?!?
            
            ret = false;
            if(rotation != (Mathf.Round(nodes[currentNode].transform.rotation.eulerAngles.y * 100f) / 100f) && setTime)
            {
                RotateToAngle(nodes[currentNode].transform.rotation.eulerAngles.y);
            }
            else
            {
                Debug.Log("rotation: " + transform.rotation.eulerAngles.y + " node rotation: " + nodes[currentNode].transform.rotation.eulerAngles.y);
                if (setTime)
                {
                    nextMove = Time.time + timeInterval;
                    Debug.Log("set time to wait for to: " + nextMove + " from: " + Time.time);
                    setTime = false;
                }
                else
                {
                    if (nodes[NextNode].transform.position.z > z)
                    {
                        angle = 0f;
                    }
                    else if (nodes[NextNode].transform.position.x > x)
                    {
                        angle = 90f;
                    }
                    else if (nodes[NextNode].transform.position.z < z)
                    {
                        angle = 180f; 
                    }
                    else if (nodes[NextNode].transform.position.x < x)
                    {
                        angle = 270;   
                    }
                    else
                    {
                        Debug.Log("current and next node at same position");
                        angle = 0f;
                    }

                    if (Time.time > nextMove)
                    {
                        RotateToAngle(angle);
                        Debug.Log("current rotation: " + transform.rotation.eulerAngles.y);
                        if (rotation == angle)
                        {
                            Debug.Log("finished rotating");
                            setTime = true;
                            currentNode = NextNode;
                        }


                    }
                    else
                    {
                        float lookAngle = nodes[currentNode].transform.rotation.eulerAngles.y;
                        if(nextMove - Time.time < 1)
                        {
                            Debug.Log("time is short " + (nextMove - Time.time));
                            IdleRotate(lookAngle, 3);
                        }
                        else if (nextMove - Time.time < 2)
                        {
                            Debug.Log("time is medium " + (nextMove - Time.time));
                            IdleRotate(lookAngle, 2);
                        }
                        else if (nextMove - Time.time < 3)
                        {
                            Debug.Log("time is long " + (nextMove - Time.time));
                            IdleRotate(lookAngle, 1);
                        }
                        Debug.Log("idling");
                        
                    }
                }
                

                
            }
            
            }
        


    }
    private void RotateToAngle(float angle)
    {
        Debug.Log("rotating");
        float rotationSpeed = 5f;
        Debug.Log("angle to rotate to: " + angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.deltaTime);
    }
    private int NextNode
    {
        get
        {
            if (currentNode < nodes.Count - 1)
            {
                return currentNode + 1;
            }
            else { return 0; }
        }
    }
    private void Chase(Vector3 targetPosition)
    {
        Vector3 position = transform.position;
        float targetZ = Mathf.Round(targetPosition.z * 100f) / 100f;
        float targetX = Mathf.Round(targetPosition.x * 100f) / 100f;
        float z = Mathf.Round(position.z * 100f) / 100f;
        float x = Mathf.Round(position.x * 100f) / 100f;

        if (x < targetX)
        {

            Debug.Log(x);
            Debug.Log(targetX);
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if (x > targetX)
        {
            Debug.Log(x);
            Debug.Log(targetX);
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        else if (z < targetZ)
        {
            Debug.Log(z);
            Debug.Log(targetZ);
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (z > targetZ)
        {
            Debug.Log(z);
            Debug.Log(targetZ);
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }

    
                       
    }
    private void RotateToFace(Vector3 position)
    {
        float rotationSpeed = 45f;
        Vector3 direction = position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
    private void IdleRotate(float angle, int state) //rotate when idle to look 10 degrees to the left and the right quickly so that the player can't just hug a wall to avoid detection
    {
        float rotateAmount = 10f;
        float angle1 = angle + rotateAmount;
        float angle2 = angle - rotateAmount;
        if(state == 1)
        {
            RotateToAngle(angle1);
        }
        else if (state == 2)
        {
            RotateToAngle(angle2);
        }
        else if (state == 3)
        {
            RotateToAngle(angle);
        }
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
