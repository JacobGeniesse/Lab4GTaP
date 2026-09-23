using UnityEngine;

public class Health : MonoBehaviour
{
    public virtual void RemoveHealth(float damage)
    {
        Debug.Log($"Took {damage}");
    }
}
