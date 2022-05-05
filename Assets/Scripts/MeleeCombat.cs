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
    public bool canAttack = true;
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
        if (canAttack)
        {
            if (Input.GetKey(KeyCode.J) && TimeForNextHit <= 0) //cuando el jugador presione J y se haya cumplido su tiempo de eenfriamiento, atacará
            {
                reproductor.PlayOneShot(swordSwing);
                Animator.SetTrigger("attacking");
                Hit();
                TimeForNextHit = TimeBetweenHits;
            }
        }
    }
    private void Hit() // cuando identifique un enemigo con la etiqueta enemy, mandará una referencia al script del enemigo para causarle daño
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

    private void OnDrawGizmos() // se encarga de generar el circulo que será el rango de ataque del jugador
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackController.position, HitRatio);
    }
}
