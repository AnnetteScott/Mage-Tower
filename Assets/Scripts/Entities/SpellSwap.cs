using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSwap : MonoBehaviour
{
    [SerializeField] private Transform MagicBullet;
    [SerializeField] private Transform BouncyBullet;
    [SerializeField] private Transform FireSpark;

    [SerializeField] private Sprite magicStaffSprite;
    [SerializeField] private Sprite bouncyStaffSprite;
    [SerializeField] private Sprite fireStaffSprite;

    private Transform currentSpell;
    private SpriteRenderer staffRenderer;
    private Player playerScript; 

    //Mana costs for each spell
    private int mbCost = 2;
    private int bbCost = 4;
    private int fsCost = 1;

    private void Start()
    {
        // Set the default spell to MagicBullet
        currentSpell = MagicBullet;
        // Find the renderer of the staff
        staffRenderer = transform.Find("Aim/Staff").GetComponent<SpriteRenderer>();
        playerScript = GetComponent<Player>();
    }

    private void Update()
    {
        //Check if the player has reached level 2 and unlocked bouncy spells
        if(playerScript.getLevel() >= 3) {
            // Check for input to swap spells
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentSpell = MagicBullet;
                staffRenderer.sprite = magicStaffSprite;
                Debug.Log("Switched to Magic Bullet");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentSpell = BouncyBullet;
                staffRenderer.sprite = bouncyStaffSprite;
                Debug.Log("Switched to Bouncy Bullet");
            }

            //Check if the player has reached level 3 and unlocked fire spark
            if(playerScript.getLevel() >= 5) {
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    currentSpell = FireSpark;
                    staffRenderer.sprite = fireStaffSprite;
                    Debug.Log("Switched to Fire Spark");
                }
            }
        } 
    }

    public Transform GetCurrentSpell()
    {
        return currentSpell;
    }

    public int GetCurrentSpellManaCost() {
        if(currentSpell == MagicBullet) {
            return mbCost;
        }
        else if (currentSpell == BouncyBullet) {
            return bbCost;
        }
        else if (currentSpell == FireSpark) {
            return fsCost;
        } else {
            return 0; //default cost if no spell is selected
        }
    }
}
