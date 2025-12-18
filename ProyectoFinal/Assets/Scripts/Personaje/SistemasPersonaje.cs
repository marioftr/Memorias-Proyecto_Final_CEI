using UnityEngine;

public class SistemasPersonaje : MonoBehaviour
{
    internal ControlesPersonaje Controles;
    internal MovimientoPersonaje Movimiento;
    internal CamaraPersonaje Camara;
    private void Awake()
    {
        TryGetComponent(out Controles);
        TryGetComponent(out Movimiento);
        TryGetComponent(out Camara);
    }
}