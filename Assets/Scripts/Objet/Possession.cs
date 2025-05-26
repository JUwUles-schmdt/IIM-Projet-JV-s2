using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Possession : MonoBehaviour
{

    [SerializeField] private bool possessing = false;

    [SerializeField] private GameObject emission;

    [SerializeField] private PlayerControllers playcont;

    private BoxCollider2D _boxCol;

    [SerializeField] private float speedMove;

    [SerializeField] private LayerMask detectWall;
    [SerializeField] private float wallDist = 1f;

    private PlayerControllers _controller;

    public enum OBJECTS
    {
        NONE,
        BOX,
        TV,
        VASE
    }
    [SerializeField] private OBJECTS _objects;

    void Start()
    {
        if(emission == null)
        {
            return;
        }
        _boxCol = GetComponent<BoxCollider2D>();
        _controller = GetComponent<PlayerControllers>();
        emission = GetComponent<GameObject>();
        playcont = GetComponent<PlayerControllers>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //if (toucher == true && Input.GetKeyDown(KeyCode.E))
        //{
        //    possessing = true;
        //    _boxCol.isTrigger = true;


        //    player.transform.position = transform.position;

        //}

        //if (possessing == true && Input.GetButton("Horizontal"))
        //    Move();

        //if (possessing == true && Input.GetKeyDown(KeyCode.R)) 
        //{
        //    _boxCol.isTrigger = false;
        //    possessing = false;
        //}

        if (Input.GetKeyDown(KeyCode.R))
        {
            possessing = false ;
            playcont.canJump = true;
            playcont.canMove = true;
        }

        if (possessing == true)
        {
            playcont.canJump = false ;
            playcont.canMove = false ;
            if (_objects == OBJECTS.BOX)
            {
                Debug.Log("box");
                Move();
            }
            else if (_objects == OBJECTS.TV) 
            {
                
            }
        }

    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shoot")
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shoot")
        {
            possessing = true;
            Debug.Log("il est 1h du zbah, marches un peu!");
        }
    }


    private void Move()
    {
        Vector3 direction = Input.GetAxis("Horizontal") * Vector2.right;
        var hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, direction, wallDist, detectWall);

        if (hit.collider != null)
            return;

        Debug.DrawRay(transform.position, direction * wallDist);


        transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * speedMove * Time.deltaTime;
    }


}
