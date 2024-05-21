using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform aimTransform;
    private Transform aimEndPointTransform;
    private Transform staffTransform;

    public Vector3 endPointOffset = new Vector3(0.75f, 1f, 0f);

    private float initialAngleLeft;
    private float initialAngleRight;
    private bool cursorBehindPlayer = false;
    private SpriteRenderer playerRenderer;
    private SpriteRenderer staffRenderer;

    public Player playerScript;
    private SpellSwap spellswap;

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs {
        public Vector3 endPointPosition;
        public Vector3 shootDirection;
    } 
    
    private void Awake() 
    {        
        aimEndPointTransform = aimTransform.Find("endPointPosition");
        playerRenderer = GetComponent<SpriteRenderer>();
        
        // Find the renderer of the staff
        staffTransform = aimTransform.Find("Staff");
        if (staffTransform != null)
        {
            staffRenderer = staffTransform.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogError("Staff transform not found as a child of Aim!");
        }

        spellswap = FindObjectOfType<SpellSwap>();
        if (spellswap == null) {
            Debug.LogError("SpellSwap reference is not set in the Inspector");
        }
    }

    //The Mathf section makes sure the aim angle makes sense in 2D
    private void Update() {
        HandleShooting();
        mouseFollow();
    }

    private void mouseFollow()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = mousePosition - transform.position;
        difference.Normalize();

        // Calculate the angle between the player and the mouse position
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // Flip player based on cursor position
        if (difference.x >= 0)
        {
            playerRenderer.flipX = false;
            staffRenderer.flipX = false;
        }
        else
        {
            playerRenderer.flipX = true;
        }

        // Flip player and staff if the cursor is behind the player and it wasn't already flipped
        bool newCursorBehindPlayer = (transform.position.x > mousePosition.x && !playerScript.isFlipped) ||
                                  (transform.position.x < mousePosition.x && playerScript.isFlipped);

        if (newCursorBehindPlayer && !cursorBehindPlayer)
        {
            playerRenderer.flipX = !playerRenderer.flipX;
            staffRenderer.flipX = !staffRenderer.flipX;
            cursorBehindPlayer = true;
        }
        else if (!newCursorBehindPlayer && cursorBehindPlayer)
        {
            cursorBehindPlayer = false;
        }

        // Adjust staff rotation based on player flip state
        float staffAngle = angle;
        if (playerRenderer.flipX)
        {
            staffAngle += 180f; // Keep staff upright when player is flipped
        }
        aimTransform.eulerAngles = new Vector3(0, 0, staffAngle);
        UpdateEndPointPosition();
    }

    private void UpdateEndPointPosition()
    {
        // Calculate the direction of the staff based on its rotation
        Vector3 staffDirection = Vector3.right;
        if (staffRenderer.flipX)
        {
            // If the staff is flipped, adjust the direction accordingly
            staffDirection = Vector3.left;
        }

        // Calculate the new position of the endPointPosition relative to the staff's position
        Vector3 newEndPointPosition = staffTransform.position + staffDirection * endPointOffset.x + Vector3.up * endPointOffset.y;

        // Apply the calculated position to the endPointPosition GameObject
        aimEndPointTransform.position = newEndPointPosition;
    }

    private void HandleShooting() {
        //Get the current spell from SpellSwap
        Transform currentSpell = spellswap.GetCurrentSpell();

        //Get the mana cost for the current spell
        int manaCost = currentSpell.GetComponent<Bullet>().manaCost;

        if(Input.GetMouseButtonDown(1) && playerScript.useMana(manaCost)) {
            Vector3 mousePositionA = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            OnShoot?.Invoke(this, new OnShootEventArgs {
                endPointPosition = aimEndPointTransform.position,
                shootDirection = mousePositionA, // Passing the normalized shoot direction
            });
        }
    }
}