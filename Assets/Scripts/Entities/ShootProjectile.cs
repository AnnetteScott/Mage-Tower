using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private Transform MagicBullet;
    private void Awake() {
        GetComponent<PlayerAim>().OnShoot += ShootProjectile_OnShoot;
    }

    private void ShootProjectile_OnShoot(object sender, PlayerAim.OnShootEventArgs e) {
        Transform bulletTranform = Instantiate(MagicBullet, e.endPointPosition, Quaternion.identity);
        bulletTranform.GetComponent<Bullet>().Setup(e.shootDirection);
    }
} 
