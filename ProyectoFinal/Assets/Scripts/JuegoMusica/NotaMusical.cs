using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class NotaMusical : MonoBehaviour
{
    private GestorMusical _GestorMusical;

    [Header("Audio")]
    public AudioSource AudioSource;
    [SerializeField] private AudioClip _Nota;
    [SerializeField, Range(0f, 1f)] private float _InicioNormalizado = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _FinalNormalizado = 0.6f;
    [SerializeField, Range(0.01f, 0.3f)] private float _FadeMaximo = 0.05f;

    public Coroutine CorrutinaActual;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.playOnAwake = false;
        AudioSource.clip = _Nota;
        _GestorMusical = FindAnyObjectByType<GestorMusical>();
        _GestorMusical.NotasMusicales.Add(this);
    }

    public void ReproducirNota()
    {
        float inicio = _Nota.length * _InicioNormalizado;
        float duracion = _Nota.length * (_FinalNormalizado - _InicioNormalizado);

        _GestorMusical.PararOtrasNotas(this);
        if (CorrutinaActual != null)
        {
            StopCoroutine( CorrutinaActual );
            AudioSource.Stop();
        }

        AudioSource.time = inicio;
        AudioSource.loop = false;

        CorrutinaActual = StartCoroutine(FadeInOut(duracion));
    }
    private IEnumerator FadeInOut(float duracion)
    {
        AudioSource.volume = 0f;
        AudioSource.Play();

        // Fade in
        float temporizador = 0f;
        while (temporizador < _FadeMaximo)
        {
            temporizador += Time.deltaTime;
            AudioSource.volume = Mathf.Lerp(0f, 1f, temporizador / _FadeMaximo);
            yield return null;
        }
        AudioSource.volume = 1f;

        yield return new WaitForSeconds(duracion - 2 *  _FadeMaximo);

        //Fade out
        temporizador = 0f;
        while (temporizador < _FadeMaximo)
        {
            temporizador += Time.deltaTime;
            AudioSource.volume = Mathf.Lerp(1f,0f, temporizador / _FadeMaximo);
            yield return null;
        }
        AudioSource.Stop();
        AudioSource.volume = 1f;
        CorrutinaActual = null;
    }
}
