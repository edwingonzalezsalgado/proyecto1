using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
/// @Autor:Edwin gonzalez salgado
/// viva UCAMP
/// Proyecto 1: Quiz
/// </summary>
public class csPreguntas : MonoBehaviour
{   
    #region Atributos de la Clase publicos
    public TextMeshProUGUI lblPregunta;
    public TextMeshProUGUI lblNumeroPregunta;
    public TextMeshProUGUI lblPuntos;   
    public Button  btnRespuesta1;  
    public Button  btnRespuesta2;
    public Button  btnRespuesta3;
    public Button  btnRespuesta4;
    public TextMeshProUGUI Text1;
    public TextMeshProUGUI Text2;
    public TextMeshProUGUI Text3;
    public TextMeshProUGUI Text4;    
    private string preguntasQueYaSalieron;
    private string respuestasQueYaSalieron;
    private int contadorPuntos;
    private int contadorPreguntas;
    public csBackTimer objBackTimer;
    #endregion

    #region Constructores de la Clase
    /// <summary>
    /// Constructor por default de la clase csPreguntas
    /// </summary>
    public csPreguntas()
    {
        respuestasQueYaSalieron = string.Empty;
        preguntasQueYaSalieron = string.Empty;
        contadorPuntos = 0;
        contadorPreguntas = 0;
    }
    #endregion

    #region Eventos de la Clase
    /// <summary>
    /// Evento unity start utilizado en escena
    /// </summary>
    void Start()
    {
        BuildInterfaz();
    }
    /// <summary>
    /// Evento cuando se presiona un boton
    /// </summary>
    /// <param name="btn">boton presionado</param>
    public void btn_onClick(Button btn)
    {   
        BloqueaActivaBotones(btnRespuesta1,false);
        BloqueaActivaBotones(btnRespuesta2,false);
        BloqueaActivaBotones(btnRespuesta3,false);
        BloqueaActivaBotones(btnRespuesta4,false);

        if(btn.tag == "correcta")
        {
            contadorPuntos = contadorPuntos + 100;
            lblPuntos.text = contadorPuntos.ToString();
        }

        msj("boton presionado: " + btn.name);
        ValidaBotones(btnRespuesta1,btn);
        ValidaBotones(btnRespuesta2,btn);
        ValidaBotones(btnRespuesta3,btn);
        ValidaBotones(btnRespuesta4,btn);    
        StartCoroutine(tiempoDeEspera(0.5f));        
    }    
    /// <summary>
    /// Evento cuando se presiona el boton de terminar
    /// </summary>
    public void btnTerminarJuego_onClick()
    { 
        TerminarJuego();
    }
    #endregion
    
    #region Metodos de la clase
    /// <summary>
    /// Metodo para cargar las preguntas y respuestas de manera aleatoria, y valida tambien si acaba el juego
    /// </summary>
    private void BuildInterfaz()
    {
        if(preguntasQueYaSalieron.Length >= 9)//si ya salieron las 9 preguntas hay que finalizar el juego
        {
            TerminarJuego();
        }
        else //el juego continua
        {            
            string[] respuestas;

            validaLabelText(lblPregunta, out respuestas);
            validaButtonText(btnRespuesta1, Text1, respuestas);
            validaButtonText(btnRespuesta2, Text2, respuestas);
            validaButtonText(btnRespuesta3, Text3, respuestas);
            validaButtonText(btnRespuesta4, Text4, respuestas);           
        }
    }
    /// <summary>
    /// Metodo que valida la pregunta a mostrar
    /// </summary>
    /// <param name="lbl">text donde se va a mostrar la pregunta</param>
    /// <param name="respuestas">arreglo con las respuestas de la pregunta que se defina</param>
    private void validaLabelText(TextMeshProUGUI lbl, out string[] respuestas)
    {        
        respuestas = new string[4];
        bool listo = false;
        string pregunta = string.Empty;

        do
        {
            int pos = getRandomNumber(10);            

            msj("pos pregunta: " + pos.ToString());     

            if(!preguntasQueYaSalieron.Contains(pos.ToString()))//si ya salió esa pregunta
            {
                GetPreguntaYrespuestas(pos, out pregunta, out respuestas);  
                listo = true;
                lbl.text = pregunta;
                contadorPreguntas++;
                lblNumeroPregunta.text = "Numero de pregunta: " + contadorPreguntas.ToString() + "/9";
                //msj("lbl.text : "+lbl.text);
                preguntasQueYaSalieron += pos.ToString();                
            }
            else
            {
                //msj("buscando otra pregunta");
            }            
        } while (!listo);

        msj("preguntasQueYaSalieron: " + preguntasQueYaSalieron);
    }
    /// <summary>
    /// Metodo para cargar el texto en los botones y marcar cual va ser el boton con la respuesta correcta o incorrecta
    /// </summary>
    /// <param name="btn">Boton a marcar como correcto o no</param>
    /// <param name="txt">Texto donde irá la respuesta</param>
    /// <param name="respuestas">Arreglo con las respuestas a mostrar</param>
    private void validaButtonText(Button btn, TextMeshProUGUI txt, string[] respuestas)
    {        
        bool listo = false;
        
        do
        {
            int pos = getRandomNumber(5);            

            if(!respuestasQueYaSalieron.Contains(pos.ToString()))//si ya salió esa respuesta
            {
                listo = true;
                txt.text = respuestas[pos-1];
                respuestasQueYaSalieron += pos.ToString();
                
                if(pos == 1)//correcta
                {
                    btn.tag = "correcta";                    
                }
                else//incorrecta
                {
                    btn.tag = "incorrecta";
                }                
            }
            else
            {
                //msj("buscando otra vez");
            }            
        } while (!listo);
        //msj("respuestasQueYaSalieron: " + respuestasQueYaSalieron);
    }
    
    /// <summary>
    /// Metodo que espera un tiempo para volver a cargar las preguntas y respuestas
    /// </summary>
    /// <param name="time">tiempo de espera</param>
    /// <returns>regresa un objeto de tipo IEnumerator</returns>
    public IEnumerator tiempoDeEspera(float time)
    {         
        
        yield return new WaitForSeconds(time);
        //SceneManager.LoadScene(1);  
        ResetControls();
        BuildInterfaz(); 
    }
    /// <summary>
    /// Metodo para establecer las propiedades de los controles a su valor por default o necesario
    /// </summary>
    private void ResetControls()
    {
        objBackTimer.startTime = 10;
        respuestasQueYaSalieron = string.Empty;
        changeButtonColor(btnRespuesta1,Color.white);
        changeButtonColor(btnRespuesta2,Color.white);
        changeButtonColor(btnRespuesta3,Color.white);
        changeButtonColor(btnRespuesta4,Color.white);
        BloqueaActivaBotones(btnRespuesta1,true);
        BloqueaActivaBotones(btnRespuesta2,true);
        BloqueaActivaBotones(btnRespuesta3,true);
        BloqueaActivaBotones(btnRespuesta4,true);
    }
    /// <summary>
    /// Metodo para terminar el juego
    /// </summary>
    private void TerminarJuego()
    {
        int mayorPuntaje = PlayerPrefs.GetInt("mayorPuntaje");

        PlayerPrefs.SetInt("contadorPuntos", contadorPuntos);
        PlayerPrefs.SetInt("isMayorPuntaje", 0);
        
        if(contadorPuntos>mayorPuntaje)
        {
            PlayerPrefs.SetInt("isMayorPuntaje", 1);
            PlayerPrefs.SetInt("mayorPuntaje", contadorPuntos);
        }
        
        ChangeScene("Fin");
    }
    #endregion

    #region Funciones de la Clase
    /// <summary>
    /// Funcion para mostrar mensajes en consola, ayuda para desactivar todos los mensaje en escena
    /// </summary>
    /// <param name="msj">mensaje a mostrar</param>
    private void msj(string msj)
    {
        Debug.Log(msj);
    }
    /// <summary>
    /// Funcion para obtener un numero aleatorio
    /// </summary>
    /// <param name="limit">indica el numero maximo exclusivo</param>
    /// <returns></returns>
    private int getRandomNumber(int limit)
    {        
        int number = UnityEngine.Random.Range(1, limit);

        return number;
    }
    /// <summary>
    /// Funcion para obtener una pregunta y sus respuestas
    /// </summary>
    /// <param name="opcion">opcion de pregunta a obtener</param>
    /// <param name="pregunta">cadena de salida de preguntas</param>
    /// <param name="respuestas">arreglo de salida de respuestas</param>
    private void GetPreguntaYrespuestas(int opcion, out string pregunta, out string[] respuestas)
    {
        pregunta = string.Empty;
        respuestas = new string[4];

        switch(opcion)
        {
            case 1:
            {
                pregunta = "¿Cuándo comienza la primera guerra mundial? ";
                respuestas = new string[] {"1910", "1810", "1922","1897"};
                break;
            }
            case 2:
            {
                pregunta = "¿Cuál es la civilización más antigua del mundo? ";
                respuestas = new string[] {"mesopotamia", "los minions", "neandertales","los chinos"};
                break;
            }
            case 3:
            {
                pregunta = "¿Cuál es la última dinastía en China?";
                respuestas = new string[] {"dinastía Qing", "yakuza", "origami","chim cham pu"};
                break;
            }
            case 4:
            {
                pregunta = "¿Quién es el primer presidente de los Estados Unidos? ";
                respuestas = new string[] {"Washington ", "lincon", "bush","obama bin laden"};
                break;
            }
            case 5:
            {
                pregunta = "¿Dónde se llevan a cabo los primeros Juegos Olímpicos de Verano?";
                respuestas = new string[] {"Atenas", "italia", "egipto","india"};
                break;
            }
            case 6:
            {
                pregunta = "¿Cuál es la dinastía más antigua que sigue gobernando?";
                respuestas = new string[] {"Japón", "inglaterra", "mexico","korea"};
                break;
            }
            case 7:
            {
                pregunta = "¿De qué país se originó la civilización azteca? ";
                respuestas = new string[] {"México", "brasil", "argentina","honduras"};
                break;
            }
            case 8:
            {
                pregunta = "¿Quién exploró el Nuevo Mundo?";
                respuestas = new string[] {"Cristobal colon.", "ash ketchup", "maverick","google"};
                break;
            }
            case 9:
            {
                pregunta = "¿Dónde se encuentra Babilonia?";
                respuestas = new string[] {"Irak", "polonia", "vietnam","ucrania"};
                break;
            }
        }
        
    }
    /// <summary>
    /// Funcion para cambiar el color de un boton
    /// </summary>
    /// <param name="btn">boton a modificar</param>
    /// <param name="selectedColor">color deseado</param>
    private void changeButtonColor(Button btn, Color selectedColor)
    {            
        ColorBlock btnColor = btn.colors;
        btnColor.normalColor = selectedColor;   
        btnColor.selectedColor = selectedColor; 
        btn.colors = btnColor;            
    }
    /// <summary>
    /// Funcion para cambiar de escena
    /// </summary>
    /// <param name="sceneName">nombre de la escena a mostrar</param>
    private void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
    }
    /// <summary>
    /// Funcion que valida si el boton tiene la respuesta correcta o incorrecta
    /// </summary>
    /// <param name="btn">boton a validar</param>
    private void ValidaBotones(Button btn, Button btnPresionado)
    {
        if((btn.tag == "correcta"))
        {
            changeButtonColor(btn,Color.green);                        
        }
        else if((btn.tag == "incorrecta")&&(btn.name == btnPresionado.name))
        {
            changeButtonColor(btn,Color.red);
        }    
    }
    /// <summary>
    /// Funcion para bloquear o activar un boton dado un valor
    /// </summary>
    /// <param name="btn">boton a bloquear o activar</param>
    /// <param name="valor">indica si se va a bloquear o activar</param>
    private void BloqueaActivaBotones(Button btn, bool valor)
    {
        btn.enabled = valor;
    }

    #endregion
}
