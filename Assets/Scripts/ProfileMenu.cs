using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileMenu : MonoBehaviour
{

    public void AcessMenu()
    {
        SceneManager.LoadScene("ProfileMenu");
    }

    public void Back() 
    {
        SceneManager.LoadScene("PrincipalMenu");
    }
}
