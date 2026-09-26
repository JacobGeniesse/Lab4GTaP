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
    [SerializeField] private GameObject meteorPrefab; //Prefab for the normal meteor
    [SerializeField] private GameObject orbitalPrefab; //prefab for the orbital meteor
    [SerializeField] private GameObject bigMeteorPrefab; //prefab for the big meteor
    private int meteorCount = 0; //How many meteors destroyed
    [SerializeField] private int bigMeteorInterval = 5; //When should the big meteor spawn?
    [SerializeField] private float spawnDelay = 1f; //Start delay for spawning meteors
    [SerializeField] private float intervalDelay = 2f; //Delay between meteor spawns

    //List for storing active big meteors
    [HideInInspector] public List<GameObject> bigMeteors = new List<GameObject>(); 

    [Header("Screen Border Variables")]
    [SerializeField] private Vector3 rightUpBorder = new Vector3(8, 7.5f, 0); //Location of the top right limit of the screen
    [SerializeField] private Vector3 leftUpBorder = new Vector3(-8, 7.5f, 0); //Location of the top left limit of the screen

    //Invoking Vars
    private StateManager stateManager; //Ref to state manager
    private CamManager camManager; //Ref to cam manager

    void Start()
    {
        //Assing refs to other managers
        stateManager = GameObject.FindAnyObjectByType<StateManager>();
        camManager = GameObject.FindAnyObjectByType<CamManager>();

        GameObject newPlayer = Instantiate(playerPrefab, playerSpawn, Quaternion.identity); //Create player
        newPlayer.name = "Player"; //name player object
        if(camManager != null)
        {
            camManager.cineCam.Follow = newPlayer.transform; //Set the camera's follow target
        }
        else
        {
            Debug.LogError("camManager is not assigned!"); //Throw an error if this fails
        }
        InvokeRepeating("ChooseMeteor", spawnDelay, intervalDelay); //Spawn meteors
    }

    void Update()
    {
        //If a game over is achieved stop spawning meteors
        if(stateManager.gameOver == true)
        {
            CancelInvoke();
        }
        //If the meteor count is greater than the big meteor interval spawn in the big meteor
        if(meteorCount > bigMeteorInterval)
        {
            BigMeteor();
        }
    }

    //Func for chosing which meteor type to spawn
    void ChooseMeteor()
    {
        //Randomly roll which meteor type to spawn
        int meteorSpawn = Random.Range(0, 6);
        switch (meteorSpawn)
        {
            //If 0 spawn an orbital
            case 0:
                SpawnMeteor(orbitalPrefab);
                break;
            default:
                //Otherwise spawn a normal meteor
                SpawnMeteor(meteorPrefab);
                break;
        }
    }

    //Func for spawning meteors
    void SpawnMeteor(GameObject meteorChoice)
    {
        //Spawn the meteor of choice at a random spawn position
        if (leftUpBorder.y > rightUpBorder.y)
        {
            //If left border is higher up, go with its y value
            Instantiate(meteorChoice, new Vector3(Random.Range(leftUpBorder.x, rightUpBorder.x), leftUpBorder.y, 0), Quaternion.identity);
        }
        else
        {
            //otherwise base spawn point on right bordervalue
            Instantiate(meteorChoice, new Vector3(Random.Range(leftUpBorder.x, rightUpBorder.x), rightUpBorder.y, 0), Quaternion.identity);
        }
    }
    //Spawning function for adding a big meteor
    void BigMeteor()
    {
        meteorCount = 0; //Set meteor count equal to zero
        if (leftUpBorder.y > rightUpBorder.y)
        {
            //Spawn a big meteor at random position
            GameObject bigShot = Instantiate(bigMeteorPrefab, new Vector3(Random.Range(-8, 8), leftUpBorder.y, 0), Quaternion.identity);
            bigMeteors.Add(bigShot); //add that meteor to a list of active big meteors
        }
        else
        {
            //Same deal as the upper if, just using the right border
            GameObject bigShot = Instantiate(bigMeteorPrefab, new Vector3(Random.Range(-8, 8), rightUpBorder.y, 0), Quaternion.identity);
            bigMeteors.Add(bigShot);
        }
    }

    //Increase the meteor destroyed count
    public void IncremenetMeteor()
    {
        meteorCount++;
    }
}
