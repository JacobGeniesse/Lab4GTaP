using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHealth : Health
{
    private float currentHealth;
    [SerializeField] private float maxHealth = 0;

    [SerializeField] private bool incrementCount = false;

    private SpawnManager spawnManager;

    private void Start()
    {
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
            if (incrementCount)
            {
                Destroy(this.gameObject);
            }
            spawnManager.IncremenetMeteor();
        }
    }
}
