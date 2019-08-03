using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public bool InstantMotion = true;
    public float Speed = 5f;
    public GameObject Orbitter;
    public GameObject Bullet;
    private Rigidbody2D BulletRB;
    public LayerMask bulletCollisionMask;
    public Transform raycastFirePoint;
    public bool repeatedFire = false;

    bool LookingForPlayer = false;

    Rigidbody2D rBody;
    // Start is called before the first frame update

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        BulletRB = Bullet.GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        
        if(InstantMotion)
        {
            InstantMove();
        }
        else
        {
            Move();
        }

        if(!repeatedFire)
        {
            if(Bullet.transform.parent != null)
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
            Debug.Log("returning");
            ReturnProjectile();
        }

        Debug.Log((Bullet.transform.position - Orbitter.transform.position).magnitude);

        if (BulletRB.isKinematic == false) {
            BounceProjectile();
        }

        if (LookingForPlayer)
        {
            Vector2 direction = Orbitter.transform.position - Bullet.transform.position;
            direction.Normalize();
            BulletRB.isKinematic = true;
            BulletRB.velocity = direction * 30;
            if ((Bullet.transform.position - Orbitter.transform.position).magnitude <= 3.0f)
            {
                Bullet.transform.parent = Orbitter.transform;
                BulletRB.velocity = new Vector2(0, 0);
                LookingForPlayer = false;
            }
        }
       
    }

    private void Shoot()
    {
        Bullet.transform.parent = null;
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Bullet.transform.position;
        direction.Normalize();
        BulletRB.isKinematic = false;
        BulletRB.velocity = direction * 30;
        LookingForPlayer = false;
    }

    private void ReturnProjectile()
    {

        Vector2 direction = Orbitter.transform.position - Bullet.transform.position;
        direction.Normalize();
        BulletRB.isKinematic = true;
        BulletRB.velocity = direction * 30;
        LookingForPlayer = true;
    }

    private void BounceProjectile()
    {
        Ray ray = new Ray(raycastFirePoint.position, transform.up); 
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Sqrt(Mathf.Pow(BulletRB.velocity.x,2) + Mathf.Pow(BulletRB.velocity.y,2)) + 0.1f, bulletCollisionMask))
        {
            
            Vector2 reflectDir = Vector2.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, rot);
        }
    }

    //checks input axis and sets a velocity for rBody
    private void InstantMove()
    {

        if (!Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 0) && !Mathf.Approximately(Input.GetAxisRaw("Vertical"), 0))
        {
            //rBody.velocity = new Vector3(0,0,0);
            rBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Speed * 100 * Time.deltaTime, Input.GetAxisRaw("Vertical") * Speed * 100 * Time.deltaTime) * 0.707f;
        }

        else if (!Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 0) || !Mathf.Approximately(Input.GetAxisRaw("Vertical"), 0))
        {
            //rBody.velocity = new Vector3(0, 0, 0);
            rBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Speed * 100 * Time.deltaTime, Input.GetAxisRaw("Vertical") * Speed * 100 * Time.deltaTime);
        }
        else
        {
            rBody.velocity = new Vector3(0,0,0);
        }
    }

    private void Move()
    {

        if (!Mathf.Approximately(Input.GetAxis("Horizontal"), 0) && !Mathf.Approximately(Input.GetAxis("Vertical"), 0))
        {
            //rBody.velocity = new Vector3(0,0,0);
            rBody.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed * 100 * Time.deltaTime, Input.GetAxis("Vertical") * Speed * 100 * Time.deltaTime) * 0.707f;
        }

        else if (!Mathf.Approximately(Input.GetAxis("Horizontal"), 0) || !Mathf.Approximately(Input.GetAxis("Vertical"), 0))
        {
            //rBody.velocity = new Vector3(0, 0, 0);
            rBody.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed * 100 * Time.deltaTime, Input.GetAxis("Vertical") * Speed * 100 * Time.deltaTime);
        }
        else
        {
            rBody.velocity = new Vector3(0, 0, 0);
        }
    }
}