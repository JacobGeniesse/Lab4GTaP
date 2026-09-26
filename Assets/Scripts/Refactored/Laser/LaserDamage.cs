using UnityEngine;

public class LaserDamage : Damage
{
    [SerializeField] private float damageAmount; //Amount of damage to deal to the meteor

    //If entering a trigger's hitbox, deal damage
    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        //Check if the object is a meteor
        if (whatIHit.tag == "Meteor")
        {
            //Do the damage to that object inherited funcs from damage parent class
            DoDamage(whatIHit.gameObject, damageAmount);
            Destroy(this.gameObject); //After doing damage destroy this game object
        }
    }
}
