using UnityEngine;
using UnityEngine.InputSystem;

public class WallController : MonoBehaviour {
    private Collider2D monCollider;
    private SpriteRenderer spriteRenderer;
    public bool triggerwall;
    public float stamina;
    public float staminaLeft;

    void Awake(){
        staminaLeft=stamina;
    }

    void Start() {
        monCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate(){
        if (triggerwall){
            staminaLeft-=0.25f;
        }
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)&&staminaLeft>0) {
            triggerwall = true;
            Color currentColor = spriteRenderer.color;
            currentColor.a = 0.5f;
            spriteRenderer.color = currentColor;
            monCollider.isTrigger = true;
        } 
        else if (Input.GetKeyUp(KeyCode.E)||staminaLeft==0) {
            triggerwall = false;
            Color currentColor = spriteRenderer.color;
            currentColor.a = 1f;
            spriteRenderer.color = currentColor;
            monCollider.isTrigger = false;
        }
    }
}