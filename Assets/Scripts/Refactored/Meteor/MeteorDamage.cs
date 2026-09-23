using UnityEngine;

public class MeteorDamage : Damage
{
    [SerializeField] float damageAmount;
    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player" )
        {
            DoDamage(whatIHit.gameObject, damageAmount);
        }
    }

}
