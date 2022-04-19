using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KnightMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Grounded;
    private Animator Animator;
    [SerializeField] private float Life;
    [SerializeField] Slider LifeSlider;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        LifeSlider.maxValue = Life;
        LifeSlider.value = LifeSlider.maxValue;
    }

    void Update()
    {

        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal<0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal>0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running",Horizontal != 0.0f );


        Debug.DrawRay(transform.position, Vector3.down * 0.3f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.3f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        if (Grounded == false)
        {
            Animator.SetBool("jumping", true);
        }
        else if(Grounded)
        {
            Animator.SetBool("jumping", false);
        }

    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Death()
    {
        Destroy(gameObject);
        print("Has perdido bro");
        SceneManager.LoadScene("PrincipalMenu");
    }
    public void TakeDamage(float damage)
    {
        Life -= damage;
        LifeSlider.value = Life;
        Animator.SetBool("attacked", true);
        if (Life <= 0)
        {
            Animator.SetTrigger("dying");
            Invoke(nameof(Death),1.2f);
        }
        else
        {
            Animator.SetBool("attacked", false);
        }
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal*Speed*Time.fixedDeltaTime, Rigidbody2D.velocity.y);
    }
}
