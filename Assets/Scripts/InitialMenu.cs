using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenu : MonoBehaviour
{
    //aqui se gestionan los botones del menu principal, el botón que dará inicio al juego 
    //y el botón que dará la salida del juego

    public void PlayGame() //este método cumple con la historia de usuario 1
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame() //este método cumple con la historia de usuario 2
    {
        Debug.Log("Se Salio xd");
        Application.Quit();
    }
}
