using UnityEngine;

public class Attraction : MonoBehaviour
{
    public float attractionForce = 10f; // Force d'attraction vers le centre

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (other.gameObject.layer == LayerMask.NameToLayer("Emission"))
        {
            Vector3 center = GetComponent<Collider>().bounds.center;
            Vector3 direction = (center - other.transform.position).normalized;

            // Appliquer la force vers le centre
            rb.AddForce(direction * attractionForce);
        }
    }
}