using UnityEngine;

public class Health : MonoBehaviour
{
    //Base func for removing health, always meant to be overwritten
    public virtual void RemoveHealth(float damage)
    {
        Debug.Log($"Took {damage}");
    }
}
