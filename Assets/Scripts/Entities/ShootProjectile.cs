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
            spellswap = FindObjectOfType<SpellSwap>();
            if (spellswap == null) {
                Debug.LogError("SpellSwap reference is not set in the Inspector");
            }
        }

        if (playerScript == null)
        {
            playerScript = FindObjectOfType<Player>();
            if (playerScript == null) {
                Debug.LogError("Player reference is not set in the Inspector");
            }
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
        // Calculate the angle between the shooting direction and the right direction (default bullet direction)
        Vector3 shootDirection = e.shootDirection - e.endPointPosition;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        
        Transform bulletTransform = Instantiate(spellType, e.endPointPosition, Quaternion.Euler(0, 0, angle));
        bulletTransform.GetComponent<Bullet>().Setup(e.shootDirection);
    }
} 
