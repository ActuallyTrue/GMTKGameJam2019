using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitControls : MonoBehaviour
{

    public GameObject Bullet;
    public float OrbitDistance = 3;
    public float BulletSpeed = 30;
    private Rigidbody2D BulletRB;

    public bool repeatedFire = false;

    bool LookingForPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        BulletRB = Bullet.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!repeatedFire)
        {
            if (Bullet.transform.parent != null)
            {
                if (Input.GetAxis("Fire1") != 0)
                {
                    Shoot();
                }
            }
        }

        else
        {
            if (Input.GetAxis("Fire1") != 0)
            {
                Shoot();
            }
        }

        if (Input.GetAxis("Fire2") != 0)
        {
            ReturnProjectile();
        }

        if (LookingForPlayer)
        {
            Vector2 direction = gameObject.transform.position - Bullet.transform.position;
            direction.Normalize();
            BulletRB.isKinematic = true;
            BulletRB.velocity = direction * BulletSpeed;
            if ((Bullet.transform.position - gameObject.transform.position).magnitude <= OrbitDistance)
            {
                Bullet.transform.parent = gameObject.transform;
                BulletRB.velocity = new Vector2(0, 0);
                LookingForPlayer = false;

                //if bullet too close, sets it to be a bit farther away 
                if ((Bullet.transform.position - gameObject.transform.position).magnitude <= OrbitDistance - 1)
                {
                    Bullet.transform.localPosition = Bullet.transform.localPosition.normalized * OrbitDistance;
                }
            }
        }
    }

    private void Shoot()
    {
        Bullet.transform.parent = null;
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Bullet.transform.position;
        direction.Normalize();
        BulletRB.isKinematic = false;
        BulletRB.velocity = direction * BulletSpeed;
        LookingForPlayer = false;
    }

    //tells the bullet to return to the player and makes it so the bullet is looking for the player to attach.
    private void ReturnProjectile()
    {
        Vector2 direction = gameObject.transform.position - Bullet.transform.position;
        direction.Normalize();
        BulletRB.isKinematic = true;
        BulletRB.velocity = direction * BulletSpeed;
        LookingForPlayer = true;
    }
}
