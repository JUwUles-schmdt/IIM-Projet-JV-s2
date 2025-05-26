using UnityEngine;

public class ChekpointController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            other.GetComponent<PlayerController>().lastChekpoint = transform;
        }
    }
}
