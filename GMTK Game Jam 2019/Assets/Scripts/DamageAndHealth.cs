using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAndHealth : MonoBehaviour
{
    public int health;
    public int damage;
    private DamageAndHealth target;
    public bool canTakeDamage;
    public bool canDealDamage;
    public LayerMask whatCanDamageYou;
    public LayerMask whatYouCanDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (GetComponentInChildren<ParticleSystem>() != null)
        {
            GetComponentInChildren<ParticleSystem>().Play();
            print("Particles!");
        }
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
 
        if (canDealDamage)
        {
            target = collision.gameObject.GetComponent<DamageAndHealth>();
            if ((whatYouCanDamage == (whatYouCanDamage | (1 << collision.gameObject.layer))) && target.canTakeDamage) //Daniel took the first part of this conditional off of the internet and doesn't know how it works
            {
                Debug.Log("Damage Taken!");
                target.TakeDamage(damage);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Damage");
        if (canDealDamage)
        {
            target = collision.gameObject.GetComponent<DamageAndHealth>();

            if ((whatYouCanDamage == (whatYouCanDamage | (1 << collision.gameObject.layer))) && target.canTakeDamage) //Daniel took the first part of this conditional off of the internet and doesn't know how it works
            {
                Debug.Log("Damage Taken!");
                target.TakeDamage(damage);
            }

        }
    }


}
