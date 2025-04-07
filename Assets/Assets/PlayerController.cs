using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpower;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public Transform groundCheck;
    public SpriteRenderer skin;
    float horizontal;
    public WallController _triggerwall;
    public bool walltrigerchecker;
    public float stamina;
    public float staminaLeft;

    void Awake(){
        staminaLeft=stamina;
    }

    private void FixedUpdate(){ 
            rb.linearVelocity = new Vector2(horizontal*speed,rb.linearVelocity.y);   
            if (walltrigerchecker==true&& staminaLeft>0){
                staminaLeft-=0.25f;
            }
            bump();
    }

    public void wallmode(InputAction.CallbackContext context){
        if(Input.GetKeyDown(KeyCode.E)){
            walltrigerchecker=true;
        }
        else if (Input.GetKeyUp(KeyCode.E)){
            walltrigerchecker=false;
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
        if (context.performed && IsGrounded()){
            rb.linearVelocity=new Vector2(rb.linearVelocity.x, jumpower);
        }
    }

    bool IsGrounded(){
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(.5f,.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
    
    void bump(){
        if (Physics2D.OverlapCapsule(groundCheck.position, new Vector2(.5f,.1f), CapsuleDirection2D.Horizontal, 0, enemyLayer)){
            rb.linearVelocity=new Vector2(rb.linearVelocity.x, jumpower);
        }
    }
}
