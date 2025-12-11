using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargaDardos : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Image _IMGCargaVertical;
    [SerializeField] private Image _IMGCargaHorizontalIzquierda;
    [SerializeField] private Image _IMGCargaHorizontalDerecha;
    [SerializeField] private TMP_Text _TextoCarga;

    [Header("Carga Vertical")]
    [SerializeField] private bool _EsCargaVertical = false;
    [SerializeField] private bool _CargaVerticalSubiendo = true;
    [SerializeField] private float _CargaVertical = 0f;
    [SerializeField] private float _VelocidadCargaVertical = 100f;

    [Header("Carga Horizontal")]
    [SerializeField] private bool _EsCargaHorizontal = false;
    [SerializeField] private bool _CargaHorizontalSubiendo = true;
    [SerializeField] private float _CargaHorizontal = 50f;
    [SerializeField] private float _VelocidadCargaHorizontal = 100f;

    private void Start()
    {
        _CargaHorizontal = 50f;
        _IMGCargaHorizontalIzquierda.fillAmount = (_CargaHorizontal - 2f) / 100f;
        _IMGCargaHorizontalDerecha.fillAmount = (98f - _CargaHorizontal) / 100f;

        _EsCargaHorizontal = true;
    }
    private void Update()
    {
        if (_EsCargaVertical)
        {
            CargaVerticalActiva();
        }
        if (_EsCargaHorizontal)
        {
            CargaHorizontalActiva();
        }
    }

    // CARGA HORIZONTAL
    public void CargaHorizontalActiva()
    {
        if (_CargaHorizontalSubiendo)
        {
            _CargaHorizontal += Time.deltaTime * _VelocidadCargaHorizontal;
            if (_CargaHorizontal > 100)
            {
                _CargaHorizontalSubiendo = false;
                _CargaHorizontal = 100;
            }
        }
        else
        {
            _CargaHorizontal -= Time.deltaTime * _VelocidadCargaHorizontal;
            if (_CargaHorizontal < 0)
            {
                _CargaHorizontalSubiendo = true;
                _CargaHorizontal = 0;
            }
        }
        _IMGCargaHorizontalIzquierda.fillAmount = (_CargaHorizontal - 2f) / 100f;
        _IMGCargaHorizontalDerecha.fillAmount = (98f - _CargaHorizontal) / 100f;
    }
    public void InicioCargaHorizontal()
    {
        _EsCargaHorizontal = true;
        _CargaHorizontal = 50;
        //_TextoCarga.text = "";
    }
    public void FinCargaHorizontal()
    {
        _EsCargaHorizontal = false;
        //_TextoCarga.text = _CargaHorizontal.ToString("F0");
        _CargaHorizontalSubiendo = true;
    }

    // CARGA VERTICAL
    public void CargaVerticalActiva()
    {
        if (_CargaVerticalSubiendo)
        {
            _CargaVertical += Time.deltaTime * _VelocidadCargaVertical;

            if (_CargaVertical > 100)
            {
                _CargaVerticalSubiendo = false;
                _CargaVertical = 100;
            }
        }
        else
        {
            _CargaVertical -= Time.deltaTime * _VelocidadCargaVertical;
            if (_CargaVertical < 0)
            {
                _CargaVerticalSubiendo = true;
                _CargaVertical = 0;
            }
        }
        _IMGCargaVertical.fillAmount = _CargaVertical / 100;
    }
    public void InicioCargaVertical()
    {
        _EsCargaVertical = true;
        _CargaVertical = 0;
        _TextoCarga.text = "";
    }
    public void FinCargaVertical()
    {
        _EsCargaVertical = false;
        _TextoCarga.text = _CargaVertical.ToString("F0");
        _CargaVerticalSubiendo = true;
    }
}
   