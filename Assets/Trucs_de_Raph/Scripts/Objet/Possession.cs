using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Possession : MonoBehaviour
{

    
    [SerializeField] private bool toucher = false;
    [SerializeField] private bool possessing = false;


    [SerializeField] private GameObject player;
    private BoxCollider2D _boxCol;

    [SerializeField] private float speedMove;

    [SerializeField] private LayerMask detectWall;
    [SerializeField] private float wallDist = 5f;

    private PlayerControllers _controller;

    void Start()
    {
        _boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toucher == true && Input.GetKeyDown(KeyCode.E))
        {
            possessing = true;
            _boxCol.isTrigger = true;
            

            player.transform.position = transform.position;

        }

        if (possessing == true && Input.GetButton("Horizontal"))
            Move();

        if (possessing == true && Input.GetKeyDown(KeyCode.R)) 
        {
            _boxCol.isTrigger = false;
            possessing = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            toucher = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        toucher = false;
        
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
