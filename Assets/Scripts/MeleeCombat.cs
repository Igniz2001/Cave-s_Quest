using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    AudioSource reproductor;
    [SerializeField] AudioClip swordSwing;
    [SerializeField] private Transform AttackController;
    [SerializeField] private float HitRatio;
    [SerializeField] private float HitDamage;
    [SerializeField] private float TimeBetweenHits;
    [SerializeField] private float TimeForNextHit;
    private Animator Animator;


    private void Start()
    {
        reproductor = GetComponent<AudioSource>();
        Animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(TimeForNextHit > 0)
        {
            TimeForNextHit -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.J) && TimeForNextHit <= 0)
        {
            reproductor.PlayOneShot(swordSwing);
            Animator.SetTrigger("attacking");
            Hit();
            TimeForNextHit = TimeBetweenHits;
        }
    }
    private void Hit()
    {
        
        Collider2D[] objects = Physics2D.OverlapCircleAll(AttackController.position, HitRatio);
        
        foreach (Collider2D collider in objects)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.transform.GetComponent<GoblinScript>().TakeDamage(HitDamage);
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackController.position, HitRatio);
    }
}
