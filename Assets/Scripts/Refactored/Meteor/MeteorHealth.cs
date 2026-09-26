using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorHealth : Health
{
    private float currentHealth; //Meteor's current health
    [SerializeField] private float maxHealth = 0; //Max health the meteor can have

    [SerializeField] private bool bigMeteor = false; //Is this a big meteor?

    private SpawnManager spawnManager; //Ref to spawn manager
    private CamManager camManager; //Ref to camManager

    private void Start()
    {
        camManager = FindAnyObjectByType<CamManager>();//Set cam manager
        spawnManager = FindAnyObjectByType<SpawnManager>(); //set spawn manager
        currentHealth = maxHealth;//Set current health equal to max
    }

    public override void RemoveHealth(float damage)
    {
        currentHealth -= damage; //subtract the damage dealt from current health
        if (currentHealth <= 0)
        {
            if (!bigMeteor)
            {
                //if not a big meteor increase the meteor count
                spawnManager.IncremenetMeteor();
            }
            else
            {
                //If this is a big meteor, remove it from the list of active big meteors
                spawnManager.bigMeteors.Remove(this.gameObject);
                //Shake the camera
                camManager.ShakeCam();
            }
            //Finally destroy this meteor
            Destroy(this.gameObject);
        }
    }
}
