using System;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    [SerializeField] private float meteorSpeed = 1; //Speed the meteor moves
    [SerializeField] private bool bigMeteor; //Is this a big meteor?
    private SpawnManager spawnManager; //ref to spawn manager

    [SerializeField] private float lowerY = -11;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnManager = FindAnyObjectByType<SpawnManager>(); //set spawn manager
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * meteorSpeed); //Move the meteor down

        //If less than a certain pos, destroy meteor
        if (transform.position.y < lowerY)
        {
            //If the meteor is a big meteor remove the meteor from the meteors list
            if (bigMeteor)
            {
                spawnManager.bigMeteors.Remove(this.gameObject);
            }
            //Destroy the object
            Destroy(this.gameObject);
        }
    }
}
