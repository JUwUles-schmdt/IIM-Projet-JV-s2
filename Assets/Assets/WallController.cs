using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Trigger Phasing through Walls
/// </summary>
public class WallController : MonoBehaviour {
    private Collider2D monCollider;
    private SpriteRenderer spriteRenderer;
    public bool triggerwall;
    public PlayerController _playercontroller;

    void Start() {
        monCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void FixedUpdate() {
        // Check Player wall input 
        if (_playercontroller.wallcheck==true){
            Color currentColor = spriteRenderer.color;
            currentColor.a = 0.5f;
            spriteRenderer.color = currentColor;
            monCollider.isTrigger = true;
        }
        else{
            Color currentColor = spriteRenderer.color;
            currentColor.a = 1f;
            spriteRenderer.color = currentColor;
            monCollider.isTrigger = false;
        }
        
    }
}