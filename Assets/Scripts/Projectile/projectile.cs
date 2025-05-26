using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    // Script a mettre sur votre projectile que vous allez tirer depuis le script attackDIST
    // IL doit y avoir un TRIGGER sur l'objet et un rigidbody

    [SerializeField] private float attractionForce = 10f;
    public float lifeTime = 5.0f;   // Le temps maximal que vivra le projectile (pour être sur qu'il se détruise au bout d'un moment)

    private void Start() {
        Destroy(gameObject, lifeTime);
    }

    // La fonction OnTriggerEnter s'enclenche quand votre Trigger touche un autre collider/trigger
    void OnTriggerEnter2D(Collider2D collision) {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.tag != "Player")              // Sinon si on touche un mur (un collider qui n'est PAS un trigger) et que ce n'est pas le joueur
        {
            Destroy(gameObject);        // On détruit simplement le projectile
        }


        if (collision.gameObject.layer == LayerMask.NameToLayer("Object"))
        {
            transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, attractionForce * Time.deltaTime);
        }
    }

    
}
