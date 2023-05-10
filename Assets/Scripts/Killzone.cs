using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killzone : MonoBehaviour
{
    public string Nivel;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.gameObject.tag == "Hero")
        {
            Destroy(other.gameObject);
            ScoreController.instance.ResetPoints();
            SceneManager.LoadScene(Nivel);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            Destroy (other.gameObject);
        }
    }
}
