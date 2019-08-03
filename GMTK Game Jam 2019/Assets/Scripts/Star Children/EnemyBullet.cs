using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rBody2D;
    public float TimeUntilDestroy = 2;

    // Start is called before the first frame update
    void Start()
    {
        rBody2D.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        TimeUntilDestroy -= Time.deltaTime;

        if (TimeUntilDestroy < 0) {
            Object.Destroy(gameObject);
            TimeUntilDestroy = 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Player")
        {
            //Kill the player
        }
        else if (hitInfo.tag == "PlayerShield")
        {
            //Decrease shield health by one
        }
    }


}
