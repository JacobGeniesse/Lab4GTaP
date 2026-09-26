using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    [HideInInspector] public bool gameOver; //Is the game currently over?

    [SerializeField] private InputActionAsset inputList; //Input list ref
    private InputAction reset; //action ref for resetting the game

    [SerializeField] private string sceneName; //name of the scene to reload

    void OnEnable()
    {
        try
        {
            reset = inputList["Reset"];
        }
        catch
        {
            throw new ArgumentException("Unable to find inputList!", nameof(StateManager));
        }

        reset.performed += ResetLevel; //Add reset to the actions called by the reset delegate
    }

    private void OnDisable()
    {
        reset.performed -= ResetLevel; //Remove reset from the actions called by the reset delegate
    }

    private void ResetLevel(InputAction.CallbackContext context)
    {
        if(gameOver == true)
        {
            SceneManager.LoadScene(sceneName); //If in a game over reload the scene
        }
    }
}
