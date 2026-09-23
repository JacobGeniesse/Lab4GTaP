using UnityEngine;

public class LaserMovement : MonoBehaviour
{

    [SerializeField] private float laserSpeed = 8f;
    [SerializeField] private float upperLimit = 11f;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * laserSpeed);

        if (transform.position.y > upperLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
