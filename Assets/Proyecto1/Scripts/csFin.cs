using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
/// @Autor:Edwin gonzalez salgado
/// viva UCAMP
/// Proyecto 1: Quiz
/// </summary>
public class csFin : MonoBehaviour
{
    #region Atributos de la clase
    public TextMeshProUGUI lblPuntaje;
    public TextMeshProUGUI lblAviso;
    public TextMeshProUGUI lblmayorPuntaje;
    #endregion

    #region Eventos de la clase
    /// <summary>
    /// Evento unity start qu valida los puntajes a mostrar
    /// </summary>
    void Start()
    {
        int contadorPuntos = PlayerPrefs.GetInt("contadorPuntos");
        lblPuntaje.text = "Puntos: " + contadorPuntos.ToString();

        int isMayorPuntaje = PlayerPrefs.GetInt("isMayorPuntaje");
        int mayorPuntaje = PlayerPrefs.GetInt("mayorPuntaje");

        lblmayorPuntaje.text = "MayorPuntaje-> " + mayorPuntaje.ToString();

        switch(isMayorPuntaje)
        {
            case 1:
            {
                lblAviso.text = "Felicidades, superaste el puntaje mayor";                
                break;
            }
        }
    }
    #endregion

    #region  Funciones de la clase
    /// <summary>
    /// Funcion para cargar la escena 0 y volver a empezar el juego
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Funcion para cerrar el juego
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
    #endregion
}
