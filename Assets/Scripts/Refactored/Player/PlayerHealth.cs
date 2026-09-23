using UnityEngine;

public class PlayerHealth : Health
{
    private GameManager gameManager;
    private float currentHealth = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public override void RemoveHealth(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            gameManager.gameOver = true;
            Destroy(this.gameObject);
        }
    }
}
