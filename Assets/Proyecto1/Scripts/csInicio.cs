using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// @Autor:Edwin gonzalez salgado
/// viva UCAMP
/// Proyecto 1: Quiz
/// </summary>
public class csInicio : MonoBehaviour
{
    #region Eventos de la Clase
    /// <summary>
    /// Evento cuando se presiona el boton de inicar
    /// </summary>
    public void btnIniciar_onClick()
    {
        ChangeScene("Preguntas");
    }
    #endregion

    #region Funciones de la clase
    /// <summary>
    /// Funcion para cambiar de escena
    /// </summary>
    /// <param name="sceneName">nombre de la escena a mostrar</param>
    private void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
    }
    #endregion
}
