using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHealth : Health
{
    private float currentHealth;
    [SerializeField] private float maxHealth = 0;

    [SerializeField] private bool bigMeteor = false;

    private SpawnManager spawnManager;
    private CamManager camManager;

    private void Start()
    {
        camManager = FindAnyObjectByType<CamManager>();
        spawnManager = FindAnyObjectByType<SpawnManager>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public override void RemoveHealth(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (!bigMeteor)
            {
                spawnManager.IncremenetMeteor();
            }
            else
            {
                spawnManager.bigMeteors.Remove(this.gameObject);
                camManager.ShakeCam();
            }
            Destroy(this.gameObject);

        }
    }
}
