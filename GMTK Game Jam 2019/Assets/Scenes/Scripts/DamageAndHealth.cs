using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageAndHealth : MonoBehaviour
{
    private float shakeTimer;
    private bool animate = false;
    public UnityEvent DamageEvent;

    [SerializeField]
    private int health;
    public int damage;
    [SerializeField]
    private float invincibleTime = 1;
    private float invincibleTimer = 0; 
    private DamageAndHealth target;
    public bool canTakeDamage;
    public bool canDealDamage;
    public LayerMask whatCanDamageYou;
    public LayerMask whatYouCanDamage;
    public GameObject DeathAnimation;

    public AudioClip audioClip;
    private AudioSource audioSource;

    public int GetHealth { get => health; }

    // Start is called before the first frame update
    void Start()
    {
        if (DamageEvent == null)
        {
            DamageEvent = new UnityEvent();
        }
        if (audioClip != null)
        {
            audioSource = GetComponentInChildren<AudioSource>();
            if (audioSource == null)
            {
                audioSource = GetComponentInParent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                    audioSource.volume = 0.4f;
                    //this should probably not be hardcoded, but this script is used for enemies rn:
                }
            }
        }
        invincibleTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            shakeTimer += Time.deltaTime;
        }
        if(invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {

        StartCoroutine(Shake(gameObject));
        animate = true;
        if (GetComponentInChildren<ParticleSystem>() != null)
        {
            GetComponentInChildren<ParticleSystem>().Play();
            //print("Particles!");
        }
        if (audioSource != null
            && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        //take the damage
        health -= damage;
        invincibleTimer = invincibleTime;


        //broadcast this event for UI
        DamageEvent.Invoke();

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if(DeathAnimation != null)
        {
            Instantiate(DeathAnimation, transform.position, Quaternion.identity);
        }
        if (audioClip != null)
        {
            GameObject damageAudioGameObject = Instantiate<GameObject>(new GameObject());
            AudioSource spawnedSoundSource =  damageAudioGameObject.AddComponent<AudioSource>();
            spawnedSoundSource.volume = audioSource.volume;
            spawnedSoundSource.clip = audioClip;
            spawnedSoundSource.pitch = 0.7f;
            spawnedSoundSource.Play();
            Destroy(damageAudioGameObject, audioClip.length + 0.1f);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        target = collision.gameObject.GetComponent<DamageAndHealth>();

        if (canDealDamage && target.invincibleTimer <= 0)
        {
            
            if ((whatYouCanDamage == (whatYouCanDamage | (1 << collision.gameObject.layer))) && target.canTakeDamage && (target.whatCanDamageYou == (target.whatCanDamageYou | (1 << this.gameObject.layer)))) //Daniel took the first part of this conditional off of the internet and doesn't know how it works
            {
                Debug.Log("Damage Taken!");
                target.TakeDamage(damage);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject.GetComponent<DamageAndHealth>();

        if (canDealDamage && target.invincibleTimer <= 0)
        {
            if ((whatYouCanDamage == (whatYouCanDamage | (1 << collision.gameObject.layer))) && target.canTakeDamage && (target.whatCanDamageYou == (target.whatCanDamageYou | (1 << this.gameObject.layer)))) //Daniel took the first part of this conditional off of the internet and doesn't know how it works
            {
                Debug.Log("Damage Taken!");
                target.TakeDamage(damage);
            }

        }
    }

    public void addHealth(int health)
    {
        if((this.health + health) > 10)
        {
            this.health = 10;
        }
        else
        {
            this.health += health;
        }
        //broadcast this event for UI
        DamageEvent.Invoke();
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
