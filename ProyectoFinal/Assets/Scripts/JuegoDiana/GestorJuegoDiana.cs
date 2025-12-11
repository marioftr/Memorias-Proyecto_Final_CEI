using TMPro;
using UnityEngine;

public class GestorJuegoDiana: MonoBehaviour
{
    private enum _EstadoJuegoDiana
    {
        Tutorial = 0,
        CargaHorizontal = 1,
        CargaVertical = 2,
        AnimacionDardo = 3,
        Resultado = 4,
        Final = 5
    }

    [Header("Referencias")]
    [SerializeField] private CargaDardos _CargaDardos;
    [SerializeField] private GameObject _PanelTutorial;
    [SerializeField] private TMP_Text _TextoTirada;
}