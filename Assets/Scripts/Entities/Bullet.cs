using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 20f;
    private Vector2 endPoint;
    private Vector2 startPoint;
    private float distance;
    private float percentage;

    public int damage = 4;

    private void Start() {
        startPoint = transform.position;
    }

    public void Setup(Vector3 end) 
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

    public float getDamage()
    {
        float damageModifier = 1;

        if (GlobalData.equippedStaffItem != null)
        {
            var resource = Resources.Load(GlobalData.equippedStaffItem);
            GameObject newInstance = Instantiate(resource) as GameObject;
            damageModifier = newInstance.GetComponent<Weapon>().power;
            Destroy(newInstance);
        }

        return damage * damageModifier;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
