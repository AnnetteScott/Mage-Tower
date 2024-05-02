using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDirection;

    public void Setup(Vector3 shootDirection) {
        this.shootDirection = shootDirection;

    }

    private void Update() {
        float moveSpeed = 45f;
        transform.position += shootDirection * moveSpeed * Time.deltaTime;
    }
}
