using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KnightMovement : MonoBehaviour
{
    AudioSource reproductor;
    public float Speed;
    public float JumpForce;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Grounded;
    private Animator Animator;
    public float Life;
    [SerializeField] AudioClip potionSound;
    [SerializeField] AudioClip rubySound;
    [SerializeField] Slider LifeSlider;
    public static int playerPoints;
    public Text Score;
    void Start()
    {
        reproductor = GetComponent<AudioSource>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        LifeSlider.maxValue = Life;
        LifeSlider.value = LifeSlider.maxValue;
    }

    void Update()
    {
        Score.text = "Score: " + playerPoints;

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

    public void IncreasePoints(int amount)
    {
        playerPoints += amount;
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
        if (damage <= 0.0f) { return; }
        Life -= damage;
        LifeSlider.value = Life;
        Animator.SetTrigger("attacked");
        if (Life <= 0)
        {
            Animator.SetTrigger("dying");
            Invoke(nameof(Death),1.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ruby")
        {
            IncreasePoints(100);
            reproductor.PlayOneShot(rubySound);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Potion")
        {
            reproductor.PlayOneShot(potionSound);
            if (Life > 0 && Life <= 800)
            {
                Life += 200;
                LifeSlider.value = Life;
                Destroy(collision.gameObject);
            }
            else if (Life == 1000)
            {
                Life += 0;
                Destroy(collision.gameObject);
            }
            else if (Life >= 850)
            {
                Life = 1000;
                LifeSlider.value = Life;
                Destroy(collision.gameObject);
            }
        }
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal*Speed*Time.fixedDeltaTime, Rigidbody2D.velocity.y);
    }
}
