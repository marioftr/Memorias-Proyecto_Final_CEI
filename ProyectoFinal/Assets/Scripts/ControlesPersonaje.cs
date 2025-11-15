using UnityEngine;

public class ControlesPersonaje : MonoBehaviour
{
    private InputSystem_Actions _Controles;

    private void Awake()
    {
        _Controles = new();
    }
    private void OnEnable()
    {
        _Controles.Enable();
    }
    private void OnDisable()
    {
        _Controles.Disable();
    }
}
