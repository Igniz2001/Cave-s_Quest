using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KnightMovement : MonoBehaviour
{
    //en este script se hace referencia al personaje del jugador, todo lo relacionado
    // al jugador menos su ataque
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
    void Start()
    {
        //aqui se inicializan los componentes externos, sea rigidbody, sonidos etc.
        reproductor = GetComponent<AudioSource>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        LifeSlider.maxValue = Life;
        LifeSlider.value = LifeSlider.maxValue;
    }

    void Update()
    {
        //aqui se actualiza todas las entradas del jugador a lo largo del gameplay
        
        Horizontal = Input.GetAxisRaw("Horizontal"); //esto se encarga del movimiento 36,38,39,41

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", Horizontal != 0.0f);


        Debug.DrawRay(transform.position, Vector3.down * 0.3f, Color.red);//esto detecta si hay suelo para generar un enfriamiento para el salto del personaje para que no sea infinito
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

    private void Jump() //esto genera el salto del personaje
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Death() 
    {
        Destroy(gameObject);
        print("Has perdido bro");
        SceneManager.LoadScene("PrincipalMenu");
    }
    public void TakeDamage(float damage) //esto recibe el daño que le hacen al jugador
    {
        if (damage <= 0.0f)
        {
            return;
        }
        else
        {
            Life -= damage;
            LifeSlider.value = Life;
            Animator.SetTrigger("attacked");
            StartCoroutine(AttackStop());
        }
        if (Life <= 0)
        {
            
            Animator.SetTrigger("dying");
            Invoke(nameof(Death),1.2f);
        }
    }

    private IEnumerator AttackStop() // esto se llama cuando el jugador es atacado para evitar que pueda atacar en ese momento
    {
        print("me llamaron");
        gameObject.GetComponent<MeleeCombat>().canAttack = false;
        yield return new WaitForSeconds(0.6f);
        gameObject.GetComponent<MeleeCombat>().canAttack = true;
    }
    private void OnTriggerEnter2D(Collider2D collision) //esto maneja la barra de vida cuando la pocion de vida es recogida
    {
        if (collision.gameObject.tag == "Ruby")
        {
            reproductor.PlayOneShot(rubySound);
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
       Rigidbody2D.velocity = new Vector2(Horizontal * Speed * Time.fixedDeltaTime, Rigidbody2D.velocity.y);
    }
}
