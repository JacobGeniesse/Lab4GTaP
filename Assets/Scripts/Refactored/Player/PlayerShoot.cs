using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab; //Prefab for laser, refactored to be a private var editable from the inspector so that it is no longer editable by other scripts
    private bool canShoot = true; //Bool for if the ship can shoot

    [SerializeField] private Vector3 shootOffset; //Vector3 for the shootOffset, refactored to use a variable instead of a raw number so that it is no longer hard-coded

    [SerializeField] private float cooldownLength; //Var for the shot cooldown length, refactored to not be hard-coded

    private PlayerInput playerInput; //Script reference for the player's input script

    void Start()
    {
        playerInput = FindAnyObjectByType<PlayerInput>(); //Assigning the input script
    }

    void Update()
    {
        Shooting(); //Run the shooting func
    }

    void Shooting()
    {
        //if the player presses the shoot button and they can shoot run the shooting func
        if (playerInput.shoot.WasPressedThisFrame() && canShoot)
        {
            Instantiate(laserPrefab, transform.position + shootOffset, Quaternion.identity); //Instantiate a laser prefab
            canShoot = false; //Player can no longer shoot
            StartCoroutine("Cooldown"); //Start cooldown Coroutine
        }
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownLength); //Wait cooldown length
        canShoot = true; //Player can shoot again
    }

}
