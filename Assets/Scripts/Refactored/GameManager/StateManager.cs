using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    [HideInInspector] public bool gameOver;

    private PlayerInput playerInput;

    [SerializeField] private string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GameObject.FindAnyObjectByType<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Reset();
    }

    private void Reset()
    {
        if(playerInput.reset.WasPressedThisFrame() && gameOver == true)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
