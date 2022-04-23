using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killzone : MonoBehaviour
{
    [SerializeField] AudioClip fallScream;
    AudioSource fallScreamSource;

    private void Start()
    {
        fallScreamSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Hero")
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene("PrincipalMenu");
        }
        else if (other.gameObject.tag == "Enemy")
        {
            fallScreamSource.PlayOneShot(fallScream);
            Destroy (other.gameObject);
        }
    }
}
