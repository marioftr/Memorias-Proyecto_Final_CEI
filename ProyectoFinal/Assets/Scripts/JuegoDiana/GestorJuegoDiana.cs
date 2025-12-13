using System.Collections;
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
    [SerializeField] private GameObject _PanelTutorial;
    [SerializeField] private TMP_Text _TextoTirada;

    [Header("Tiradas")]
    public int TiradasMaximas = 3;
    public int TiradaActual;

    private void Start()
    {
        _PanelTutorial.SetActive(true);
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
            yield return null;
            EstadoActual = (EstadoJuegoDiana)((int)EstadoActual + 1);
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
        print($"Cambiando estado de {_EstadoAnterior} a {EstadoActual}");
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
    private void AlEntrarTutorial()
    {
        _PanelTutorial.SetActive(true);
        TiradaActual = 0;
        _TextoTirada.text = "";
    }
    private void AlEntrarCargaHorizontal()
    {
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
        _TextoTirada.text = $"{TiradaActual} / {TiradasMaximas}";
    }
    private void FinalizarTirada()
    {
        if (TiradaActual < TiradasMaximas)
        {
            _EstadoAnterior = EstadoActual;
            CambiarEstado(EstadoJuegoDiana.CargaHorizontal);
        }
        else
        {
            StartCoroutine(SiguienteEstado());
        }
    }
    private void SiguienteTirada()
    {
        // Leer valores de tiradas
        // Calcular posición en la diana
        // Sumar puntuación
        // Lanzar animación
    }

    private void FinalJuegoDardos()
    {
        print("Minijuego de dardos terminado!");
        for (int i = 0; i < TiradasMaximas; i++)
        {
            print($"Resultados en tirada {i+1}:\n" +
                $"Horizontal: {_CargaDardos.CargaHorizontal[i].ToString("F0")} || Vertical: {_CargaDardos.CargaVertical[i].ToString("F0")}");
        }
        // Cambiar de escena
    }
    private void AlEntrar(EstadoJuegoDiana estado)
    {
        print($"Anterior: {_EstadoAnterior}. Actual: {estado}");
        _EstadoAnterior = EstadoActual;
    }
}