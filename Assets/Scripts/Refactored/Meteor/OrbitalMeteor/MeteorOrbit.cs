using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System;

public class MeteorOrbit : MonoBehaviour
{
    private Transform playerTrans; //Var to reference the player pos
    private Vector3 targetPos; //var to reference the target the meteor will move towards

    private float currentRadius = 5f; //Var to reference the current radius of the meteor's orbit
    [SerializeField] private float approachSpeed = 0.5f; //How fast the meteor will approach the target
    private float rotationDirection; //Direction the meteor is roating towards
    private float currentAngle; //Current angle of the meteor
    
    void Start()
    {
        playerTrans = GameObject.Find("Player").GetComponent<Transform>(); //Obtain Player transform

        try
        {
            targetPos = playerTrans.position; //Set the target for the meteor
        }
        catch
        {
            //Throw an error if this fails
            targetPos = Vector3.zero;
            throw new ArgumentException("Unable to find player target!", nameof(MeteorOrbit));
        }
        //Set the inital angle and radius for the meteor
        currentRadius = Vector3.Distance(transform.position, targetPos);
        currentAngle = Vector3.SignedAngle(transform.position, targetPos, Vector3.right);

        //Set the orbiting direction for the meteor
        int direction = UnityEngine.Random.Range(0, 2);
        if(direction == 0)
        {
            rotationDirection = 1;
        }
        else
        {
            rotationDirection = -1;
        }

    }

    void Update()
    {
        MoveShip(); //Call func for ship rotation
        DecreaseRadius(); //Call func for decreasing the radius
    }

    //Helper func for moving the ship
    private void MoveShip()
    {
        //Adjust the current angle of the ship
        currentAngle += IncrementRot() * Time.deltaTime;

        //If currentAngle goes above or below the max or min value wrap around to the other value
        if (currentAngle >= 360)
        {
            currentAngle = 0;
        }
        else if (currentAngle <= 0)
        {
            currentAngle = 360;
        }

        //Determine the coords for the x and y axis
        float shipPosX = 0;
        float shipPosY = 0;

        if (playerTrans != null)
        {
            shipPosX = targetPos.x + (currentRadius * Mathf.Cos(currentAngle));
            shipPosY = targetPos.y + (currentRadius * Mathf.Sin(currentAngle));
        }

        //set the meteor's position
        Vector3 shipPos = new Vector3(shipPosX, shipPosY, 0);

        transform.position = shipPos; //Set the ship's new position
    }

    //Helper func for decreasing the meteor's radius from target position
    private void DecreaseRadius()
    {
        //If the ship's radius is greater than a certain amount decrease it
        if (currentRadius > 1)
        {
            currentRadius -= approachSpeed * Time.deltaTime;
        }
    }

    //Helper func for adjusting the rotation of the meteor
    float IncrementRot()
    {
        //Calc meteor offset
        Vector3 offset = Vector3.zero;
        if(playerTrans != null)
        {
            offset = targetPos - transform.position;
        }
        
        //calc distance from target
        float distance = offset.sqrMagnitude;

        //Return the speed and direction the meteor should travel
        return (distance * 0.05f) * rotationDirection;
    }
}
