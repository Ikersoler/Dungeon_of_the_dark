using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public const int TRANSITION_VELOCITY = 10;

    public bool softTransition = false;
    public float transitionVelocity = 10f;
    public float rotationTransitionVelocity = 500f;
    public float raycastDistance = 1f; // Distancia del raycast para detectar obstáculos

    [SerializeField] private LayerMask obstacleLayer;
    Vector3 targetGridPos;
    Vector3 prevTargetGridPos;
    Vector3 targetRotation;

    private void Start()
    {
        targetGridPos = Vector3Int.RoundToInt(transform.position);
        transitionVelocity = TRANSITION_VELOCITY;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        
        prevTargetGridPos = targetGridPos;

        Vector3 targetPosition = targetGridPos;

        if (targetRotation.y > 270f && targetRotation.y < 361f) targetRotation.y = 0f;
        if (targetRotation.y < 0f) targetRotation.y = 270f;

        if (!softTransition)
        {
            transform.position = targetPosition;
            transform.rotation = Quaternion.Euler(targetRotation);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * transitionVelocity);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * rotationTransitionVelocity);
        }
        
       
    }

    
    public void RotateLeft() { if (AtRest) targetRotation -= Vector3.up * 90f; }
    public void RotateRight() { if (AtRest) targetRotation += Vector3.up * 90f; }

    public void MoveForward()
    {
        if (AtRest && !ObstacleInDirection(transform.forward))
        {
            targetGridPos += transform.forward;
        }
        else
        {
            Debug.Log("obsataculo");
        }
    }

    public void MoveBack()
    {
        if (AtRest && !ObstacleInDirection(-transform.forward))
        {
            targetGridPos -= transform.forward;
        }
    }

    public void MoveLeft()
    {
        if (AtRest && !ObstacleInDirection(-transform.right))
        {
            targetGridPos -= transform.right;
        }
    }

    public void MoveRight()
    {
        if (AtRest && !ObstacleInDirection(transform.right))
        {
            targetGridPos += transform.right;
        }
    }

    bool ObstacleInDirection(Vector3 direction)
    {
        RaycastHit hit;
        bool obstacle = Physics.Raycast(transform.position, direction,out hit, raycastDistance, obstacleLayer);
        if (!obstacle)
        {
            return false;
        }

        GameObject obstacleGO = hit.collider.gameObject;

        if(obstacleGO.CompareTag("Puerta"))
        {
            Debug.Log("puerta");
            DoorController doorController = obstacleGO.GetComponent<DoorController>();
            doorController.TryOpenDoor();


        }

        return true;

    }

    bool AtRest
    {
        get
        {
            return (Vector3.Distance(transform.position, targetGridPos) < 0.05f) &&
                   (Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f);
        }
    }



}
