using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargaDardos : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GestorJuegoDiana _GestorJuegoDiana;
    [SerializeField] private Image _IMGCargaVertical;
    [SerializeField] private Image _IMGCargaHorizontalIzquierda;
    [SerializeField] private Image _IMGCargaHorizontalDerecha;
    public TMP_Text TextoCarga;

    [SerializeField] private float _Carga;

    [Header("Carga Horizontal")]
    public float[] CargaHorizontal;
    public bool CargaHorizontalActiva = false;
    public bool DebeMoverseHorizontal = false;
    [SerializeField] private bool _CargaHorizontalSubiendo = true;
    [SerializeField] private float _VelocidadCargaHorizontal = 100f;

    [Header("Carga Vertical")]
    public float[] CargaVertical;
    public bool CargaVerticalActiva = false;
    public bool DebeMoverseVertical = false;
    [SerializeField] private bool _CargaVerticalSubiendo = true;
    [SerializeField] private float _VelocidadCargaVertical = 100f;

    private void Awake()
    {
        ReiniciarCarga();
        CargaHorizontal = new float[_GestorJuegoDiana.TiradasMaximas];
        CargaVertical = new float[_GestorJuegoDiana.TiradasMaximas];
    }
    private void Update()
    {
        if (DebeMoverseHorizontal)
        {
            MovimientoCargaHorizontal();
        }
        if (DebeMoverseVertical)
        {
            MovimientoCargaVertical();
        }
    }

    // CARGA HORIZONTAL
    public void InicioCargaHorizontal()
    {
        if (!CargaHorizontalActiva) return;
        DebeMoverseHorizontal = true;
        _Carga = 50f;
        _IMGCargaHorizontalIzquierda.fillAmount = (_Carga - 2f) / 100f;
        _IMGCargaHorizontalDerecha.fillAmount = (98f - _Carga) / 100f;
    }
    public void MovimientoCargaHorizontal()
    {
        if (!CargaHorizontalActiva) return;
        if (_CargaHorizontalSubiendo)
        {
            _Carga += Time.deltaTime * _VelocidadCargaHorizontal;
            if (_Carga > 100)
            {
                _CargaHorizontalSubiendo = false;
                _Carga = 100;
            }
        }
        else
        {
            _Carga -= Time.deltaTime * _VelocidadCargaHorizontal;
            if (_Carga < 0)
            {
                _CargaHorizontalSubiendo = true;
                _Carga = 0;
            }
        }
        _IMGCargaHorizontalIzquierda.fillAmount = (_Carga - 2f) / 100f;
        _IMGCargaHorizontalDerecha.fillAmount = (98f - _Carga) / 100f;
    }
    public void FinCargaHorizontal()
    {
        if (_GestorJuegoDiana.EstadoActual != GestorJuegoDiana.EstadoJuegoDiana.CargaHorizontal) return;
        if (!CargaHorizontalActiva) return;
        DebeMoverseHorizontal = false;
        TextoCarga.text = "";
        _CargaHorizontalSubiendo = true;
        GuardarCargaHorizontal(_Carga);
        StartCoroutine(_GestorJuegoDiana.SiguienteEstado());
    }
    private void GuardarCargaHorizontal(float cargaHorizontal)
    {
        CargaHorizontal[_GestorJuegoDiana.TiradaActual-1] = cargaHorizontal;
        print(CargaHorizontal[_GestorJuegoDiana.TiradaActual-1].ToString("F0"));
        _Carga = 0;
        CargaHorizontalActiva = false;
    }

    // CARGA VERTICAL
    public void InicioCargaVertical()
    {
        if (_GestorJuegoDiana.EstadoActual != GestorJuegoDiana.EstadoJuegoDiana.CargaVertical) return;
        if (!CargaVerticalActiva) return;
        DebeMoverseVertical = true;
        _Carga = 0;
    }
    public void MovimientoCargaVertical()
    {
        if (!CargaVerticalActiva) return;
        if (_CargaVerticalSubiendo)
        {
            _Carga += Time.deltaTime * _VelocidadCargaVertical;

            if (_Carga > 100)
            {
                _CargaVerticalSubiendo = false;
                _Carga = 100;
            }
        }
        else
        {
            _Carga -= Time.deltaTime * _VelocidadCargaVertical;
            if (_Carga < 0)
            {
                _CargaVerticalSubiendo = true;
                _Carga = 0;
            }
        }
        _IMGCargaVertical.fillAmount = _Carga / 100;
    }
    public void FinCargaVertical()
    {
        if (_GestorJuegoDiana.EstadoActual != GestorJuegoDiana.EstadoJuegoDiana.CargaVertical) return;
        if (!CargaVerticalActiva) return;
        DebeMoverseVertical = false;
        TextoCarga.text = "";
        _CargaVerticalSubiendo = true;
        GuardarCargaVertical(_Carga);
        StartCoroutine(_GestorJuegoDiana.SiguienteEstado());
    }
    private void GuardarCargaVertical(float cargaVertical)
    {
        CargaVertical[_GestorJuegoDiana.TiradaActual-1] = cargaVertical;
        print(CargaVertical[_GestorJuegoDiana.TiradaActual-1].ToString("F0"));
        ReiniciarCarga();
    }

    private void ReiniciarCarga()
    {
        _Carga = 0f;

        _IMGCargaHorizontalIzquierda.fillAmount = 0.48f;
        _IMGCargaHorizontalDerecha.fillAmount = 0.48f;
        CargaHorizontalActiva = false;
        DebeMoverseHorizontal = false;

        _IMGCargaVertical.fillAmount = 0f;
        CargaVerticalActiva = false;
        DebeMoverseVertical = false;
    }
}
   