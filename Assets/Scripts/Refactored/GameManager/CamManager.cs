using UnityEngine;
using Unity.Cinemachine;
using System;
public class CamManager : MonoBehaviour
{
    [Header("Camera References")]
    public CinemachineCamera cineCam; //Reference to the cinemachine camera component
    [SerializeField] private CinemachineFollow cineFollow; //Reference to the cinemachine follow component
    [SerializeField] private CinemachineBasicMultiChannelPerlin cineShake; //Reference to the perlin component

    [Header("Camera Zoom Variables")]
    [SerializeField] private Vector3 standardZoom; //Cam offset for standard zoom
    [SerializeField] private Vector3 bigZoom; // Cam offset for when a big meteor is active
    [SerializeField] private float zoomSpeed = 0.5f; //Speed of transistion between zooms

    [Header("Camera Shake Variables")]
    [SerializeField] private float shakeLength; //How long should the camera shake for?
    private float currentShakeLength; //Current time left on the shake

    //Helper classes
    private SpawnManager spawnManager;

    void Start()
    {
        //Try catch for assigning spawnManager
        try
        {
            spawnManager = GetComponent<SpawnManager>();
        }
        catch
        {
            throw new ArgumentException("Unable to find SpawnManager component!", nameof(CamManager));
        }

        //Error check for cinemachine follow component
        if(cineFollow == null)
        {
            Debug.LogError("Cinemachine follow var not assigned!");
        }

        //Error check for perlin component
        if(cineShake == null)
        {
            Debug.LogError("Cinemachine Basic Multi Channel Perlin var not assigned!");
        }
        else
        {
            cineShake.AmplitudeGain = 0; //If the shake var exists set it to zero
        }
    }

    void Update()
    {
        if(currentShakeLength > 0)
        {
            //Decrease currentShakeLength if above zero
            currentShakeLength -= Time.deltaTime;
        }
        else
        {
            //Stop shaking the camera if at or below zero
            if(cineShake.AmplitudeGain != 0)
            {
                cineShake.AmplitudeGain = 0;
            }
        }
    }

    void LateUpdate()
    {
        //If there's a big meteor active zoom out, if not zoom in
        if(spawnManager.bigMeteors.Count > 0)
        {
            ZoomOut();
        }
        else
        {
            ZoomIn();
        }
    }

    //Helper func for zooming the camera in
    public void ZoomIn()
    {
        //If not already at desired value move towards it
        if(cineFollow.FollowOffset != standardZoom)
        {
            cineFollow.FollowOffset = Vector3.MoveTowards(cineFollow.FollowOffset, standardZoom, zoomSpeed * Time.deltaTime);
        }
    }

    //Helper func for zooming the camera out
    public void ZoomOut()
    {
        //If not already at desired value move towards it
        if (cineFollow.FollowOffset != bigZoom)
        {
            cineFollow.FollowOffset = Vector3.MoveTowards(cineFollow.FollowOffset, bigZoom, zoomSpeed * Time.deltaTime);
        }
    }

    //Helper func for starting to shake the camera
    public void ShakeCam()
    {
        currentShakeLength = shakeLength;
        cineShake.AmplitudeGain = 1;
    }
}
