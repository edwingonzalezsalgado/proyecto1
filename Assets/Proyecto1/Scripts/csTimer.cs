using System.Collections;
using UnityEngine;
using TMPro;
/// <summary>
/// @Autor:Edwin gonzalez salgado
/// viva UCAMP
/// Proyecto 1: Quiz
/// </summary>
public class csTimer : MonoBehaviour
{
    #region Atributos de la Clase publicos
    public TextMeshProUGUI lblTimer;    
    #endregion

    #region Eventos de la Clase    
    /// <summary>
    /// Evento unity update
    /// </summary>
    void Update()
    {
        StartCoroutine(ExampleCoroutine());
    }
    #endregion

    #region Metodos de la clase
    /// <summary>
    /// Metodo para iniciar el proceso del timer
    /// </summary>
    /// <returns></returns>
    IEnumerator ExampleCoroutine()
    {
        do
        {
            string tiempo = ((int)Time.timeSinceLevelLoad).ToString();
            lblTimer.text = "Tiempo transcurrido: " + tiempo;

            msj("1 time: " + tiempo);
            yield return new WaitForSeconds(1);            
        } while (true);
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
