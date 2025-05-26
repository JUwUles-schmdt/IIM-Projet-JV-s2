using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float jumpower;
    public float speed;
    public float fallingSpeed;
    public float fallingDirection;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public LayerMask wallLayer;
    public Transform groundCheck;
    public SpriteRenderer skin;
    float horizontal;
    public bool walltriger;
    public bool wallcheck;
    public bool walltoggle;
    public float stamina;
    public float staminaLeft;
    public EnemyController EnemyController;
    public float maxHp;
    public float currentHp;
    private bool isdead;
    public Transform lastChekpoint;
    public GameObject healthBar;

    void awake(){
        lastChekpoint =GameObject.transform;
        currentHp =maxHp;
        staminaLeft=stamina;
        isdead=false;
    }
    public void movement (InputAction.CallbackContext context){
        if (Input.GetKey(KeyCode.Q)&&!isdead){
            rb.linearVelocity=new Vector2(1*speed,rb.linearVelocity.y);
            
        }
        else if (Input.GetKey(KeyCode.D)&&!isdead){
            rb.linearVelocity=new Vector2(-1*speed,rb.linearVelocity.y);
        }}
    private void FixedUpdate(){ 
            rb.linearVelocity = new Vector2(horizontal*speed,fallingSpeed*rb.linearVelocity.y);   
            if (walltriger==true&& staminaLeft>0){
                staminaLeft-=0.25f;
                wallcheck=true;
            }
            else {
                wallcheck=false;
            }
            bump();
            IsFalling();
    }
    public void wallmode(InputAction.CallbackContext context){
        if(Input.GetKeyDown(KeyCode.E)){
            walltriger=true;
        }
        else if (Input.GetKeyUp(KeyCode.E)){
            walltriger=false;
        }
    }
    public void Move(InputAction.CallbackContext context){
        horizontal = context.ReadValue<Vector2>().x;

        if (horizontal<0)
        {
            skin.flipX=true;
        }
        else if (horizontal>0)
        {
            skin.flipX=false;
        }
    }
    public void jump(InputAction.CallbackContext context){
        if (context.performed && IsGrounded()&&!isdead){
            rb.linearVelocity=new Vector2(rb.linearVelocity.x, jumpower);

        }
    }
    bool IsGrounded(){
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(.5f,.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
    void bump(){
        if (Physics2D.OverlapCapsule(groundCheck.position, new Vector2(.5f,.1f), CapsuleDirection2D.Horizontal, 0, enemyLayer)&&!isdead){
            rb.linearVelocity=new Vector2(rb.linearVelocity.x, jumpower);
            staminaLeft=stamina;
        }
    }
    void IsFalling(){
        fallingDirection = Input.GetAxis("Vertical");

        if (fallingDirection < 0){
            fallingSpeed = 0.5f;
        }
        else {
            fallingSpeed = 1f;
        }
}

    public void dead(){
        isdead=true;
        rb.linearVelocity = Vector2.zero;
        transform.position = lastChekpoint.position;
        currentHp = maxHp;
        stamina = staminaLeft;
    }
    public void enemyChecker(){
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            if (currentHp!=1){
                currentHp-=1;
            }
            else{
                dead();
            }
        }
    }
}