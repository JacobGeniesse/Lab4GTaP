using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    [HideInInspector] public bool gameOver;

    [SerializeField] private InputActionAsset inputList;
    private InputAction reset;

    [SerializeField] private string sceneName;

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

        reset.performed += Reset;
    }

    private void OnDisable()
    {
        reset.performed -= Reset;
    }

    private void Reset(InputAction.CallbackContext context)
    {
        if(gameOver == true)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
