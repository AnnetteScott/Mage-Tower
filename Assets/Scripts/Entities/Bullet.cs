using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float moveSpeed = 45f;
    private Vector3 moveDirection;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
    }

    public static float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(n < 0) n += 360;

        return n;
    }

    public void Setup(Vector3 shootDir) {
        moveDirection = shootDir.normalized * moveSpeed;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 4f);
    }
        
    private void Update()
    {
        transform.position += moveDirection * Time.deltaTime;
    }
}
