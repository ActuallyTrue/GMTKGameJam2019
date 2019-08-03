using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float speed = 5f;

    Rigidbody2D rBody;
    // Start is called before the first frame update

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    //checks input axis and sets a velocity for rBody
    private void Move()
    {

        if (Input.GetAxis("Horizontal") != 0 & Input.GetAxis("Vertical") != 0)
        {
            rBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * 100 * Time.deltaTime, Input.GetAxis("Vertical") * speed * 100 * Time.deltaTime) * 0.707f;
        }

        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            rBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * 100 * Time.deltaTime, Input.GetAxis("Vertical") * speed * 100 * Time.deltaTime);
        }
        else
        {
            rBody.velocity = rBody.velocity * 0;
        }
    }
}