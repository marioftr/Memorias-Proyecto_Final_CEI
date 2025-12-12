using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargaDardos : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Image _IMGCargaVertical;
    [SerializeField] private Image _IMGCargaHorizontalIzquierda;
    [SerializeField] private Image _IMGCargaHorizontalDerecha;
    public TMP_Text TextoCarga;

    [SerializeField] private float _Carga;

    [Header("Carga Horizontal")]
    public float CargaHorizontal = 0f;
    public bool CargaHorizontalActiva = false;
    public bool DebeMoverseHorizontal = false;
    [SerializeField] private bool _CargaHorizontalSubiendo = true;
    [SerializeField] private float _VelocidadCargaHorizontal = 100f;

    [Header("Carga Vertical")]
    public float CargaVertical = 0f;
    public bool CargaVerticalActiva = false;
    public bool DebeMoverseVertical = false;
    [SerializeField] private bool _CargaVerticalSubiendo = true;
    [SerializeField] private float _VelocidadCargaVertical = 100f;

    private void Start()
    {
        _Carga = 0f;
        _IMGCargaVertical.fillAmount = 0f;
        _IMGCargaHorizontalIzquierda.fillAmount = 0.48f;
        _IMGCargaHorizontalIzquierda.fillAmount = 0.48f;
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
    public void InicioCargaHorizontal()
    {
        if (!CargaHorizontalActiva) return;
        DebeMoverseHorizontal = true;
        _Carga = 50f;
        _IMGCargaHorizontalIzquierda.fillAmount = (_Carga - 2f) / 100f;
        _IMGCargaHorizontalDerecha.fillAmount = (98f - _Carga) / 100f;
    }
    public void FinCargaHorizontal()
    {
        if (!CargaHorizontalActiva) return;
        DebeMoverseHorizontal = false;
        TextoCarga.text = "";
        _CargaHorizontalSubiendo = true;
        GuardarCargaHorizontal(_Carga);
        CargaHorizontalActiva = false;
    }
    private void GuardarCargaHorizontal(float cargaHorizontal)
    {
        CargaHorizontal = cargaHorizontal;
        print(CargaHorizontal.ToString("F0"));
        _Carga = 0;
    }

    // CARGA VERTICAL
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
    public void InicioCargaVertical()
    {
        if (!CargaVerticalActiva) return;
        DebeMoverseVertical = true;
        _Carga = 0;
    }
    public void FinCargaVertical()
    {
        if (!CargaVerticalActiva) return;
        DebeMoverseVertical = false;
        TextoCarga.text = "";
        _CargaVerticalSubiendo = true;
        GuardarCargaVertical(_Carga);
        CargaVerticalActiva = false;
    }
    private void GuardarCargaVertical(float cargaVertical)
    {
        CargaVertical = cargaVertical;
        print(CargaVertical.ToString("F0"));
        _Carga = 0;
    }
}
   