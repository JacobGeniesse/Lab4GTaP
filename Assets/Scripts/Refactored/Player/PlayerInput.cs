using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputList;

    [HideInInspector, ReadOnly(true)] public InputAction reset;
    [HideInInspector, ReadOnly(true)] public InputAction move;
    [HideInInspector, ReadOnly(true)] public InputAction shoot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reset = inputList["Reset"];
        move = inputList["Move"];
        shoot = inputList["Shoot"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
