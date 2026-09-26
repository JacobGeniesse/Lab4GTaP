using UnityEngine;
using Unity.Cinemachine;
using System;
public class CamManager : MonoBehaviour
{
    public CinemachineCamera cineCam;
    [SerializeField] private CinemachineFollow cineFollow;
    [SerializeField] private CinemachineBasicMultiChannelPerlin cineShake;

    [SerializeField] private Vector3 standardZoom;
    [SerializeField] private Vector3 bigZoom;
    [SerializeField] private float zoomSpeed = 0.5f;

    private SpawnManager spawnManager;
    private bool activeShake;
    [SerializeField] private float shakeLength;
    private float currentShakeLength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            spawnManager = GetComponent<SpawnManager>();
        }
        catch
        {
            throw new ArgumentException("Unable to find SpawnManager component!", nameof(CamManager));
        }

        if(cineFollow == null)
        {
            Debug.LogError("Cinemachine follow var not assigned!");
        }

        if(cineShake == null)
        {
            Debug.LogError("Cinemachine Basic Multi Channel Perlin var not assigned!");
        }
        else
        {
            cineShake.AmplitudeGain = 0;
        }
    }

    void Update()
    {
        if(currentShakeLength > 0)
        {
            currentShakeLength -= Time.deltaTime;
        }
        else
        {
            if(activeShake == true)
            {
                cineShake.AmplitudeGain = 0;
                activeShake = false;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(spawnManager.bigMeteors.Count > 0)
        {
            ZoomOut();
        }
        else
        {
            ZoomIn();
        }
    }

    public void ZoomIn()
    {
        if(cineFollow.FollowOffset != standardZoom)
        {
            cineFollow.FollowOffset = Vector3.MoveTowards(cineFollow.FollowOffset, standardZoom, zoomSpeed * Time.deltaTime);
        }
    }

    public void ZoomOut()
    {
        if(cineFollow.FollowOffset != bigZoom)
        {
            cineFollow.FollowOffset = Vector3.MoveTowards(cineFollow.FollowOffset, bigZoom, zoomSpeed * Time.deltaTime);
        }
    }

    public void ShakeCam()
    {
        if(activeShake == false)
        {
            currentShakeLength = shakeLength;
            activeShake = true;
        }

        cineShake.AmplitudeGain = 1;
    }
}
