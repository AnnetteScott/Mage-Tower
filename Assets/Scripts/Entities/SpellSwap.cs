using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSwap : MonoBehaviour
{
    [SerializeField] private Transform MagicBullet;
    [SerializeField] private Transform BouncyBullet;
    [SerializeField] private Transform FireSpark;

    private Transform currentSpell;

    //Mana costs for each spell
    private int mbCost = 2;
    private int bbCost = 4;
    private int fsCost = 1;

    private void Start()
    {
        // Set the default spell to MagicBullet
        currentSpell = MagicBullet;
    }

    private void Update()
    {
        // Check for input to swap spells
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentSpell = MagicBullet;
            Debug.Log("Switched to Magic Bullet");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentSpell = BouncyBullet;
            Debug.Log("Switched to Bouncy Bullet");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSpell = FireSpark;
            Debug.Log("Switched to Fire Spark");
        }
    }

    public Transform GetCurrentSpell()
    {
        return currentSpell;
    }

    public int GetCurrentSpellManaCost() {
        if(currentSpell = MagicBullet) {
            return mbCost;
        }
        else if (currentSpell = BouncyBullet) {
            return bbCost;
        }
        else if (currentSpell = FireSpark) {
            return fsCost;
        } else {
            return 0; //default cost if no spell is selected
        }
    }
}
