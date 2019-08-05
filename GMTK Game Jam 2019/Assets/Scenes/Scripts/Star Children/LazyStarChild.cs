using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyStarChild : MonoBehaviour
{

    public float fireRate;
    private float fireTimer;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private Vector2 lastPos;
    private Vector2 currentPos;
    private float xVelocity;
    private float yVelocity;
    private Animator anim;
    private Quaternion rotationForBullet;

    public GameObject Player;
    EnemyBehavior enemyBehavior;



    // Start is called before the first frame update
    void Start()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
        fireTimer = fireRate;
        if(Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
        currentPos = new Vector2(transform.position.x, transform.position.y);
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        currentPos = new Vector2(transform.position.x, transform.position.y);
        xVelocity = currentPos.x - lastPos.x;
        yVelocity = currentPos.y - lastPos.y;
        Debug.Log(xVelocity);
        anim.SetFloat("xVelocity", xVelocity);
        anim.SetFloat("yVelocity", yVelocity);

        if (enemyBehavior.ShouldFire())
        {
            Aim();
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0)
            {
                Shoot();
                fireTimer = fireRate;
            }
        }

        

        lastPos = currentPos;
    }

    private void Aim()
    {
        Vector3 targetDir = transform.position - Player.transform.position;
        float zAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 180f;
        rotationForBullet = Quaternion.Euler(0, 0, zAngle);

    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, rotationForBullet);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
