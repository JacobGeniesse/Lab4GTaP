using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Player Spawning Variables")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector3 playerSpawn = Vector3.zero;

    [Header("Meteor Spawning Variables")]
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private GameObject bigMeteorPrefab;
    private int meteorCount = 0;
    [SerializeField] private int bigMeteorInterval = 5;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float intervalDelay = 2f;

    [Header("Screen Border Variables")]
    [SerializeField] private Vector3 rightUpBorder = new Vector3(8, 7.5f, 0);
    [SerializeField] private Vector3 leftUpBorder = new Vector3(-8, 7.5f, 0);

    //Invoking Vars
    private StateManager stateManager;

    void Start()
    {
        stateManager = GameObject.FindAnyObjectByType<StateManager>();
        Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
        InvokeRepeating("SpawnMeteor", spawnDelay, intervalDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if(stateManager.gameOver == true)
        {
            CancelInvoke();
        }
        if(meteorCount > bigMeteorInterval)
        {
            BigMeteor();
        }
    }

    void SpawnMeteor()
    {
        if(leftUpBorder.y > rightUpBorder.y)
        {
            Instantiate(meteorPrefab, new Vector3(Random.Range(leftUpBorder.x, rightUpBorder.x), leftUpBorder.y, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(meteorPrefab, new Vector3(Random.Range(leftUpBorder.x, rightUpBorder.x), rightUpBorder.y, 0), Quaternion.identity);
        }
    }

    void BigMeteor()
    {
        meteorCount = 0;
        if (leftUpBorder.y > rightUpBorder.y)
        {
            Instantiate(bigMeteorPrefab, new Vector3(Random.Range(-8, 8), leftUpBorder.y, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(bigMeteorPrefab, new Vector3(Random.Range(-8, 8), rightUpBorder.y, 0), Quaternion.identity);
        }
    }

    public void IncremenetMeteor()
    {
        meteorCount++;
    }
}
