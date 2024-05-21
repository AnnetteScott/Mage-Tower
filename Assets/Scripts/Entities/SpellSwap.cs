using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSwap : MonoBehaviour
{
    [SerializeField] private Transform MagicBullet;
    [SerializeField] private Transform BouncyBullet;

    private Transform currentSpell;

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
    }

    public Transform GetCurrentSpell()
    {
        return currentSpell;
    }
}
