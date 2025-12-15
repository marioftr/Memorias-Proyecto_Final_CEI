using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestorJuegoDiana : MonoBehaviour
{
    public enum EstadoJuegoDiana
    {
        Inicio = 0,
        Tutorial = 1,
        CargaHorizontal = 2,
        CargaVertical = 3,
        AnimacionDardo = 4,
        Final = 5
    }

    public EstadoJuegoDiana EstadoActual = 0;
    private EstadoJuegoDiana _EstadoAnterior = 0;

    [Header("Referencias")]
    [SerializeField] private CargaDardos _CargaDardos;
    [SerializeField] private Dardo _Dardo;
    [SerializeField] private GameObject _PanelInicio;
    [SerializeField] private GameObject _PanelTutorial;
    [SerializeField] private GameObject _PanelJuego;
    [SerializeField] private GameObject _PanelFinal;
    [SerializeField] private TMP_Text _TextoTirada;
    [SerializeField] private TMP_Text _TextoResultados;

    [Header("Tiradas")]
    public int TiradasMaximas = 3;
    public int TiradaActual;

    [Header("Dardos")]
    [SerializeField] private List<Dardo> _DardosCreados;
    [SerializeField] private int _DardoActual;

    private void Awake()
    {
        _CargaDardos = FindAnyObjectByType<CargaDardos>();
        _DardosCreados = new();
    }
    private void Start()
    {
        CambiarPaneles();
        CrearDardos();
        TiradaActual = 0;
        _TextoTirada.text = "";
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _EstadoAnterior = EstadoActual;
            StartCoroutine(SiguienteEstado());
        }
    }

    // MODIFICAR ESTADO DEL JUEGO
    public IEnumerator SiguienteEstado()
    {
        if (EstadoActual < EstadoJuegoDiana.Final)
        {
            yield return null; // Esperar 1 frame para que no se ejecuten varios estados a la vez
            EstadoActual = (EstadoJuegoDiana)((int)EstadoActual + 1); // Convertir enum a int, sumarle 1, volver a convertirlo a enum
            CambiarEstado(EstadoActual);
        }
    }
    private void CambiarEstado(EstadoJuegoDiana estado)
    {
        EstadoActual = estado;
        if (EstadoActual == _EstadoAnterior)
        {
            return;
        }
        switch (EstadoActual)
        {
            case EstadoJuegoDiana.Inicio:
                AlEntrar(EstadoActual);
                break;
            case EstadoJuegoDiana.Tutorial:
                AlEntrar(EstadoActual);
                AlEntrarTutorial();
                break;
            case EstadoJuegoDiana.CargaHorizontal:
                AlEntrar(EstadoActual);
                AlEntrarCargaHorizontal();
                break;
            case EstadoJuegoDiana.CargaVertical:
                AlEntrar(EstadoActual);
                AlEntrarCargaVertical();
                break;
            case EstadoJuegoDiana.AnimacionDardo:
                AlEntrar(EstadoActual);
                AlEntrarAnimacionDardo();
                break;
            case EstadoJuegoDiana.Final:
                AlEntrar(EstadoActual);
                FinalJuegoDardos();
                break;
        }
    }

    // ESTADOS DEL JUEGO
    private void AlEntrar(EstadoJuegoDiana estado)
    {
        print($"Anterior: {_EstadoAnterior}\nActual: {estado}");
        CambiarPaneles();
        _EstadoAnterior = EstadoActual;
    }
    private void AlEntrarTutorial()
    {
        _PanelInicio.SetActive(false);
        _PanelTutorial.SetActive(true);
        TiradaActual = 0;
        _TextoTirada.text = "";
    }
    private void AlEntrarCargaHorizontal()
    {
        _PanelInicio.SetActive(false);
        _PanelTutorial.SetActive(false);
        IniciarTirada();
        _CargaDardos.CargaHorizontalActiva = true;
        _CargaDardos.TextoCarga.text = "Pulsa";
        _CargaDardos.InicioCargaHorizontal();
    }
    private void AlEntrarCargaVertical()
    {
        _CargaDardos.CargaVerticalActiva = true;
        _CargaDardos.TextoCarga.text = "Mantén";
        // InicioCargaVertical() al mantener el botón
    }
    private void AlEntrarAnimacionDardo()
    {
        // Animación dardo
        FinalizarTirada();
    }

    // TIRADAS
    private void IniciarTirada()
    {
        TiradaActual++;
        _TextoTirada.text = $"Tiradas: {TiradaActual} / {TiradasMaximas}";
        CalcularDardoActual();
    }
    private void FinalizarTirada()
    {
        if (TiradaActual < TiradasMaximas)
        {
            SiguienteTirada();
        }
        else
        {
            StartCoroutine(SiguienteEstado()); // Pasar al último estado
        }
    }
    public void SiguienteTirada() // No borrar !! Accesible desde botón Jugar en Unity
    {
        CambiarEstado(EstadoJuegoDiana.CargaHorizontal); // Volver a CargaHorizontal para siguiente tirada
    }
    public void IniciarTutorial()
    {
        CambiarEstado(EstadoJuegoDiana.Tutorial);
    }
    private void CambiarPaneles()
    {
        if(EstadoActual == EstadoJuegoDiana.Inicio)
        {
            _PanelInicio.SetActive(true);
            _PanelTutorial.SetActive(false);
            _PanelJuego.SetActive(false);
            _PanelFinal.SetActive(false);
            return;
        }
        if(EstadoActual == EstadoJuegoDiana.Tutorial)
        {
            _PanelInicio.SetActive(false);
            _PanelTutorial.SetActive(true);
            _PanelJuego.SetActive(false);
            _PanelFinal.SetActive(false);
            return;
        }
        if(EstadoActual == EstadoJuegoDiana.Final)
        {
            _PanelInicio.SetActive(false);
            _PanelTutorial.SetActive(false);
            _PanelJuego.SetActive(false);
            _PanelFinal.SetActive(true);
            return;
        }
        else
        {
            _PanelInicio.SetActive(false);
            _PanelTutorial.SetActive(false);
            _PanelJuego.SetActive(true);
            _PanelFinal.SetActive(false);
            return;
        }
    }
    private void FinalJuegoDardos()
    {
        print("Minijuego de dardos terminado!");
        StringBuilder textoResultados = new StringBuilder();
        for (int i = 0; i < TiradasMaximas; i++)
        {
            print($"Resultados en tirada {i+1}:\n" +
                $"Horizontal: {_CargaDardos.CargaHorizontal[i].ToString("F0")} || Vertical: {_CargaDardos.CargaVertical[i].ToString("F0")}");
            textoResultados.AppendLine($"TIRADA {i+1}: Horizontal: {_CargaDardos.CargaHorizontal[i].ToString("F0")} || Vertical: {_CargaDardos.CargaVertical[i].ToString("F0")}");
        }
        _TextoResultados.text = textoResultados.ToString();
        // Cambiar de escena
    }

    // DARDOS
    private void CalcularDardoActual()
    {
        _DardoActual = TiradaActual;
    }
    private void CrearDardos()
    {
        for (int i = 0;i < TiradasMaximas; i++)
        {
            Dardo nuevoDardo = Instantiate(_Dardo);
            _DardosCreados.Add(nuevoDardo);
            _DardosCreados[i].ActivarDesactivarDardo(false);
        }
        DefinirDardos();
    }
    private void DefinirDardos()
    {
        if(_DardosCreados.Count == 0)
        {
            print("No hay dardos creados");
            return;
        }
        for (int i = 0; i<_DardosCreados.Count ; i++)
        {
            _DardosCreados[i].DefinirDardo(i);
        }
    }

}