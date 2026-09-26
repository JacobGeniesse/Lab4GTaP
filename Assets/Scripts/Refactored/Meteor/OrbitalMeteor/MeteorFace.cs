using UnityEngine;
using System;

public class MeteorFace : MonoBehaviour
{
    private Transform playerTrans; //player's transform
    private Vector3 target; //Target's transform



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTrans = GameObject.Find("Player").GetComponent<Transform>(); //Find playerTransform

        try
        {
            target = playerTrans.position; //Try to set target point for meteor
        }
        catch
        {
            //If this fails throw an error
            throw new ArgumentException("Unable to find player game object!", nameof(MeteorFace));
        }

        RotateMeteor(); //rotate the meteor
    }

    void Update()
    {
        RotateMeteor(); //rotate the meteor towards the target
    }

    void RotateMeteor()
    {
        //Delegate a var for transform.right for ease of use
        Vector3 right = transform.right;

        //Assign a value for the target's position
        Vector3 targetPos = target - transform.position;

        //Calculate the difference between the direction the meteor is facing and the normalized target position
        float currentDif = (Vector3.Dot(right.normalized, targetPos.normalized));

        //Transistion currentDif from Radians to Degrees for rotation use
        currentDif = currentDif * Mathf.Rad2Deg;

        transform.Rotate(0, 0, currentDif); //Rotate by the currentDif amount to have the ship lock onto target

        //Debug.Log(currentDif); //For testing if the currentDifference is being logged correctly
    }

}
