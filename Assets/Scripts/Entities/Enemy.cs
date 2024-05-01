using UnityEngine;

public class Enemy : Entity
{
    public GameObject pointA;
    public GameObject pointB;
    public float speed;
    public Vector3 offset;
    private float distance;
    private float percentage;

    public void Start()
    {
        setHealthToMax();
    }

    private void Update()
    {
        //Distance between the two target points
        distance = Vector2.Distance(pointA.transform.position, pointB.transform.position);
        percentage += Time.deltaTime * speed / distance;

        //Lerp towards the points
        transform.position = Vector2.Lerp(pointB.transform.position, pointA.transform.position, percentage);

        //Enemy reached the target point
        if (Vector2.Distance(transform.position, pointA.transform.position) < float.Epsilon)
        {
            flip();
            swapVectors();
        }

        healthSlider.transform.position = transform.position + offset; 
    }

    /// <summary>
    /// Flip the enemy sprite along the x axis
    /// </summary>
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        percentage = 0;

    }

    /// <summary>
    /// Swap the position of the start and end points
    /// </summary>
    private void swapVectors()
    {
        Vector2 vectorTemp = pointA.transform.position;
        pointA.transform.position = pointB.transform.position;
        pointB.transform.position = vectorTemp;
    }

    /// <summary>
    /// Draw the spheres and line for the points
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().takeDamage(damage);
        }
    }
}