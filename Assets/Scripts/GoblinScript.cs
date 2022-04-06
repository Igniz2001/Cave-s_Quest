using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : MonoBehaviour
{
    public GameObject Knight;
    private float LastAttack;
    private Animator Animator;
    [SerializeField] private float Life;


    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Knight == null) return;

        Vector3 direction = Knight.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(Knight.transform.position.x - transform.position.x);
        if (distance < 1.0f && Time.time > LastAttack + 1.5f)
        {
            Attack();
            Animator.SetBool("enemyAttack", true);
            LastAttack = Time.time;
        }
        else
        {
            Animator.SetBool("enemyAttack", false);
        }

    }
    private void Death()
    {
        Animator.SetBool("dying", true);
    }
    public void TakeDamage(float damage)
    {
        Life -= damage;
        Animator.SetBool("attacked",true);
        if (Life <= 0.0f)
        {
            Death();
        }
        else
        {
            Animator.SetBool("attacked", false);
        }
    }
    private void Attack()
    {
        Debug.Log("Attack");
    }
}
