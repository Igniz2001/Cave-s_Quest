using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool triggered;
    AudioSource reproductor;
    [SerializeField] private GameObject Enemies;

    private void Start()
    {
        reproductor = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            triggered = true;
            Enemies.SetActive(true);
            reproductor.Play();
        }
    }
}
