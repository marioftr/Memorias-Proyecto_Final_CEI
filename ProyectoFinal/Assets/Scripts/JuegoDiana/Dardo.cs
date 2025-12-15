using UnityEngine;

public class Dardo : MonoBehaviour
{
    [Header("Referencias")]
    private GestorJuegoDiana _GestorJuegoDiana;
    private CargaDardos _CargaDardos;
    private Transform _Transform;
    public int Numero;
    private bool _Activo;
    private bool _ActivoPrevio = false;

    [SerializeField] private Vector3 _PosicionInicial = new Vector3(-45, -18, 200);
    [SerializeField] private Vector3 _RotacionInicial = new Vector3(180, -15, 0);

    [Header("Offset")]
    [SerializeField] private float _OffsetRealH;
    [SerializeField] private float _OffsetRealV;
    [SerializeField] private float _OffsetSuaveH;
    [SerializeField] private float _OffsetSuaveV;
    [SerializeField] private float _OffsetMaximoH = 60f;
    [SerializeField] private float _OffsetMaximoV = 100f;
    [SerializeField] private float _VelocidadSuavizado;

    private void Awake()
    {
        _GestorJuegoDiana = FindAnyObjectByType<GestorJuegoDiana>();
        _CargaDardos = FindAnyObjectByType<CargaDardos>();
        _Transform = transform;
    }
    private void Start()
    {
        _Transform.position = _PosicionInicial;
        _Transform.eulerAngles = _RotacionInicial;
        _VelocidadSuavizado = 5f;
    }
    private void Update()
    {
        _Activo = (Numero == _GestorJuegoDiana.TiradaActual); // Comprueba si el número del dardo coincide con el número de la tirada
        if (_Activo != _ActivoPrevio)
        {
            ActivarDesactivarDardo(_Activo); // Cambia su estado
            _ActivoPrevio = _Activo; // Guarda su estado para no volver a cambiarlo
            if (_Activo)
            {
                _OffsetSuaveH = 0f; // Reinicia el suavizado si el dardo se acaba de activar
                _OffsetSuaveV = -1f;
                print($"Movimiento {Numero}");
            }
        }
        if (!_Activo) return;
        MovimientoDardo();
    }
    private void MovimientoDardo()
    {
        if (_GestorJuegoDiana.EstadoActual == GestorJuegoDiana.EstadoJuegoDiana.CargaHorizontal)
        {
            _OffsetRealH = (_CargaDardos.Carga - 50f) / 50f; // -1 (-50)... 0 ... +1 (+50)
            _OffsetSuaveH = Mathf.Lerp(_OffsetSuaveH, _OffsetRealH, _VelocidadSuavizado * Time.deltaTime); // Dardo sigue el nivel de Carga
            _Transform.position = _PosicionInicial + new Vector3(_OffsetSuaveH * _OffsetMaximoH, 0, 0);
            return;
        }
        if (_GestorJuegoDiana.EstadoActual == GestorJuegoDiana.EstadoJuegoDiana.CargaVertical)
        {
            _OffsetRealV = (_CargaDardos.Carga / 50f); // 0 (0) ... +1 (+50) ... +2 (+100)
            _OffsetSuaveV = Mathf.Lerp(_OffsetSuaveV, _OffsetRealV, _VelocidadSuavizado * Time.deltaTime);
            _Transform.position = _PosicionInicial + new Vector3(_OffsetSuaveH*_OffsetMaximoH, _OffsetSuaveV * _OffsetMaximoV, 0);
            return;
        }
    }
    public void DefinirDardo(int numero)
    {
        Numero = numero + 1;
    }
    public void ActivarDesactivarDardo(bool opcion)
    {
        GestorJuego.ActivarDesactivarObjeto(gameObject, opcion);
    }
}
