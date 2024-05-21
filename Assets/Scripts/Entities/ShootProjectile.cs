using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private SpellSwap spellswap;
    [SerializeField] private Player playerScript;

    private void Awake() {
        if (spellswap == null)
        {
            Debug.LogError("SpellSwap reference is not set in the Inspector");
        }

        if (playerScript == null)
        {
            Debug.LogError("Player reference is not set in the Inspector");
        }

        GetComponent<PlayerAim>().OnShoot += ShootProjectile_OnShoot;
    }

    private void ShootProjectile_OnShoot(object sender, PlayerAim.OnShootEventArgs e) {
         if (spellswap == null || playerScript == null)
        {
            Debug.LogError("SpellSwap or PlayerScript reference is missing");
            return;
        }

        Transform spellType = spellswap.GetCurrentSpell();
        Transform bulletTranform = Instantiate(spellType, e.endPointPosition, Quaternion.identity);
        bulletTranform.GetComponent<Bullet>().Setup(e.shootDirection);
    }
} 
