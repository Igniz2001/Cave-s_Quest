using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileMenu : MonoBehaviour
{
    public void Next() 
    {
        SceneManager.LoadScene("PrincipalMenu");
    }

    public void AccessLeaderboard()
    {
        SceneManager.LoadScene("ProfileMenu");
    }
}
