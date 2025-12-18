using UnityEngine;

public class CamaraPersonaje : MonoBehaviour
{
    [SerializeField] public Transform TransformCamara;
    [SerializeField] private float _Sensibilidad = 0.1f;
    [SerializeField] private float _LimiteCamara = 90;
    public Vector2 EjesRaton;
    public Vector2 RotacionCamara;
    private Transform _Transform;

    private void Awake()
    {
        _Transform = transform;
    }
    private void Start()
    {
        LimitarRaton();
    }
    private void Update()
    {
        MovimientoCamara();
    }
    private void LimitarRaton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void MovimientoCamara()
    {
        if (TransformCamara == null)
        {
            Debug.LogError("No tengo asignada una cámara en Unity.");
            return;
        }
        // Girar el personaje en su eje Y usando el EjesRaton.x
        RotacionCamara.y = EjesRaton.x * _Sensibilidad;
        _Transform.localEulerAngles += new Vector3(0, RotacionCamara.y, 0);

        // Girar la cámara en su eje X usando el EjesRaton.y
        RotacionCamara.x += EjesRaton.y * _Sensibilidad;
        // Clamp limita un valor entre dos valores
        RotacionCamara.x = Mathf.Clamp(RotacionCamara.x, -_LimiteCamara, _LimiteCamara);

        TransformCamara.localEulerAngles = new Vector3(-RotacionCamara.x, 0, 0);
    }
}