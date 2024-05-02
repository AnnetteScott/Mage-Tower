using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIm : Entity
{
    private Transform aimTransform;
    private Transform aimEndPointTransform;
    private Transform staffTransform;
    private SpriteRenderer playerRenderer;
    private SpriteRenderer staffRenderer;

    private Player playerScript;

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
        staffTransform = transform.Find("Staff");
        playerRenderer = GetComponent<SpriteRenderer>();

        if (aimTransform != null)
        {
            staffTransform = aimTransform.Find("Staff");

            if (staffTransform != null)
            {
                staffRenderer = staffTransform.GetComponent<SpriteRenderer>();
            }
            else
            {
                Debug.Log("Staff transform not found as a child of Aim!");
            }
        }
        else
        {
            Debug.Log("Aim transform not found!");
        }
        
        aimEndPointTransform = aimTransform.Find("endPointPosition");
    }

    //The Mathf section makes sure the aim angle makes sense in 2D
    private void Update() {
        HandleShooting();
        Vector3 mousePositionA = GetMousePosition();
        Vector3 aimDirection = (mousePositionA - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        mouseFollow();
    }

    private void mouseFollow() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = mousePosition - transform.position;
        difference.Normalize();

        // Calculate the angle between the player and the mouse position
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // Adjust the angle based on player flip state
        if (playerScript != null && playerScript.isFlipped)
        {
            angle = 180 - angle;
        }

        // Set the rotation of the aim object
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        // Set the rotation of the staff object
        if (staffTransform != null)
        {
            // Adjust staff rotation to face the same direction as aim, but flipped if necessary
            staffTransform.rotation = Quaternion.Euler(0, 0, angle);
            if (playerScript != null && playerScript.isFlipped)
            {
                staffTransform.Rotate(0, 180, 0);
            }
        }
    }

    private void HandleShooting() {
        if(Input.GetMouseButtonDown(0)) {
            OnShoot?.Invoke(this, new OnShootEventArgs {
                endPointPosition = aimEndPointTransform.position, shootPosition = GetMousePosition(), } );
        }
    }
}