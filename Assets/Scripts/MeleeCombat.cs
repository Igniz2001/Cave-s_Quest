using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private Transform AttackController;
    [SerializeField] private float HitRatio;
    [SerializeField] private float HitDamage;
    private Animator Animator;


    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            Animator.SetBool("attacking", true);
            Hit();
        }
        else
        {
            Animator.SetBool("attacking", false);
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
