using UnityEngine;

public class BlocController : MonoBehaviour
{
    public float speed;
    public bool IsMoved { get; set; } = true;

    void Start()
    {

    }

    void Update()
    {
        if (!IsMoved)
            return;

        transform.position += transform.up * speed * Time.deltaTime;
    }
}