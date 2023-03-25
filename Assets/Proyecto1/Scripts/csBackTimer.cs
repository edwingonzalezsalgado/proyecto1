using UnityEngine;
using TMPro;
/// <summary>
/// @Autor:Edwin gonzalez salgado
/// viva UCAMP
/// Proyecto 1: Quiz
/// </summary>
public class csBackTimer : MonoBehaviour
{
    #region Atributos de la clase
    public TextMeshProUGUI lblBackTimer; 
    public float startTime;
    public csPreguntas objPreguntas;
    #endregion
    
    #region Eventos de la clase
    /// <summary>
    /// Evento unity start
    /// </summary>
    void Start()
    {
        lblBackTimer.text = startTime.ToString();
        startTime = Time.timeSinceLevelLoad + startTime;
    }
    /// <summary>
    /// Evento unity update
    /// </summary>
    void Update()
    {
        if(startTime > 0)
        {
            startTime -= Time.deltaTime;
            lblBackTimer.text = "Tiempo para responder: " + startTime.ToString("0.0");
        }
        else
        {
            StartCoroutine(objPreguntas.tiempoDeEspera(0.5f));
            startTime = 10;
            msj("se acabó el tiempo");
        }
    }
    #endregion

    #region Funciones de la Clase
    /// <summary>
    /// Funcion para mostrar mensajes en consola
    /// </summary>
    /// <param name="msj">mensaje a mostrar</param>
    private void msj(string msj)
    {
        //Debug.Log(msj);
    }
    #endregion
}
