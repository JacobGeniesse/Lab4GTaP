using UnityEngine;

public class LaserMovement : MonoBehaviour
{

    [SerializeField] private float laserSpeed = 8f; //speed laser travels
    [SerializeField] private float upperLimit = 11f; //Destruction point for laser

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * laserSpeed); //move laser upwards

        if (transform.position.y > upperLimit)
        {
            Destroy(this.gameObject); //If hitting upper limit destroy the laser
        }
    }
}
