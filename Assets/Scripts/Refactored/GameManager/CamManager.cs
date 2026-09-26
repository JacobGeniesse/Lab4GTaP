using UnityEngine;
using Unity.Cinemachine;
using System;
public class CamManager : MonoBehaviour
{
    public CinemachineCamera cineCam;
    [SerializeField] private CinemachineFollow cineFollow;

    [SerializeField] private Vector3 standardZoom;
    [SerializeField] private Vector3 bigZoom;
    [SerializeField] private float zoomSpeed = 0.5f;

    private SpawnManager spawnManager;

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
}
