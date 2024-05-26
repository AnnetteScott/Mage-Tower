using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 20f;
    protected Vector2 endPoint;
    protected Vector2 startPoint;
    private float distance;
    private float percentage;

    public int damage = 4;
    public int manaCost = 2;

    protected virtual void Start() {
        startPoint = transform.position;
    }

    public virtual void Setup(Vector3 end) 
    {
        endPoint = end;
    }
        
    private void FixedUpdate()
    {
        distance = Vector2.Distance(startPoint, endPoint);
        //Distance between the two target points
        percentage += Time.deltaTime * moveSpeed / distance;

        //Lerp towards the points
        transform.position = Vector2.Lerp(startPoint, endPoint, percentage);

        //Spell reached the target point
        if (Vector2.Distance(transform.position, endPoint) < float.Epsilon)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
