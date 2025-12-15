using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestorJuego : MonoBehaviour
{
    public static GameObject PanelPrincipal;
    public static GameObject PanelOpciones;

    [Header("Gestión Paneles")]
    [SerializeField] private GameObject _PanelPrincipal;
    [SerializeField] private GameObject _PanelOpciones;

    [Header("Gestión Opciones")]
    [SerializeField] private AudioMixer _AudioMixer;
    [SerializeField] private Toggle _TogglePantallaCompleta;
    [SerializeField] private Slider _SliderVolumen;
    // "const" para no modificar sin querer el nombre de la variable en otra línea
    private const string _ParametroVolumen = "Musica";

    private void Awake()
    {
        PanelPrincipal = _PanelPrincipal;
        PanelOpciones = _PanelOpciones;
    }
    public static void CargarEscena(int id)
    {
        SceneManager.LoadScene(id);
    }
    public static void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else

        Application.Quit();
#endif
    }

    private void Start()
    {
        MostrarMenuPrincipal();
    }
    public static void MostrarMenuPrincipal()
    {
        OcultarTodos();
        PanelPrincipal.SetActive(true);
    }
    public static void MostrarMenuOpciones()
    {
        OcultarTodos();
        PanelOpciones.SetActive(true);
    }
    public static void OcultarTodos()
    {
        PanelPrincipal.SetActive(false);
        PanelOpciones.SetActive(false);
    }

    private void OnEnable()
    {
        // Carga valores guardados
        // PlayerPrefs.GetFloat lee el valor guardado en los archivos del jugador (1. el valor guardado, 2. el valor por defecto si no hay otro)
        _SliderVolumen.value = PlayerPrefs.GetFloat(_ParametroVolumen, 0.75f);
        // isOn = Screen.fullScreen sirve para asignar el valor "true" al toggle si lee que el juego está en pantalla completa, y "false" si está en ventana
        _TogglePantallaCompleta.isOn = Screen.fullScreen;

        // Aplica los valores
        AplicarVolumen();
        AplicarPantallaCompleta();
    }
    public void AplicarVolumen()
    {
        float valor = _SliderVolumen.value; // Asigna el valor del Slider a una variable local

        // Asigna al AudioMixer el volumen con una fórmula logarítmica para que se sienta más natural
        /* _AudioMixer.SetFloat(_ParametroVolumen, Mathf.Log10(valor) * 20); */
        // Con el volumen a 0 el logaritmo devuelve infinito negativo, así que se asigna un valor por defecto para niveles muy bajos
        /*float db;
        if (valor > 0.0001f)
        {
            db = Mathf.Log10(valor) * 20;
        }
        else db = -80f;*/
        // Versión más corta:
        float db = valor > 0.0001f ? Mathf.Log10(valor) * 20 : -80f;

        _AudioMixer.SetFloat(_ParametroVolumen, db); // Ahora sí, asigna al AudioMixer el volumen
        PlayerPrefs.SetFloat(_ParametroVolumen, valor); // Guarda el valor en los archivos del jugador
    }
    public void AplicarPantallaCompleta()
    {
        bool nuevoEstado = _TogglePantallaCompleta.isOn; // Aplica la pantalla completa o ventana según esté seleccionado en el toggle
        Screen.fullScreen = nuevoEstado;
        Debug.Log("Pantalla completa: " + nuevoEstado); // Prueba para ver si funciona la pantalla completa en el editor
    }

    public static void ActivarDesactivarObjeto(GameObject objeto, bool opcion)
    {
        if (objeto.TryGetComponent(out Collider colision))
        {
            colision.enabled = opcion;
        }
        if(objeto.TryGetComponent(out Renderer renderer))
        {
            renderer.enabled = opcion;
        }
        if(objeto.TryGetComponent(out Light luz))
        {
            luz.enabled = opcion;
        }
        for (int i = 0; i < objeto.transform.childCount; i++)
        {
            ActivarDesactivarObjeto(objeto.transform.GetChild(i).gameObject, opcion);
        }
    }
}