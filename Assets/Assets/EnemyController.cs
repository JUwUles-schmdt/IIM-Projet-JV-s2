using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public LayerMask playerlayer;
    public Transform playercheck;

    void Start()
    {
        
    }

    void Update()
    {
        StartCoroutine(playerchecker());
    }

    IEnumerator playerchecker()
    {
        if (Physics2D.OverlapCapsule(playercheck.position, new Vector2(.5f,.1f), CapsuleDirection2D.Horizontal, 0, playerlayer))
        {
            yield return new WaitForSeconds(0.2f);
            Destroy(gameObject); 
        }
    }
}
