using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{

    public float strength = 500;


    public void PushAwayFrom(GameObject attacker)
    {
        Vector2 knockBack = (gameObject.transform.position - attacker.transform.position).normalized * strength;
        gameObject.GetComponent<Rigidbody2D>().AddForce(knockBack);
    }
}
