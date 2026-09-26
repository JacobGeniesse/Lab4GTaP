using UnityEngine;

public class MeteorDamage : Damage
{
    [SerializeField] float damageAmount; //Amount of damage to deal
    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player" ) //If hitting a player
        {
            DoDamage(whatIHit.gameObject, damageAmount); //deal damage to the player
        }
    }

}
