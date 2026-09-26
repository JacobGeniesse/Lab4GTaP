using UnityEngine;

public class Damage : MonoBehaviour
{
    //Source func for doing damage
    public virtual void DoDamage(GameObject hitObject, float damageAmount)
    {
        //Try to get the health component from the hit object
        if (hitObject.TryGetComponent<Health>(out Health health))
        {
            health.RemoveHealth(damageAmount); //If a derivitive script is found do damage
        }
        else
        {
            Debug.LogWarning("Unable to find health component on this object!"); //If not log a warning
        }
    }
}
