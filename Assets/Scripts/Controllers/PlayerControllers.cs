using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerControllers : MonoBehaviour
{
    [field: SerializeField] public Stats Stat { get; private set; }

    private Rigidbody2D _rb2d;
    private Collider2D _collider2D;

    private SpriteRenderer _spriteRend;
    private Transform _transform;

    [Header("MOVE")]
    //[SerializeField] private float speedMove;
    [SerializeField] private bool canMove = true; //Un bool pou savoir si l'on peut bouger ou non (utile pendant la possession)

    [Header("JUMP")]
    //[SerializeField] private float forceJump;
    [SerializeField] private bool canJump = true;
    public float fallingSpeed;
    public float falllingDirection;
    public Transform groundCheck;
    public LayerMask enemyLayer;

    //Un bool pou savoir si l'on peut sauter ou non (utile pendant la possession)
    //[SerializeField] private Transform groundCheck;
    private int _limiteJump = 2;
    private int _currentJump;

    [Header("WALL")]
    [SerializeField] private LayerMask detectWall;
    [SerializeField] private int wallDist = 2;
    //public WallController _triggerwall;
    //public bool walltrigerchecker;
    //public float stamina;
    //public float staminaLeft;

    //La partie possess est une partie qui permet de poss�der les objet quel qu'ils soient
    [Header("POSSESS")]
    [SerializeField] private bool _possess = false; //Un bool pour savoir si l'on est en train de poss�der ou non
    [SerializeField] private bool toucher = false; //Un bool pour savoir si l'on est dans une zone o� l'on peut poss�der
    [SerializeField] private bool isTV = false;
    [SerializeField] private bool isVase = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        TryGetComponent(out _transform);
        TryGetComponent(out _collider2D);
        TryGetComponent(out _rb2d);
        TryGetComponent(out _spriteRend);
        
    }

    private void FixedUpdate()
    {
        slowFall();
        new Vector2(_rb2d.linearVelocity.x, fallingSpeed * _rb2d.linearVelocity.y);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Horizontal") && canMove == true)
            Move();

        if (Input.GetKeyDown(KeyCode.Space) && _currentJump < _limiteJump && canJump)
            Jump();

        //Si on est dans une zone de possession et que l'on appuie sur E on poss�de, si on poss�de et qu'on appuie sur R on d�poss�de
        if (toucher == true && Input.GetKeyDown(KeyCode.E))
        { 
            Possess();
            if(isTV == true)
                TV();
        }

        if (_possess == true && Input.GetKeyDown(KeyCode.R))
            ExitPossess();


    }

    void Move()
    {
        Vector3 direction = Input.GetAxisRaw("Horizontal") * Vector2.right;

        var hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, direction, wallDist, detectWall);

        if (hit.collider != null)
            return;

        Debug.DrawRay(transform.position, direction * wallDist);

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //if (!Input.GetKey(KeyCode.LeftControl))
        //    _spriteRend.flipX = Input.GetAxisRaw("Horizontal") < 0;

        transform.position += direction * Stat.speedMove * Time.deltaTime;
    }

    void Jump()
    {
        _rb2d.linearVelocity = Vector2.up * Stat.forceJump;
        _currentJump++;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            
            _currentJump = 0;
        }

    }

    void slowFall()
    {
        falllingDirection = Input.GetAxisRaw("Horizontal");
        if (falllingDirection < 0)
        {
            fallingSpeed = 0;
        }
        else
        {

            fallingSpeed = 1f;

        }
    }

    void bump()
    {
        if (Physics2D.OverlapCapsule(groundCheck.position, new Vector2(.5f, .1f), CapsuleDirection2D.Horizontal, 0, enemyLayer))
        {
            Debug.Log("yolooooooooooooooo");
            _rb2d.linearVelocity = new Vector2(_rb2d.linearVelocity.x, Stat.forceJump);
        }
    }



    public void SetDamage(int degat)
    {
        Stat.life = Mathf.Abs(degat);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si on entre dans le Trigger d'un objet, on peut le poss�der
        if (collision.gameObject.layer == LayerMask.NameToLayer("Object"))
        {
            toucher = true;
        }

        if (collision.gameObject.tag == "TV")
        {
            isTV = true;
        }


    }
    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            SetDamage(-1);


        //Si on sort du Trigger d'un objet, on peut plus le poss�der
        if (collision.gameObject.layer == LayerMask.NameToLayer("Object"))
        {
            toucher = false;
        }

        if (collision.gameObject.tag == "TV")
        {
            isTV = false;
        }
    }



    //La fonction pour poss�der
    void Possess()
    {
        _possess = true;

        Color currentColor = _spriteRend.color;
        currentColor.a = 0;
        _spriteRend.color = currentColor;
        canJump = false;
    }
    //La fonction pour d�poss�der
    void ExitPossess()
    {
        _possess = false;

        Color currentColor = _spriteRend.color;
        currentColor.a = 1;
        _spriteRend.color = currentColor;
        canJump = true;
        canMove = true;
    }
    
    void TV()
    {
        canMove = false;
    }
}