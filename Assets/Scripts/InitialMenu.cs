using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenu : MonoBehaviour
{
    //aqui se gestionan los botones del menu principal, el bot�n que dar� inicio al juego 
    //y el bot�n que dar� la salida del juego

    public void PlayGame() //este m�todo cumple con la historia de usuario 1
    {
        SceneManager.LoadScene("PlayerName");
    }

    public void ExitGame() //este m�todo cumple con la historia de usuario 2
    {
        Debug.Log("Se Salio xd");
        Application.Quit();
    }
}
