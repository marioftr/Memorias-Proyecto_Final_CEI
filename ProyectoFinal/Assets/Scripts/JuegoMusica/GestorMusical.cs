using System.Collections.Generic;
using UnityEngine;

public class GestorMusical : MonoBehaviour
{
    public List<NotaMusical> NotasMusicales = new List<NotaMusical>();

    public void PararOtrasNotas(NotaMusical notaActual)
    {
        for (int i = 0; i<NotasMusicales.Count; i++)
        {
            if (NotasMusicales[i] != notaActual && NotasMusicales[i].CorrutinaActual != null)
            {
                NotasMusicales[i].StopCoroutine(NotasMusicales[i].CorrutinaActual);
                NotasMusicales[i].AudioSource.Stop();
                NotasMusicales[i].CorrutinaActual = null;
            }
        }
    }
}
