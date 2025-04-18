using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerControllers : MonoBehaviour
{
    //public float speed;
    //public float jumpower;
    //public Rigidbody2D rb;
    //public LayerMask groundLayer;
    //public LayerMask enemyLayer;
    //public Transform groundCheck;
    //public SpriteRenderer skin;
    //float horizontal;
    //public WallController _triggerwall;
    //public bool walltrigerchecker;
    //public float stamina;
    //public float staminaLeft;



    private Rigidbody2D _rb2d;
    private Collider2D _collider2D;

    private SpriteRenderer _spriteRend;
    private Transform _transform;

    [Header("MOVE")]
    [SerializeField] private float speedMove;
    [SerializeField] private bool canMove = true; //Un bool pou savoir si l'on peut bouger ou non (utile pendant la possession)

    [Header("JUMP")]
    [SerializeField] private float forceJump;
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
    [SerializeField] private int wallDist = 5;
    //public WallController _triggerwall;
    //public bool walltrigerchecker;
    //public float stamina;
    //public float staminaLeft;

    [Header("DASH")]
    [SerializeField] private float _dashingPower = 15f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;
    private bool _canDash = true;
    private bool _isDashing;

    //La partie possess est une partie qui permet de posséder les objet quel qu'ils soient
    [Header("POSSESS")]
    [SerializeField] private bool _possess = false; //Un bool pour savoir si l'on est en train de posséder ou non
    [SerializeField] private bool toucher = false; //Un bool pour savoir si l'on est dans une zone où l'on peut posséder
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
        if (_isDashing)
            return;

        if (Input.GetButton("Horizontal") && canMove == true)
            Move();

        if (Input.GetKeyDown(KeyCode.Space) && _currentJump < _limiteJump && canJump)
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
            StartCoroutine(Dash());

        //Si on est dans une zone de possession et que l'on appuie sur E on possède, si on possède et qu'on appuie sur R on dépossède
        if (toucher == true && Input.GetKeyDown(KeyCode.E))
        { 
            Possess();
            if(isTV == true)
                TV();
        }

        if (_possess == true && Input.GetKeyDown(KeyCode.R))
            ExitPossess();


    }

    private void Move()
    {
        Vector3 direction = Input.GetAxis("Horizontal") * Vector2.right;
        var hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, direction, wallDist, detectWall);

        if (hit.collider != null)
            return;

        Debug.DrawRay(transform.position, direction * wallDist);
        if (!Input.GetKey(KeyCode.LeftControl))
            _spriteRend.flipX = Input.GetAxis("Horizontal") < 0;

        _transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * speedMove * Time.deltaTime;
    }

    void Jump()
    {
        _rb2d.linearVelocity = Vector2.up * forceJump ;
        _currentJump++;
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            
            _currentJump = 0;
        }

    }

    void bump()
    {
        if (Physics2D.OverlapCapsule(groundCheck.position, new Vector2(.5f, .1f), CapsuleDirection2D.Horizontal, 0, enemyLayer))
        {
            Debug.Log("yolooooooooooooooo");
            _rb2d.linearVelocity = new Vector2(_rb2d.linearVelocity.x, forceJump);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si on entre dans le Trigger d'un objet, on peut le posséder
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
        //Si on sort du Trigger d'un objet, on peut plus le posséder
        if (collision.gameObject.layer == LayerMask.NameToLayer("Object"))
        {
            toucher = false;
        }

        if (collision.gameObject.tag == "TV")
        {
            isTV = false;
        }
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        float ogGravity = _rb2d.gravityScale;
        _rb2d.gravityScale = 0f;
        _rb2d.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * _dashingPower, 0f);
        yield return new WaitForSeconds(_dashingTime);
        _rb2d.gravityScale = ogGravity;
        _isDashing = false;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }

    //La fonction pour posséder
    void Possess()
    {
        _possess = true;

        Color currentColor = _spriteRend.color;
        currentColor.a = 0;
        _spriteRend.color = currentColor;
        canJump = false;
    }
    //La fonction pour déposséder
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

}