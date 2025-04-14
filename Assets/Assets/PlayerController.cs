using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public SpriteRenderer skin;
<<<<<<< Updated upstream
=======
    float horizontal;
    public bool walltriger;
    public bool wallcheck;
    public float stamina;
    public float staminaLeft;
    public ObjectController objectController;
>>>>>>> Stashed changes

    void FixedUpdate()
    {
        
    }

<<<<<<< Updated upstream
    public void movement (InputAction.CallbackContext context){
        if (Input.GetKey(KeyCode.Q)){
            rb.linearVelocity=new Vector2(1*speed,rb.linearVelocity.y);
            
        }
        else if (Input.GetKey(KeyCode.D)){
            rb.linearVelocity=new Vector2(-1*speed,rb.linearVelocity.y);
=======
    private void FixedUpdate(){ 
            rb.linearVelocity = new Vector2(horizontal*speed,rb.linearVelocity.y);   
            if (walltriger==true&& staminaLeft>0){
                staminaLeft-=0.25f;
                wallcheck=true;
            }
            else {
                wallcheck=false;
            }
            bump();
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
        if (context.performed && IsGrounded()){
            rb.linearVelocity=new Vector2(rb.linearVelocity.x, jumpower);
>>>>>>> Stashed changes
        }
    }

    bool IsGrounded(){
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(.5f,.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
<<<<<<< Updated upstream
=======
    
    void bump(){
        if (Physics2D.OverlapCapsule(groundCheck.position, new Vector2(.5f,.1f), CapsuleDirection2D.Horizontal, 0, enemyLayer)){
            rb.linearVelocity=new Vector2(rb.linearVelocity.x, jumpower);
            staminaLeft=stamina;
        }
    }
>>>>>>> Stashed changes
}
