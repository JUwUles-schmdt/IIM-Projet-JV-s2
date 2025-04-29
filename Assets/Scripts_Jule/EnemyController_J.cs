using UnityEngine;

public class EnemyController_J : MonoBehaviour
{
    public LayerMask playerlayer;
    public Transform playercheck;
    public WallController staminaLeft;
    public WallController stamina;

    void Start()
    {
        
    }


    void playerchecker(){
        if (Physics2D.OverlapCapsule(playercheck.position, new Vector2(.5f,.1f), CapsuleDirection2D.Horizontal, 0, playerlayer)){
            staminaLeft=stamina;
        }

    }
}
