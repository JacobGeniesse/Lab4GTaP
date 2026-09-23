using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    [SerializeField] private float meteorSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * meteorSpeed);

        if (transform.position.y < -11f)
        {
            Destroy(this.gameObject);
        }
    }
}
