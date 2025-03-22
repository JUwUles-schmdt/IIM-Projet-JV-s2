using UnityEngine;
using System.Collections;

public class attackDist : MonoBehaviour
{    
    public int degats = 1;
    public Transform weapon;                    
    private Vector3 positionWeapon;             
    public GameObject projectil;                
    private GameObject projectilSave;           

    public float speedProjectil = 5f;           
    public float projectilLifeTime = 3f;       
    public float reloadTime = 1f;             
    private bool reloading;                     

    private Vector3 mousePos;                   
    private Vector3 direction;                  
    private float angleProjectil;               

    private SpriteRenderer skin;                

    private Animator anim;                      

    void Start() {
        skin = GetComponent<SpriteRenderer>();  
        positionWeapon = weapon.localPosition;  
    }

    void Update() {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;                              
        direction = mousePos - weapon.position;                         
        direction.Normalize();                                          
        angleProjectil = Vector3.SignedAngle(transform.right, direction, Vector3.forward);  
        weapon.rotation = Quaternion.Euler(0, 0, angleProjectil);       

        if (!skin.flipX) {
            weapon.localPosition = positionWeapon;     
        }

        if (skin.flipX) {
            weapon.localPosition = new Vector3(-positionWeapon.x, positionWeapon.y, 0);    
        }


        
        if (Input.GetButton("Fire2") && !reloading) {
            reloading = true;              
            projectilSave = Instantiate(projectil, weapon.position, Quaternion.Euler(0, 0, angleProjectil));    
            projectilSave.GetComponent<Rigidbody2D>().linearVelocity = direction * speedProjectil;                    
            projectilSave.GetComponent<projectile>().degats = degats;
            projectilSave.GetComponent<projectile>().lifeTime = projectilLifeTime;
            StartCoroutine(waitShoot());
        }
    }

    IEnumerator waitShoot() {
        yield return new WaitForSeconds(reloadTime); 
        reloading = false;                           
    }
}
