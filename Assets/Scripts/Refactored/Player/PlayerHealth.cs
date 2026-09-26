using UnityEngine;

public class PlayerHealth : Health
{
    private StateManager gameManager; //Ref to statemanager
    [SerializeField] private float currentHealth = 1; //current player health

    void Start()
    {
        //Find the state manager
        gameManager = GameObject.Find("GameManager").GetComponent<StateManager>();
    }

    public override void RemoveHealth(float damage)
    {
        //Subtract damage dealt from current health
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            //If health is less than or equal to 0 destroy the player and give a game over
            gameManager.gameOver = true;
            Destroy(this.gameObject);
        }
    }
}
