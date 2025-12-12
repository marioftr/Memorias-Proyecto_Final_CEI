using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestorJuegoDiana: MonoBehaviour
{
    public enum EstadoJuegoDiana
    {
        Inicio,
        Tutorial,
        CargaHorizontal,
        CargaVertical,
        AnimacionDardo,
        Final
    }

    public EstadoJuegoDiana EstadoActual;
    
    [Header("Referencias")]
    [SerializeField] private CargaDardos _CargaDardos;
    [SerializeField] private GameObject _PanelTutorial;
    [SerializeField] private TMP_Text _TextoTirada;

    [Header("Tiradas")]
    [SerializeField] private int _TiradasMaximas = 3;
    [SerializeField] private int _TiradaActual;

    private void Start()
    {
        _PanelTutorial.SetActive(true);
        CambiarEstado(EstadoJuegoDiana.Inicio);
    }

    private void Update()
    {
        switch (EstadoActual)
        {
            case EstadoJuegoDiana.Inicio:
                UpdateInicio();
                break;
            case EstadoJuegoDiana.Tutorial:
                UpdateTutorial();
                break;
            case EstadoJuegoDiana.CargaHorizontal:
                UpdateCargaHorizontal();
                break;
            case EstadoJuegoDiana.CargaVertical:
                UpdateCargaVertical();
                break;
            case EstadoJuegoDiana.AnimacionDardo:
                UpdateAnimacionDardo();
                break;
            case EstadoJuegoDiana.Final:
                break;
        }
    }

    // ESTADOS DEL JUEGO
    private void CambiarEstado(EstadoJuegoDiana nuevoEstado)
    {
        if (nuevoEstado == EstadoActual)
        {
            return;
        }
        print($"Cambiando estado de {EstadoActual} a {nuevoEstado}");
        EstadoActual = nuevoEstado;
        switch (EstadoActual)
        {
            case EstadoJuegoDiana.Inicio:
                break;
            case EstadoJuegoDiana.Tutorial:
                AlEntrarTutorial();
                break;
            case EstadoJuegoDiana.CargaHorizontal:
                AlEntrarCargaHorizontal();
                break;
            case EstadoJuegoDiana.CargaVertical:
                AlEntrarCargaVertical();
                break;
            case EstadoJuegoDiana.AnimacionDardo:
                AlEntrarAnimacionDardo();
                break;
            case EstadoJuegoDiana.Final:
                FinalJuegoDardos();
                break;
        }
    }
    private void UpdateInicio()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CambiarEstado(EstadoJuegoDiana.Tutorial);
        }
    }
    private void AlEntrarTutorial()
    {
        PrintAlEntrar(EstadoActual);
        _PanelTutorial.SetActive(true);
        _TiradaActual = 0;
        _TextoTirada.text = "";
    }
    private void UpdateTutorial()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CambiarEstado(EstadoJuegoDiana.CargaHorizontal);
        }

        // Interacción tutorial
        // Iniciar tirada
    }
    private void AlEntrarCargaHorizontal()
    {
        PrintAlEntrar(EstadoActual);
        _PanelTutorial.SetActive(false);
        _CargaDardos.CargaHorizontalActiva = true;
        _CargaDardos.TextoCarga.text = "Pulsa";
        _CargaDardos.InicioCargaHorizontal();
    }
    private void UpdateCargaHorizontal()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CambiarEstado(EstadoJuegoDiana.CargaVertical);
        }
        // Cambiar funcionalidad del botón de CargaDardos
        // Leer y guardar valor horizontal
    }
    private void AlEntrarCargaVertical()
    {
        PrintAlEntrar(EstadoActual);
        _CargaDardos.CargaVerticalActiva = true;
        _CargaDardos.TextoCarga.text = "Mantén";
    }
    private void UpdateCargaVertical()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CambiarEstado(EstadoJuegoDiana.AnimacionDardo);
        }

        // Cambiar botón
        // Guardar valor vertical
        // Finalizar tirada

    }
    private void AlEntrarAnimacionDardo()
    {
        PrintAlEntrar(EstadoActual);
    }
    private void UpdateAnimacionDardo()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CambiarEstado(EstadoJuegoDiana.Final);
        }

        // Animación (temporizador opcional)
        // Siguiente tirada
    }

    // TIRADAS
    private void IniciarTirada()
    {
        // Comprobar tirada actual
        // Actualizar texto tirada
        // Cambiar estado
    }
    private void SiguienteTirada()
    {
        // Leer valores de tiradas
        // Calcular posición en la diana
        // Sumar puntuación
        // Lanzar animación
    }
    private void FinalizarTirada()
    {

    }

    private void FinalJuegoDardos()
    {
        PrintAlEntrar(EstadoActual);
        print("Minijuego de dardos terminado!");
        // Mostrar resultado y cambiar de escena
    }
    private void PrintAlEntrar(EstadoJuegoDiana estado)
    {
        print($"Estado actual: {estado}");
    }
}