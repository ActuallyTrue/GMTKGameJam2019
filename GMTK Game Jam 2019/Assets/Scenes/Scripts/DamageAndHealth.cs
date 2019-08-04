using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAndHealth : MonoBehaviour
{
    private float shakeTimer;
    private bool animate = false;

    public int health;
    public int damage;
    private DamageAndHealth target;
    public bool canTakeDamage;
    public bool canDealDamage;
    public LayerMask whatCanDamageYou;
    public LayerMask whatYouCanDamage;
    public GameObject DeathAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            shakeTimer += Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(Shake(gameObject));
        animate = true;
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
        if(!DeathAnimation.Equals(null))
        {
            Instantiate(DeathAnimation, transform.position, Quaternion.identity);
        }

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

    IEnumerator Shake(GameObject character, float animLength = 0.25f, float animPower = 1) //Shake violently, courtesy of project Unleavened
    {
        float power = animPower * 5 * Time.deltaTime;

        bool notDone = true;
        while (notDone)
        {
            if ((shakeTimer < (animLength)))
            {
                character.transform.Translate(UnityEngine.Random.Range(-power, power), UnityEngine.Random.Range(-power, power), 0);
                yield return null;
            }
            else
            {
                //ResetPositions();
                //end this thread
                shakeTimer = 0f;
                notDone = false;
                //AnimStateReset();
            }
        }
    }

}
