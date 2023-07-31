using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : MonoBehaviour
{
    // en este script se maneja todo el comportamiento del enemigo del juego
    //este script cumple con las historias de usuario 7, 8 y 13
    AudioSource reproductor;
    public AudioClip death;
    public StateMachine state;
    public GameObject Knight;
    public float Speed;
    private float LastAttack;
    public float cooldown;
    private Animator Animator;
    [SerializeField] private float Life;
    [SerializeField] private Transform AttackController;
    [SerializeField] private float HitRatio;
    [SerializeField] private float HitDamage;
    public float soundEnter;
    public float deathEnter;
    private bool jumpingBackwards = false;
    public float JumpForce = 1.5f;

    [Header("Variables Bonitas")]
    public float attackRange = 1;
    public float followDistance = 4;
    public float escapeDistance = 6;
    public float borderRadio = 1;
    public float borderDistance = 1;

    bool isInBorder = false;
    private void Start()
    {   // Aqui se llama el animador para que las animaciones funcionen cuando se les llame en los metodos
        Animator = GetComponent<Animator>();
        reproductor = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Knight == null) return; // si el jugador no existe, entonces que omita cualquiera de las acciones aqui mostradas

        if (jumpingBackwards)
        {
            // Aplicar salto hacia atrás
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 movement = new Vector2(-1 * transform.localScale.x, 1); // Salto en dirección opuesta al enemigo
            rb.AddForce(movement * JumpForce, ForceMode2D.Impulse);
            jumpingBackwards = false;
        }

        switch (state) //aqui se inicia la maquina de estados y se definen cada uno de los estados que van a tomar partido en el codigo
        {
            case StateMachine.idle: //este es el estado del enemigo cuando no haga nada
                IdleState();
                break;
            case StateMachine.follow: //este es el estado del enemigo cuando persiga el heroe
                FollowState();
                break;
            case StateMachine.attack: //este es el estado del enemigo cuando ataque al heroe
                AttackState();
                break;
            case StateMachine.death: //este es el estado del enemigo cuando muere
                DeathState();
                break;
            default:
                break;
        }
        
    }

    public void Look() //este es un metodo para que el enemigo detecte al jugador
    {

        Vector3 direction = Knight.transform.position - transform.position;// con esto el enemigo siempre mira al jugador
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); // con la linea 26 y 27 rota al enemigo dependiendo de donde este el jugador

    }

    public void Attack() // despliega la animación, llama el metodo que causa el daño al jugador y le da un tiempo de enfriamiento
    {
        Animator.SetTrigger("enemyAttack");
        EnemyHit();
        LastAttack = Time.time;
    }

    private void Death()
    {   // Aqui se detona la muerte del enemigo 
        Destroy(gameObject);
    }

    public void IdleState() 
    {
        float distance = Mathf.Abs(Knight.transform.position.x - transform.position.x);
        if (distance < followDistance)
        {
            StateChange(StateMachine.follow);
        }
    }

    public void FollowState()
    {
        Look();
        EdgeLook();
        if (!isInBorder)
        {
            transform.Translate(Speed * Time.deltaTime * transform.localScale.x, 0, 0);
        }
        Animator.SetBool("running", !isInBorder);
        float distance = Mathf.Abs(Knight.transform.position.x - transform.position.x);
        if (distance < attackRange)
        {
            StateChange(StateMachine.attack);
        }
        else if (distance > escapeDistance)
        {
            StateChange(StateMachine.idle);
        }
    }

    public void AttackState()
    {
        Look();
        float distance = Mathf.Abs(Knight.transform.position.x - transform.position.x);
        if (distance < attackRange && Time.time > LastAttack + cooldown)
        {   //cuando la distancia sea menor a 1.0f, el enemigo atacará y cumplira con un tiempo de espera para el proximo ataque 
            Attack();
        }
        else if(distance > attackRange)
        {
            StateChange(StateMachine.follow);
        }
    }

    public void DeathState() //esto es solo para llenar la maquina de estados del enemigo
    {

    }

    public void StateChange(StateMachine e) //esto cambia los estados del enemigo a lo largo del juego 
    {
        if (state == StateMachine.follow)
        {
            Animator.SetBool("running", false);
        }
        state = e;
    }

    private void EdgeLook() //esto detecta los bordes de los abismos
    {

        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position + new Vector3(transform.localScale.x * borderDistance, 0, 0), borderRadio);
        isInBorder = false;
        foreach (Collider2D collider in objects)
        {
            if (collider.CompareTag("Border"))
            {
                isInBorder = true;
            }
        }
    }

    private void DeathSound() // esto solo reproduce un sonido de muerte cuando muere el enemigo
    {
        reproductor.PlayOneShot(death);
    }
    public void TakeDamage(float damage)
    {   // Aqui muestra como el enemigo toma daño de parte del jugador en caso de ser golpeado
        Life -= damage;
        Animator.SetTrigger("attacked");
        if (Life <= 0) //si la vida llega a 0...
        {
            HitDamage = 0.0f; //dejará de hacer daño al heroe
            Speed = 0.0f; //no se moverá
            Animator.SetTrigger("dying"); //se animará la muerte
            Invoke(nameof(DeathSound), soundEnter); // se llama con retardo el sonido y la muerte para que coincidan a la vez
            Invoke(nameof(Death), deathEnter);
            
        }
        else
        {
            jumpingBackwards = true;
        }
    }
    private void EnemyHit()
    {   //Esto hace que el enemigo le haga daño al jugador y llama el script del jugador para hacerle daño

        Collider2D[] objects = Physics2D.OverlapCircleAll(AttackController.position, HitRatio);

        foreach (Collider2D collider in objects)
        {
            if (collider.CompareTag("Hero"))
            {
                KnightMovement km = collider.transform.GetComponent<KnightMovement>();
                if (km != null) km.TakeDamage(HitDamage);

            }
        }
    }

    private void OnDrawGizmos()
    {   //Esto es para el enemy attack controller, la esfera verde que hará daño a todo lo que se encuentre dentro
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(AttackController.position, HitRatio);
        Gizmos.DrawWireSphere(transform.position + new Vector3(transform.localScale.x * borderDistance, 0, 0), borderRadio);
    }
}

public enum StateMachine{ //esta es la maquina de estados, donde se ordenan cada uno de los comportamientos del enemigo
    idle = 0,
    follow = 1,
    attack = 2,
    death = 3
}
