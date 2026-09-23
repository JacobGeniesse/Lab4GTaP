using UnityEngine;

public class LaserDamage : Damage
{
    [SerializeField] private float damageAmount;
    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Meteor")
        {
            DoDamage(whatIHit.gameObject, damageAmount);
            Destroy(this.gameObject);
        }
    }
}
