using UnityEngine;

public class Shape : MonoBehaviour
{
    public string shapeName;

    public Rigidbody2D rb;
    public Vector2 velocity;

    public virtual void Start()
    {
        Debug.Log("Hello , my shape is " + shapeName);
        rb.linearVelocity = velocity;
    }

}
