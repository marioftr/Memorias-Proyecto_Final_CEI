using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
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

    private EstadoJuegoDiana _EstadoActual = 0;
    private EstadoJuegoDiana _EstadoAnterior = 0;

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
        //CambiarEstado();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SiguienteEstado();
        }
    }

    // ESTADOS DEL JUEGO
    private void SiguienteEstado()
    {
        if (_EstadoActual < EstadoJuegoDiana.Final)
        {
            _EstadoAnterior = _EstadoActual;
            _EstadoActual = (EstadoJuegoDiana)((int)_EstadoActual + 1);
            CambiarEstado();
        }
    }
    private void CambiarEstado()
    {
        if (_EstadoActual == _EstadoAnterior)
        {
            return;
        }
        print($"Cambiando estado de {_EstadoAnterior} a {_EstadoActual}");
        switch (_EstadoActual)
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
    private void AlEntrarTutorial()
    {
        PrintAlEntrar(_EstadoActual);
        _PanelTutorial.SetActive(true);
        _TiradaActual = 0;
        _TextoTirada.text = "";
    }
    private void AlEntrarCargaHorizontal()
    {
        PrintAlEntrar(_EstadoActual);
        _PanelTutorial.SetActive(false);
        _CargaDardos.CargaHorizontalActiva = true;
        _CargaDardos.TextoCarga.text = "Pulsa";
        _CargaDardos.InicioCargaHorizontal();
    }
    private void AlEntrarCargaVertical()
    {
        PrintAlEntrar(_EstadoActual);
        _CargaDardos.CargaVerticalActiva = true;
        _CargaDardos.TextoCarga.text = "Mantén";
    }
    private void AlEntrarAnimacionDardo()
    {
        PrintAlEntrar(_EstadoActual);
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
        PrintAlEntrar(_EstadoActual);
        print("Minijuego de dardos terminado!");
        // Mostrar resultado y cambiar de escena
    }
    private void PrintAlEntrar(EstadoJuegoDiana estado)
    {
        print($"Estado actual: {estado}");
    }
}