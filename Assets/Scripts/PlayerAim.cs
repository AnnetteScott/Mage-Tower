using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIm : MonoBehaviour
{
    private Transform aimTransform;
    private Transform aimEndPointTransform;
    private SpriteRenderer playerRenderer;
    private SpriteRenderer staffRenderer;

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs {
        public Vector3 endPointPosition;
        public Vector3 shootPosition;

    } 

    //These GetMousePosition functions are for obtaining the location of the cursor, and are called in the update function.
    public static Vector3 GetMousePosition() {
        Vector3 vec = Input.mousePosition;
        vec.z = 0f;
        return vec;
    }
    
    private void Awake() {
        aimTransform = transform.Find("Aim");
        playerRenderer = GetComponent<SpriteRenderer>();
        staffRenderer = aimTransform.Find("Staff").GetComponent<SpriteRenderer>();
        aimEndPointTransform = aimTransform.Find("endPointPosition");
    }

    //The Mathf section makes sure the aim angle makes sense in 2D
    private void Update() {
        HandleShooting();
        Vector3 mousePositionA = GetMousePosition();
        Vector3 aimDirection = (mousePositionA - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

         // Check if the mouse is to the left or right of the player
        if (mousePositionA.x < transform.position.x) {
            // Flip the player sprite to face left
            playerRenderer.flipX = true;
            //Flip the staff sprite vertically
            staffRenderer.flipY = true;
        } else {
            // Flip the player sprite to face right
            playerRenderer.flipX = false;
            // Reset the staff sprite
            staffRenderer.flipY = false;
        }

    }

    private void HandleShooting() {
        if(Input.GetMouseButtonDown(0)) {
            OnShoot?.Invoke(this, new OnShootEventArgs {
                endPointPosition = aimEndPointTransform.position, shootPosition = GetMousePosition(), } );
        }
    }
}