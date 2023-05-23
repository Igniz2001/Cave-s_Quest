using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public string scene;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Scene escene = SceneManager.GetActiveScene();

        if (escene.buildIndex == 4 && other.gameObject.tag == "Hero" )
        {
            print("Has ganado el nivel");
            SceneManager.LoadScene(scene);
        }

        else if (other.gameObject.tag == "Hero")
        {
            print("Has ganado el nivel");
            SceneManager.LoadScene(scene);
        }
        
    }
}
