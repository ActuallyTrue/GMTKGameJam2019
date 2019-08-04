using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitControls : MonoBehaviour
{

    public bool BulletDefending = true;
    public GameObject Bullet;
    public float OrbitDistance = 3;
    public float BulletSpeed = 30;
    private Rigidbody2D BulletRB;

    public bool repeatedFire = false;

    //sounds
    private AudioSource audioSource;
    public AudioClip fireSFX;
    public AudioClip returnSFX;
    public AudioClip[] swingSFX;


    bool LookingForPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        BulletRB = Bullet.GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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
                if (!audioSource.isPlaying && swingSFX.Length > 0)
                {
                    int source = Random.Range(0, swingSFX.Length);
                    audioSource.pitch = Random.Range(0.8f, 1.2f);
                    audioSource.PlayOneShot(swingSFX[source]);
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
                BulletDefending = true;

                //if bullet too close, sets it to be a bit farther away 
                if ((Bullet.transform.position - gameObject.transform.position).magnitude <= OrbitDistance - 1)
                {
                    Bullet.transform.localPosition = Bullet.transform.localPosition.normalized * OrbitDistance;
                }
            }
        }
    }

    public void Shoot()
    {
        print("Shoot");
        Bullet.transform.parent = null;
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Bullet.transform.position;
        direction.Normalize();
        BulletRB.isKinematic = false;
        BulletRB.velocity = direction * BulletSpeed;
        LookingForPlayer = false;
        BulletDefending = false;

        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(fireSFX);
    }

    //tells the bullet to return to the player and makes it so the bullet is looking for the player to attach.
    private void ReturnProjectile()
    {
        Vector2 direction = gameObject.transform.position - Bullet.transform.position;
        direction.Normalize();
        BulletRB.isKinematic = true;
        BulletRB.velocity = direction * BulletSpeed;
        LookingForPlayer = true;
        if (!audioSource.isPlaying)
        {
            audioSource.pitch = 1.0f;
            audioSource.PlayOneShot(returnSFX);
        }
    }
}
