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

    void FixedUpdate()
    {
        
    }

    public void movement (InputAction.CallbackContext context){
        if (Input.GetKey(KeyCode.Q)){
            rb.linearVelocity=new Vector2(1*speed,rb.linearVelocity.y);
            
        }
        else if (Input.GetKey(KeyCode.D)){
            rb.linearVelocity=new Vector2(-1*speed,rb.linearVelocity.y);
        }
    }

    bool IsGrounded(){
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(.5f,.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
}
