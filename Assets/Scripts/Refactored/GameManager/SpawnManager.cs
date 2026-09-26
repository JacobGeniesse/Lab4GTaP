using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [Header("Player Spawning Variables")]
    [SerializeField] private GameObject playerPrefab; //Prefab for spawning in the player
    [SerializeField] private Vector3 playerSpawn = Vector3.zero; //Var for where the player should spawn

    [Header("Meteor Spawning Variables")]
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private GameObject bigMeteorPrefab;
    private int meteorCount = 0;
    [SerializeField] private int bigMeteorInterval = 5;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float intervalDelay = 2f;

     public List<GameObject> bigMeteors = new List<GameObject>();

    [Header("Screen Border Variables")]
    [SerializeField] private Vector3 rightUpBorder = new Vector3(8, 7.5f, 0);
    [SerializeField] private Vector3 leftUpBorder = new Vector3(-8, 7.5f, 0);

    //Invoking Vars
    private StateManager stateManager;
    private CamManager camManager;

    void Start()
    {
        stateManager = GameObject.FindAnyObjectByType<StateManager>();
        camManager = GameObject.FindAnyObjectByType<CamManager>();
        GameObject newPlayer = Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
        if(camManager != null)
        {
            camManager.cineCam.Follow = newPlayer.transform;
        }
        else
        {
            Debug.LogError("camManager is not assigned!");
        }
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
            GameObject bigShot = Instantiate(bigMeteorPrefab, new Vector3(Random.Range(-8, 8), leftUpBorder.y, 0), Quaternion.identity);
            bigMeteors.Add(bigShot);
        }
        else
        {
            GameObject bigShot = Instantiate(bigMeteorPrefab, new Vector3(Random.Range(-8, 8), rightUpBorder.y, 0), Quaternion.identity);
            bigMeteors.Add(bigShot);
        }
    }

    public void IncremenetMeteor()
    {
        meteorCount++;
    }
}
