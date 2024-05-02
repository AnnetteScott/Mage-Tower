using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform aimTransform;
    private Transform aimEndPointTransform;

    private Vector3 oldMousePositionLeft;
    private Vector3 oldMousePositionRight;
    private float initialAngleLeft;
    private float initialAngleRight;

    public Player playerScript;

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
    
    private void Awake() 
    {        
        aimEndPointTransform = aimTransform.Find("endPointPosition");
    }

    //The Mathf section makes sure the aim angle makes sense in 2D
    private void Update() {
        HandleShooting();
        mouseFollow();
    }

    private void mouseFollow() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = mousePosition - transform.position;
        difference.Normalize();

        // Calculate the angle between the player and the mouse position
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        //Player has gone from right to left and not moved mouse
        if (oldMousePositionRight.Equals(mousePosition) && playerScript.isFlipped)
        {
            angle = initialAngleRight * -1;
        }
        //Player has gone from left to right and not moved mouse
        else if (oldMousePositionLeft.Equals(mousePosition) && !playerScript.isFlipped)
        {
            angle = initialAngleLeft;
        }
        //Player is facing left and moving mouse
        else if (playerScript.isFlipped)
        {
            angle += 180;
        }

        //Player is facing left and moving mouse
        if(playerScript.isFlipped && !oldMousePositionLeft.Equals(mousePosition))
        {
            initialAngleLeft = angle * -1;
            oldMousePositionLeft = mousePosition;
        }

        //player is facing right and moving mouse
        if(!playerScript.isFlipped && !oldMousePositionRight.Equals(mousePosition))
        {
            initialAngleRight = angle;
            oldMousePositionRight = mousePosition;
        }

        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void HandleShooting() {
        if(Input.GetMouseButtonDown(0)) {
            OnShoot?.Invoke(this, new OnShootEventArgs {
                endPointPosition = aimEndPointTransform.position, shootPosition = GetMousePosition(), } );
        }
    }
}