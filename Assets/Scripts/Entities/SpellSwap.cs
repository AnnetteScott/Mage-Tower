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
    private SpriteRenderer staffCrystal;
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
        staffCrystal = transform.Find("Aim/Staff/StaffCrystal").GetComponent<SpriteRenderer>();
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
                staffCrystal.sprite = magicStaffSprite;
                Debug.Log("Switched to Magic Bullet");
                TestSwitch(KeyCode.Alpha1); // Call TestSwitch after switching to different spell
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentSpell = BouncyBullet;
                staffCrystal.sprite = bouncyStaffSprite;
                Debug.Log("Switched to Bouncy Bullet");
                TestSwitch(KeyCode.Alpha2); // Call TestSwitch after switching to different spell
            }

            //Check if the player has reached level 3 and unlocked fire spark
            if(playerScript.getLevel() >= 5) {
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    currentSpell = FireSpark;
                    staffCrystal.sprite = fireStaffSprite;
                    Debug.Log("Switched to Fire Spark");
                    TestSwitch(KeyCode.Alpha3); // Call TestSwitch after switching to different spell
                }
            }
        } 
    }

    private void TestSwitch(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Alpha1:
                if (currentSpell == MagicBullet && staffCrystal.sprite == magicStaffSprite) {
                    Debug.Log("All tests passed");
                } else {
                    Debug.Log("Test failed");
                }
                break;
            case KeyCode.Alpha2:
                if (currentSpell == BouncyBullet && staffCrystal.sprite == bouncyStaffSprite) {
                    Debug.Log("All tests passed");
                } else {
                    Debug.Log("Test failed");
                }
                break;
            case KeyCode.Alpha3:
                if (currentSpell == FireSpark && staffCrystal.sprite == fireStaffSprite) {
                    Debug.Log("All tests passed");
                } else {
                    Debug.Log("Test failed");
                }
                break;
            default:
                Debug.Log("Invalid key pressed for testing");
                break;
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
