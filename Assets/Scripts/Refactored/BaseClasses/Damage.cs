using UnityEngine;

public class Damage : MonoBehaviour
{
    public virtual void DoDamage(GameObject hitObject, float damageAmount)
    {
        if (hitObject.TryGetComponent<Health>(out Health health))
        {
            health.RemoveHealth(damageAmount);
        }
        else
        {
            Debug.LogWarning("Unable to find health component on this object!");
        }
    }
}
