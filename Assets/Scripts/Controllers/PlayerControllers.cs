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

    public Possession possession;

    private SpriteRenderer _spriteRend;
    private Transform _transform;

    [Header("MOVE")]
    //[SerializeField] private float speedMove;
    public bool canMove = true;

    [Header("JUMP")]
    //[SerializeField] private float forceJump;
    public bool canJump = true;

    public float fallingSpeed;
    public float fallingDirection;
    public Transform groundCheck;
    public LayerMask enemyLayer;

    //Un bool pour savoir si l'on peut sauter ou non (utile pendant la possession)
    private int _limiteJump = 2;
    private int _currentJump;

    [Header("WALL")]
    [SerializeField] private LayerMask detectWall;
    [SerializeField] private int wallDist = 2;
    //public WallController _triggerwall;
    //public bool walltrigerchecker;
    //public float stamina;
    //public float staminaLeft;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        TryGetComponent(out _transform);
        TryGetComponent(out _collider2D);
        TryGetComponent(out _rb2d);
        TryGetComponent(out _spriteRend);
        TryGetComponent(out possession);

        
    }

    private void FixedUpdate()
    {
        slowFall();
        new Vector2(_rb2d.linearVelocity.x, fallingSpeed * _rb2d.linearVelocity.y);

        if (possession == null)
            return;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Horizontal") && canMove)
            Move();

        if (Input.GetKeyDown(KeyCode.Space) && _currentJump < _limiteJump && canJump)
            Jump();


        bump();


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


    

    void slowFall()
    {
        fallingDirection = Input.GetAxisRaw("Horizontal");
        if (fallingDirection < 0)
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



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _currentJump = 0;
        }
        
    }
    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            SetDamage(-1);

    }


}