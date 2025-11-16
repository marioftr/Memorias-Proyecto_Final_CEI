using UnityEngine;

public class SistemasPersonaje : MonoBehaviour
{
    internal ControlesPersonaje Controles;
    internal MovimientoPersonaje Movimiento;

    private void Awake()
    {
        TryGetComponent(out Controles);
        TryGetComponent(out Movimiento);
    }
}