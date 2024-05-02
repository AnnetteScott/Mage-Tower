using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private Transform MagicBullet;
    private void Awake() {
        GetComponent<PlayerAIm>().OnShoot += ShootProjectile_OnShoot;
    }

    private void ShootProjectile_OnShoot(object sender, PlayerAIm.OnShootEventArgs e) {
        Transform bulletTranform = Instantiate(MagicBullet, e.endPointPosition, Quaternion.identity);
        Bullet bullet = bulletTranform.GetComponent<Bullet>();

        if(bullet != null)
        {
            Vector3 shootDirection = (e.shootPosition - e.endPointPosition).normalized;
            bulletTranform.GetComponent<Bullet>().Setup(shootDirection);
        }
    }
} 
