using UnityEngine;

public class Attraction : MonoBehaviour
{
    [SerializeField] private float attractionForce = 10f; // Force d'attraction vers le centre


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Emission"))
        {
            transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, attractionForce * Time.deltaTime);
        }
    }

}