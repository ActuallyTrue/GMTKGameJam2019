using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyStarChild : MonoBehaviour
{

    public float fireRate;
    private float fireTimer;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject Player;
    public EnemyBehavior enemyBehavior;



    // Start is called before the first frame update
    void Start()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
        fireTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBehavior.ShouldFire())
        {
            Aim(Player);
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0)
            {
                Shoot();
                fireTimer = fireRate;
            }
        }
    }


    private void Aim(GameObject Player)
    {
        Vector3 targetDir = transform.position - Player.transform.position;
        float zAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 180f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
