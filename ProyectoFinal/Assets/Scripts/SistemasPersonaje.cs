using UnityEngine;

public class SistemasPersonaje : MonoBehaviour
{
    internal ControlesPersonaje _Controles;

    private void Awake()
    {
        TryGetComponent(out _Controles);
    }
}